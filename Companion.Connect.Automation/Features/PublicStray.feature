@PublicStray
Feature: PublicStray
	In order Execute Intake PublicStray To Outcome Functionality

Background: 
	Given I Login


Scenario: Test_Intake_PublicStray_Outcome_Adoption
	When I Change Shelter "Central Missouri Humane Society"
	And I Search "k"
	And I Click New Intake 
	And I Select "Public Stray" Intake
	And I Select Partner "John"
