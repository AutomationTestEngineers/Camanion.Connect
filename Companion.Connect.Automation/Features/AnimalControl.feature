@AnimalControl @E2E
Feature: AnimalControl
	In order Execute Animal Control Intake To Outcome Functionality

Background: 
	Given I Login

Scenario Outline: Animal_Contol_Intake_To_Outcome
	When I Change Shelter "<Shelter>"	
	And I Click Add
	And I Select "Animal Control" Intake
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
		Then "Update data successful" Message Should Be Display

	Examples: 
	| Outcome         | Shelter                         | Person | IntakeSection |
	| Death           | Demo Shelter                    | kc     | 1             |
	| Return to Owner | Demo Shelter                    | kc     | 2             |
	| Euthanasia      | Central Missouri Humane Society | kc     | 3             |
	| Transfer        | Demo Shelter                    | kc     | 4             |
	| Adoption        | Demo Shelter                    | kc     | 5             |


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
	When I Delete Recent Intake
		Then "Update data successful" Message Should Be Display

#@Delete
#Scenario: Delete Animal
	#When I Change Shelter "Demo Shelter"
	#And I Delete Recent Outcome "Animal_"
	#And I Delete Recent Intake "Animal_"