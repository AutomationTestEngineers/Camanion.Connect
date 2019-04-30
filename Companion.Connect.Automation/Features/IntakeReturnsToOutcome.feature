Feature: IntakeReturnsToOutcome
	In order Execute Intake Return Functionality

@IntakeReturnsToOutcome
Scenario: [Test_001] Intake_Return_Outcome_Adoption
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen
