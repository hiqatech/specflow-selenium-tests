Feature: New Business

#@Regression
Scenario Outline: Chrome - I can create a Challange Plus New Business
Given I navigate to the https://10.252.50.72/DartaPI/Base/Views/Pages/Landing.aspx website
And I am on the LogInPage page
And I select the Allianz_Bank element
And I login with johndoe username and Password1 password
And I am on the MainPage page
And I select the data_base_dropdown element
And I select the data_base_R6_selection element
And I connect to the SERV8604 server DartaUATR1 database
And I select the add_new_poliy_button element
And I am on the NewPolicyPage page
And I select the product_name_dropdown element
And I select the challange_plus_product_selection element
And I enter system_date-19 as the signature_date_entry
And I generate random 10 char long number starts with 01
And I enter random_number as the proposal_number_entry
#And I enter <proposal_number> as the proposal_number_entry
And I enter BENASSI as the agent_search_entry
And I select the agent_search_submit_button element
And I select the select_1st_agent_button element
And I select the confirm_product_and_agent_button element
And I select the client_search_button element
And I enter <client_name> as the client_search_entry
And I select the client_search_submit_button element
And I select the select_1st_client_button element
And I select the is_life_assured_checkbox element
And I select the confirm_client_details_button element
And I select the confirm_client_FATCA_button element
And I select the confirm_client_CRS_button element
And I select the clean_bills_of_health_checkbox element
And I select the confirm_client_Underwriting_button element
And I enter ID001 as the document_number_entry
And I enter GOV as the issuing_authority_entry
And I enter system_date-24 as the issue_date_entry
And I select the document_type_dropdown element
And I select the document_type_1st_selection element
And I select the confirm_client_Money_Laundering_button element
And I select the confirm_policy_relationships_button element
And I select the add_new_beneficary_button element
And I select the policyholder_selection element
And I select the add_beneficary_button element
And I select the confirm_beneficaries_button element
And I enter <bank_check_sum> as the bank_check_sum_entry
And I enter <bank_short_code> as the bank_short_code_entry
And I enter <bank_account_number> as the bank_account_number_entry
And I enter <bank_account_name> as the bank_account_name_entry
And I select the add_premium_details_button element
And I select the add_single_premium_button element
And I enter <premium_amount> as the premium_amount_entry
And I select the add_funds_button element
And I select the add_funds_1st_selection element
And I select the add_funds_update_button element
And I select the confirm_add_funds_button element
And I select the confirm_premium_fund_payment_button element
And I select the accept_alert_message element
And I enter system_date-24 as the premium_received_date_entry
And I enter system_date+24 as the proposal_complete_date_entry
When I select the submit_proposal_confirm_button element
Then I should see the success_proposal_completed_message element

#Then I should logout

Examples: 
| client_name | premium_amount	| bank_check_sum | bank_short_code | bank_account_number | bank_account_name        |
| ALEXANDER	  | 10000			| IT80P			 | 0358901600      | 010570418340        | SUPERCHANNEL - ARGOS SPA |
#| ALEXANDER   | 25000			| IT80P	   	     | 0358901600      | 010570418340        | SUPERCHANNEL - ARGOS SPA |
#| ALEXANDER   | 50000			| IT80P          | 0358901600      | 010570418340        | SUPERCHANNEL - ARGOS SPA |

