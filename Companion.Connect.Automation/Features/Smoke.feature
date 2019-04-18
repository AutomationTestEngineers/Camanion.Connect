Feature: Smoke
	In order Execute Basic Functionality


@Smoke
Scenario Outline: Test_001_Login
	Given I Enter "<UserName>,<Password>"
	Then User Should see "<UserName>"
	When I Change Shelter "<ShelterName>"
	And I Search "<Search>"
	Then User Should See Search Reasult "<Search>"
	
Examples: 
| UserName                    | Password  | ShelterName                     | Search |
| inazir@companionprotect.com | Muridke@1 | Central Missouri Humane Society | K      |
	
