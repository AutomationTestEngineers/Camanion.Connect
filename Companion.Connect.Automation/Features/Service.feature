@Service @E2E
Feature: Service
	In order Execute Service Intake To Outcome Functionality

Background: 
	Given I Login


Scenario Outline: Test_Intake_Service_Outcome_Adoption
	When I Change Shelter "Prairie Paws Animal Shelter"	
	And I Click Add
	And I Select "Service" Intake
	And I Select Partner "John"
	And I Add Animal
	And I Enter Details "Service"
	And I Add Procedure
	And I Search Animal
	And I Request Medical Exam
	And  I Enter Animal Microchip Details
	And I Enter Animal Rabies Vaccine Details And Realase Holds
	And I Enter Animal Details To Profile
	And I Click Pencil Icon From Result
	And I Click on "<Outcome>" Outcome

	Examples: 
	| Outcome |
	| Service |
