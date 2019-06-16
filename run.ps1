
param(
	[string]$NewVersion='',
	[string]$Category=''
)
function SetParameter($key, $value){
	$parametersXMLName = "$PSScriptRoot\$PROJECTNAME\bin\Debug\Parameter.xml"
	[xml]$parametersXML = New-Object System.Xml.XmlDocument
	$parametersXML.Load($parametersXMLName)
	$xpathNavigator = $parametersXML.CreateNavigator()
	$xpathNavigator.SelectSingleNode([System.String]::Format("Parameter/{0}", $key)).SetValue($value)	
	$parametersXML.Save($parametersXMLName)
}
function SendMail($html,$cat){
	$DateTime = get-date -format ddd_dd.MM.yyyy_HH.mm
	$emailSmtpServer = "smtp.gmail.com"
	$emailSmtpServerPort = “587”
	$emailSmtpUser = "Automation.Test.Engineers@gmail.com"
	$emailSmtpPass = "Automation@123"
	$emailMessage = New-Object System.Net.Mail.MailMessage
	$emailMessage.From = New-Object MailAddress("ch.pradeep4026@gmail.com", "Pradeep Chinthala" );
	$emailMessage.To.Add( "ranaim76@hotmail.com" )
	$emailMessage.To.Add( "ch.pradeep26@gmail.com" )
	$emailMessage.Subject = “Comapanion Connect Result On $DateTime”
	$emailMessage.IsBodyHtml = “True”
	$emailMessage.Priority = 1
	$attach = new-object Net.Mail.Attachment($html) 
	$emailMessage.Attachments.Add($attach)
$emailMessage.Body = @”
    <br />
    Hi, 
    <br />
    <br />
	<strong>Executed Category: $cat</strong>
    <br />
	<br />
	Thanks,
    <br />
    Pradeep CH
    <br />
    QA Automation.
“@
	$SMTPClient = New-Object System.Net.Mail.SmtpClient( $emailSmtpServer , $emailSmtpServerPort )
	$SMTPClient.EnableSsl = $true
	$SMTPClient.Credentials = New-Object System.Net.NetworkCredential( $emailSmtpUser , $emailSmtpPass );
	$SMTPClient.Send($emailMessage)
}
try
{
	#%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% Usage %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
	# .\run.ps1
	# Change the Category if you want to execute all the test cases Example ::====>>>> [string]$TESTSELECT = 'cat == E2E'

	# Result:
	# All Results will Saved in [C:\Automation] Location with DateTime
	# All SrcenShots Will Saved [C:\evidence] Location with Test Case Name
	#%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

	[string]$PROJECTNAME = 'Companion.Connect.Automation'
	[string]$TESTSELECT = 'E2E'
	[string]$Ignore=''
	[bool] $Nuget = $false
	
	$DateTime = get-date -format ddd_dd.MM.yyyy_HH.mm.ss
	$RESULTDIR="C:\Automation\$DateTime"
	
	#Start-Transcript -path C:\Automation\${DateTime}\logs\log_${DateTime}_$environment.log    
    Start-Transcript -path $RESULTDIR\logs\log_${DateTime}_$environment.log	
	Set-Location $PSScriptRoot

	# To Run Get Category 
	if($Category -ne ''){
		[string]$TESTSELECT =$Category
	}
    # Parameter Setup For New Vesion 
	if($NewVersion -ne ''){
		SetParameter "NewVersion" $NewVersion
	}
	# For Intakes Test no need to execute other tests
	if($TESTSELECT -ne 'Intake')
	{
		$Ignore += ' && cat != Intake'
	}
	#Update Path
	$listDirectories = Get-ChildItem -Path .\packages -Include tools* -Recurse -Directory | Select-Object FullName
	foreach($directory in $listDirectories.FullName) {
		$env:Path+=";"+$directory
	}
	# File Names
	$SOLUTION="$PSScriptRoot\Companion.Connect.Automation.sln"
	$PROJECT="$PSScriptRoot\$PROJECTNAME\$PROJECTNAME.csproj"
	$RESULTLOG="$RESULTDIR\$DATEYYYYMMDD"+"TestResult"+"$ENVIRONMENT.log"
	$RESULTXML="$RESULTDIR\$DATEYYYYMMDD"+"TestResult"+"$ENVIRONMENT.xml"
	$RESULTERR="$RESULTDIR\$DATEYYYYMMDD"+"StdErr"+"$ENVIRONMENT.txt"
	$RESULOUTTXT="$RESULTDIR\$DATEYYYYMMDD"+"TestResult"+"$ENVIRONMENT.txt"
	#$FEATURESDIR="$PSScriptRoot\$PROJECTNAME\Features"
	$XSLTFILE="$PSScriptRoot\NUnitExecutionReport.xslt"
    $OUTHTML = "$RESULTDIR\$ENVIRONMENT_${DateTime}.html"

	if($Nuget)  # To Restore the NuGet packages
	{
		$args = @("restore", $SOLUTION) 
        & NuGet $args
	}	

	# Execute Tests
    $OUTPUT  = nunit3-console --out=$RESULOUTTXT --framework=net-4.5 --result="$RESULTXML;format=nunit2" $PROJECT --where "cat == $TESTSELECT$Ignore"
    $OUTPUT | Out-File $RESULTLOG 
    Write-Host $OUTPUT -Separator "`n"
    $TESTRESULTS = $OUTPUT | Select-String 'Test Count'
    $DURATION = $OUTPUT | Select-String 'Duration: '

    (Get-Content $RESULOUTTXT) | ForEach-Object { $_ -replace '=>', '*****' } | Set-Content $RESULOUTTXT

	#Generate Html Report
    specflow nunitexecutionreport --ProjectFile=$PROJECT --xmlTestResult=$RESULTXML --testOutput=$RESULOUTTXT --OutputFile=$OUTHTML 
	#Send Email
	SendMail $OUTHTML $TESTSELECT
	Stop-Transcript
}
Catch
{
	Stop-Transcript
    write-host $_.Exception.Message;
}
