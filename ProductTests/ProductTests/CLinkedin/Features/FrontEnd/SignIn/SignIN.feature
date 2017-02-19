Feature: Linkedin SignIn Tests


@Regression
Scenario Outline: Chrome - I can signin as a user with correct username and password
Given I navigate to the https://www.linkedin.com/ website
And I am on the SignInPage page
And I enter <user_name> as the user_name_entry
And I enter <pass_word> as the pass_word_entry
When I select the sign_in_button element
And I am on the MainPage page 
Then I should see the user_profile_image element
And I should signout

#@source:TestData.xlsx:SignIn
Examples: 
| user_name         | pass_word   |
| kiszols@yahoo.com | Stridentb52 |

@Regression
Scenario Outline: Chrome - I can signin/signout with correct username and password
Given I navigate to the https://www.linkedin.com/ website
And I am on the SignInPage page
And I login with <user_name> username and <pass_word> password
And I should signout

Examples: 
| user_name         | pass_word   |
| kiszols@yahoo.com | Stridentb52 |