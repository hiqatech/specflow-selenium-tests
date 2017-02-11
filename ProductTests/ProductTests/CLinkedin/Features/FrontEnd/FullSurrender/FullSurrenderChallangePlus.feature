
Feature: FullSurrender

@Regression
Scenario Outline: Chrome - I can run a FullSurrender transaction
Given I navigate to the https://10.252.50.72/DartaPI/Base/Views/Pages/Landing.aspx website
And I am on the LogInPage page
And I select the Allianz_Bank element
And I login with johndoe username and Password1 password
And I am on the MainPage page
And I select the data_base_dropdown element
And I select the data_base_R1_selection element
And I connect to the SERV8604 server DartaUATR1 database
And I select random way PolicyNumber where PolicyStatus='I' from the BP_BasicPolicy table
And I enter query_result as the search_policy_entry
And I select the policy_search_submit_button element
And I select the policy_search_submit_result element
And I am on the PolicyPage page
And I select the policy_encashments_button element
And I select the full_surrender_button element
And I enter system_date-2 as the application_signed_date_entry
And I enter system_date+1 as the application_received_date_entry
And I select the full_surrender_submit_button element
And I should see the sucess_full_surrender_completed_message element
When I run the batch process for 5 days
Then The BP_BasicPolicy table PolicyStatus should be <policy_status>
And The PH_PaymentHistory table Details should be <details>
#And The IH_InvestmentHistory table InvestmentFund should be x
#And The IH_InvestmentHistory table EntrySource should be x
#And The IH_InvestmentHistory table AttributionRunDate should be x
#And The IH_InvestmentHistory table Amount  should be x
#And The NP_NewBusinessProposal table SignatureDate  should be x
#And The NP_NewBusinessProposal table PaymentValueDate  should be x
#And The NP_NewBusinessProposal table ProposalDate  should be x
#And The PE_PolicyElement table PaymentDate  should be x
#And The PE_PolicyElement table ProposalCompDate  should be x

#Then I should logout

#@source:TestData.xlsx:FullSurrender
Examples: 
| policy_number | policy_status | details | InvestmentFund | EntrySource | AttributionRunDate | Amount |
| x             | I             | FORCE   | x              | x           | x                  | x      |			
