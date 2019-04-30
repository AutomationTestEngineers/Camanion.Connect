@Surrender
Feature: Surrender
	In order Execute Surrender Intake To Outcome Functionality

Background: 
	Given I Login


Scenario: Test_Intake_Surrender_Outcome_Adoption
	When I Change Shelter "Central Missouri Humane Society"
	And I Search "k"
	And I Click New Intake 
	And I Select "Return" Intake
	And I Select Partner "John"
