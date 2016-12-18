Feature: LinkedIn LogIn Tests

@Smoke
@Browser:Chrome
Scenario:Chrome -  I can login as a user with my correct username and password
Given I navigate to the https://www.linkedin.com website
And I enter kiszols@yahoo.com as the user_name
And I enter Stridentb52 as the pass_word
When I select the sign_in_button element
Then I should see the user_profile element
#And I should logout

@Smoke
@Browser:Chrome
Scenario Outline:Chrome -  I can not login as user with not correct username and password
Given I navigate to the https://www.linkedin.com website
And I enter <user_name> as the user_name
And I enter <pass_word> as the pass_word
When I select the sign_in_button element
Then I should not see the user_profile element

@source:TestData.xlsx:LogIn
Examples: 
|	user_name			|	pass_word			|