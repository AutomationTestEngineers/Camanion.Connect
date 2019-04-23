Feature: Smoke
	In order Execute Basic Functionality

Background: 
	Given I Enter "inazir@companionprotect.com,Muridke@1"
	Then User Should see "inazir@companionprotect.com"

@Smoke
Scenario Outline: Test_001_Login112	
	When I Change Shelter "<ShelterName>"
	And I Search "<Search>"
	Then User Should See Search Reasult "<Search>"
	When I Click Intake "<Intake>"
	#Then User Should See Intake Header "<Intake>"
	
Examples: 
| ShelterName                     | Search | Intake        |
| Central Missouri Humane Society | K      | animalcontrol |
	

@Smoke1
Scenario Outline: Test_001_Login221
	When I Change Shelter "<ShelterName>"
	And I Search "<Search>"
	Then User Should See Search Reasult "<Search>"
	When I Click Intake "<Intake>"
	#Then User Should See Intake Header "<Intake>"
	
Examples: 
| ShelterName                     | Search | Intake        |
| Central Missouri Humane Society | K      | animalcontrol |

