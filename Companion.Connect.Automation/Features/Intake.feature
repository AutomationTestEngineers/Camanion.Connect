Feature: Intake
	In order Execute Intake Functionality

Background: 
	Given I Login

@Smoke
Scenario Outline: Test_001_Login112	
	When I Change Shelter "<ShelterName>"
	And I Search "<Search>"
	Then User Should See Search Reasult "<Search>"
	When I Click Intake "<Intake>"
	
Examples: 
| ShelterName                     | Search | Intake        | HeaderName     |
| Central Missouri Humane Society | K      | animalcontrol | Animal Control |
	


