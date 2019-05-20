@Return @E2E
Feature: Return
	In order Execute Return Intake To Outcome Functionality

Background: 
	Given I Login


Scenario Outline: Return_Intake_To_Outcome
	When I Change Shelter "<Shelter>"	
	And I Click Add
	And I Select "Return" Intake
	And I Select Partner "<Person>"
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical "<IntakeSection>"
	And I Enter Details
		Then User Should See Animal Name
	When I Enter Animal Details To Profile
	And I Realease Animal Holds
	And I Click New Outcome Button
	And I Select "<Outcome>"
	And I Delete Recent Outcome
	And I Delete Recent Intake

	Examples: 
	| Outcome         | Shelter                         | Person | IntakeSection|
	| Death           | Demo Shelter                    | k      | 1            |
	| Return to Owner | Demo Shelter                    | k      | 2            |
	| Euthanasia      | Central Missouri Humane Society | k      | 3            |
	| Transfer        | Demo Shelter                    | k      | 4            |
	| Adoption        | Demo Shelter                    | k      | 4            |


@Intake
Scenario: Return_Intake
	When I Change Shelter "Demo Shelter"	
	And I Click Add
	And I Select "Return" Intake
	And I Select Partner "k"
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical "5"
	And I Enter Details
		Then User Should See Animal Name