Feature: Darta - NewBusiness

@Smoke
Scenario: BusinessServices - Darta - NewBusinessSimple - NewBusiness - I can request a NewBusiness for all products
Given I connect to the WINDEVAD0376 server RA database
#And I restore the RA database on the WINDEVAD0376 server
And I clear the database FmTransaction table
And I read data from the dataBase capture table for the requests
And I read data from the dataBase verify table where the BatchRunId 3 to verify
And I create AddProposal requests from this data and send to the services
#When I run the batch process for 3 days
Then I verify the values in the database by the test expected database
And I write the test results into the server TestResults database
#When I run the batch process for 5 days
And I read data from the dataBase verify table where the BatchRunId 5 to verify
Then I verify the values in the database by the test expected database
And I write the test results into the server TestResults database


@Smoke
Scenario: BusinessServices - Darta - NewBusinessExcel - NewBusiness - I can request a NewBusiness for all products
And I read data from the Excel capture table for the requests
And I read data from the Excel verify table where the BatchRunId 5 to verify