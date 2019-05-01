@AnimalControl
Feature: AnimalControl
	In order Execute Animal Control Intake To Outcome Functionality

Background: 
	Given I Login


Scenario Outline: Test_Animal_Contol_Death
	When I Change Shelter "Central Missouri Humane Society"
	And I Search "k"
	And I Click New Intake 
	And I Select "Animal Control" Intake
	And I Select Partner "k"
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical
	And I Enter Details
		Then User Should See Animal Name
	When I Enter Animal Details To Profile
	And I Realease Animal Holds
	And I Click New Outcome Button
	And I Select "<Outcome>"
	#And I Delete Recent Outcome
	#And I Delete Recent Intake

	Examples: 
	| Outcome |
	| Death   |
