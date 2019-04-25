Feature: SpecFlowFeature2
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
	Given I Login

@Smoke
Scenario Outline: Test_001_Login112	
	When I Change Shelter "<ShelterName>"
	And I Search "<Search>"
	Then User Should See Search Reasult "<Search>"
	When I Click Intake "<Intake>"
	Then User Should See Intake Header "<HeaderName>"
	When I Search Partner "K"
	
Examples: 
| ShelterName                     | Search | Intake        | HeaderName     |
| Central Missouri Humane Society | K      | animalcontrol | Animal Control |
