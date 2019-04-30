@Transfer
Feature: Transfer
	In order Execute Transfer Intake To Outcome Functionality

Background: 
	Given I Login


Scenario: Test_Intake_Transfer_Outcome_Adoption
	When I Change Shelter "Central Missouri Humane Society"
	And I Search "k"
	And I Click New Intake 
	And I Select "Transfer" Intake
	And I Select Partner "John"
