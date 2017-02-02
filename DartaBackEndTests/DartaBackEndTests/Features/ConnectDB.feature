Feature: ConnectDB

@Smoke
Scenario: ConnectDB
	Given I connect to the SERV8604 server RegressionTests database
	And I run the SELECT TOP 1 * FROM [RegressionTests].[dbo].[CE_Client] WHERE ClientReference = 3958176 query
	And I update the DB data with the UPDATE [RegressionTests].[dbo].[CE_Client] SET ClientName2 = 'MS SCOTTISHL'  WHERE ClientReference = 3958176 query
	And I run the SELECT TOP 1 * FROM [RegressionTests].[dbo].[CE_Client] WHERE ClientReference = 3958176 query
	When I compare the answer1.xml and answer2.xml query result files
	Then The differencial comparation file by the query results should be empty
	#And I restore the RegressionTests database on the SERV8604 server