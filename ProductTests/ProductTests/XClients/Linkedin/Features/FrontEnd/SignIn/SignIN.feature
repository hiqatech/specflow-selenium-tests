Feature: Linkedin SignIn Tests

@Regression
Scenario Outline: I can signin as a user with correct username and password
Given I start the WebDriver with <driver> browser
And I navigate to the https://www.linkedin.com/ website
And I am on the SignInPage page
And I enter <user_name> as the user_name_entry
And I enter <pass_word> as the pass_word_entry
When I select the sign_in_button element
And I am on the MainPage page 
Then I should see the user_profile_image element
And I select the user_menu_dropdown element
And I select the user_menu_sign_out_button element
And I am on the SignOutPage page
And I should see the user_name_entry element

#@source:TestData.xlsx:SignIn
Examples: 
| driver | user_name			| pass_word		|
| Chrome | kiszols@yahoo.com	| Stridentb52	|

@Regression
Scenario Outline: I can signin/signout with correct username and password
Given I start the WebDriver with <driver> browser
Given I navigate to the https://www.linkedin.com/ website
And I am on the SignInPage page
And I login with <user_name> username and <pass_word> password
And I should signout

Examples: 
| driver | user_name			| pass_word		|
| Chrome | kiszols@yahoo.com	| Stridentb52	|