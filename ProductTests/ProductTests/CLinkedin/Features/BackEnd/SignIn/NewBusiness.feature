Feature: BackEndNewBusiness

@Smoke
Scenario: Server - I can create a Allianz Challange Plus New Business
Given I connect to the SERV8604 server DartaUATR1 database  
And I ping the DartaBusinessServices with
| Field					| Value |
| database				| R1     |
| distribution			| 01     |
| username				| x     |
| locale				| IT     |
| agentid				| x     |
| company				| DAR     |
#| servicingagentid		| x	|
| timeout				| x		|
| userclientreference	| x	x	|
And I have a NewBusinessProposal to AddProposal
|	Field					|	Value			|
|	ApplicationSignedDate	|	system_date-5	|
|	Address					|	Dublin			|
And I send POST request to AddProposal
When I run the batch process
Then The DataBase values shoudl be
|	Table			|	Entry		|	Value		|
|BP_BasicPolicy 	|	PolicyStatus 	|	I		|
|PH_PaymentHistory	|	Details		|	IN FORCE	|