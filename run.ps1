try
{
	#%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% Usage %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
	# .\run.ps1
	# Change the Category if you want to execute all the test cases Example ::====>>>> [string]$TESTSELECT = 'cat == E2E'


	# Result:
	# All Results Stored in [C:\Automation] Location with DateTime
	# All SrcenShots Stores [C:\evidence] Location with Test Case Name
	#%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


	[string]$PROJECTNAME = 'Companion.Connect.Automation'
	[string]$TESTSELECT = 'PublicStray'
	
	$DateTime = get-date -format ddd_dd.MM.yyyy_HH.mm.ss
	$RESULTDIR="C:\Automation\$DateTime"
	
	#Start-Transcript -path C:\Automation\${DateTime}\logs\log_${DateTime}_$environment.log    
    Start-Transcript -path $RESULTDIR\logs\log_${DateTime}_$environment.log	
	Set-Location $PSScriptRoot

	## For E2E tests Should not execute Individual Intake Tests
	if($TESTSELECT -eq 'E2E')
	{
	# If E2E tag present then intake tag should skip 
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
	$EXECUTIONREP="$env:USERPROFILE\$DATEYYYYMMDD"+"AutomationTest.ExecutionReport"+"$CONFIGURATION"
	$FEATURESDIR="$PSScriptRoot\$PROJECTNAME\Features"
	$XSLTFILE="$PSScriptRoot\NUnitExecutionReport.xslt"

    $OUTHTML = "$RESULTDIR\$ENVIRONMENT_${DateTime}.html"


	# Execute Tests
    $OUTPUT  = nunit3-console --out=$RESULOUTTXT --framework=net-4.5 --result="$RESULTXML;format=nunit2" $PROJECT --where "cat == $TESTSELECT"
	
    $OUTPUT | Out-File $RESULTLOG 
    Write-Host $OUTPUT -Separator "`n"
    $TESTRESULTS = $OUTPUT | Select-String 'Test Count'
    $DURATION = $OUTPUT | Select-String 'Duration: '

    (Get-Content $RESULOUTTXT) | ForEach-Object { $_ -replace '=>', '*****' } | Set-Content $RESULOUTTXT

	# Generate Html Report
    #specflow nunitexecutionreport --ProjectFile=$PROJECT --xmlTestResult=$RESULTXML --testOutput=$RESULOUTTXT --OutputFile=$OUTHTML --XsltFile=$XSLTFILE   
	 

	Stop-Transcript
}
Catch
{
	Stop-Transcript
    write-host $_.Exception.Message;
}