Feature:Darta_Allianz_UserAdmin

@Regression @Darta_UAT @WebPI
Scenario Outline:Darta/Allianz - NewUser - I can create new user on PI
Given I start the WebDriver on the <browser> browser


Examples: 
|browser	|	user_name		|
|Chrome		| 	TestUser		|