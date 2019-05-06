@Return
Feature: Return
	In order Execute Return Intake To Outcome Functionality

Background: 
	Given I Login


Scenario: Test_Intake_Return_Outcome_Adoption
	When I Change Shelter "Central Missouri Humane Society"
	And I Search "k"
	And I Click New Intake 
	And I Select "Return" Intake
	And I Select Partner "k"
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical
	And I Enter Details
