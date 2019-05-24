@PublicStray @E2E
Feature: PublicStray
	In order Execute Intake PublicStray To Outcome Functionality

Background: 
	Given I Login


Scenario Outline: PublicStray_Intake_To_Outcome
	When I Change Shelter "<Shelter>"	
	And I Click Add
	And I Select "Public Stray" Intake
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
	| Death           | Demo Shelter                    | John   | 1             |
	| Return to Owner | Demo Shelter                    | John   | 2             |
	| Euthanasia      | Central Missouri Humane Society | John   | 3             |
	| Transfer        | Demo Shelter                    | John   | 4             |
	| Adoption        | Demo Shelter                    | John   | 2             |


@Intake
Scenario: PublicStray_Intake
	When I Change Shelter "Demo Shelter"	
	And I Click Add
	And I Select "Public Stray" Intake
	And I Select Partner "John"
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical "4"
	And I Enter Details
		Then User Should See Animal Name
	When I Delete Recent Intake
		Then "Update data successful" Message Should Be Display
