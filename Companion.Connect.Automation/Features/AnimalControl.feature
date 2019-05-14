@AnimalControl @E2E
Feature: AnimalControl
	In order Execute Animal Control Intake To Outcome Functionality

Background: 
	Given I Login

Scenario Outline: Contol_Intake_To_Outcome
	When I Change Shelter "Demo Shelter"	
	And I Click Add
	And I Select "Animal Control" Intake
	And I Select Partner "k"
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical "1"
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
	#| Return to Owner |
	#| Euthanasia      |
	#| Transfer        |
	#| Return to Owner |
	#| Adoption        |


@Intake
Scenario: Animal_Contol_Intake
	When I Change Shelter "Demo Shelter"	
	And I Click Add
	And I Select "Animal Control" Intake
	And I Select Partner "k"
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical "2"
	And I Enter Details
		Then User Should See Animal Name