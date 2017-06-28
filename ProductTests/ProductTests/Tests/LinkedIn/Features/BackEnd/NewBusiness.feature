Feature:Darta_NewBusiness

@Regression @Darta_TA @BusinessServices
Scenario:Darta/AllDistributions - NewBusiness - I can NewBusiness all products
Given I read data from the DataBase NewBusiness capture table for the requests
And I create AddProposal requests from this data and send to the services
When I run the batch process for 0 days
And I read data from the DataBase NewBusiness verify table where the BatchRunId 0 to verify
Then I verify the values in the database by the test expected values
When I run the batch process for 1 days
And I read data from the DataBase NewBusiness verify table where the BatchRunId 1 to verify
Then I verify the values in the database by the test expected values
When I run the batch process for 6 days
And I read data from the database NewBusiness verify table where the BatchRunId 7 to verify
Then I verify the values in the database by the test expected values
When I run the batch process for 1 days
And I read data from the database NewBusiness verify table where the BatchRunId 8 to verify
Then I verify the values in the database by the test expected values
And I write the test results into the server TestsReports database
And I restore the test database