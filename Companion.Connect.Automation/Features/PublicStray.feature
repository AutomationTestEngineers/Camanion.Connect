@PublicStray
Feature: PublicStray
	In order Execute Intake PublicStray To Outcome Functionality

Background: 
	Given I Login


Scenario Outline: Test_Intake_PublicStray_Outcome_Adoption
	When I Change Shelter "Demo Shelter"	
	And I Click Add
	And I Select "Public Stray" Intake
	And I Select Partner "John"
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical
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
