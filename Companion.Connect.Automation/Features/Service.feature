@Service
Feature: Service
	In order Execute Service Intake To Outcome Functionality

Background: 
	Given I Login


Scenario: Test_Intake_Service_Outcome_Adoption
	When I Change Shelter "Central Missouri Humane Society"
	And I Search "k"
	And I Click New Intake 
	And I Select "Service" Intake
	And I Select Partner "John"