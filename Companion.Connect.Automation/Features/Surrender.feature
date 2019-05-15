﻿@Surrender @E2E
Feature: Surrender
	In order Execute Surrender Intake To Outcome Functionality

Background: 
	Given I Login


Scenario Outline: Surrender_Intake_To_Outcome
	When I Change Shelter "Demo Shelter"
	And I Click Add
	And I Select "Surrender" Intake
	And I Select Partner "John"
	And I Enter Payment Details
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical "7"
	And I Enter Details
		Then User Should See Animal Name
	When I Enter Animal Details To Profile
	And I Realease Animal Holds
	And I Click New Outcome Button
	And I Select "<Outcome>"
	And I Delete Recent Outcome
	And I Delete Recent Intake

	Examples: 
	| Outcome         |	
	#| Death           |
	| Return to Owner |



@Intake
Scenario: Surrender_Intake
	When I Change Shelter "Demo Shelter"
	And I Click Add
	And I Select "Surrender" Intake
	And I Select Partner "John"
	And I Enter Payment Details
	And I Add Animal
	And I Enter Behavior
	And I Enter Medical "8"
	And I Enter Details
		Then User Should See Animal Name