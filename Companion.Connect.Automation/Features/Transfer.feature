@Transfer @E2E
Feature: Transfer
	In order Execute Transfer Intake To Outcome Functionality

Background: 
	Given I Login


Scenario Outline: Test_Intake_Transfer_Intake_To_Outcome
	When I Change Shelter "Demo Shelter"	
	And I Click Add
	And I Select "Transfer" Intake
	And I Select Partner "k"
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical "9"
	And I Enter Details
		Then User Should See Animal Name
	When I Enter Animal Details To Profile
	And I Realease Animal Holds
	And I Click New Outcome Button
	And I Select "<Outcome>"
	And I Delete Recent Outcome
	And I Delete Recent Intake

	Examples: 
	| Outcome         |	
	| Death           |


@Intake
Scenario: Test_Intake_Transfer_Intake
	When I Change Shelter "Demo Shelter"	
	And I Click Add
	And I Select "Transfer" Intake
	And I Select Partner "k"
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical "10"
	And I Enter Details
		Then User Should See Animal Name