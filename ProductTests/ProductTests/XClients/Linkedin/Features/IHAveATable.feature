Feature: IHAveATable

@mytag
Scenario: I have a vertical table to verify
Given I have a vertical table
| Key		|	Value		|
| ID		|	01			|
| date		|	17.03.17	|
| numer		|	999			|
| letter	|	a			|

Scenario: I have a horizontal table to verify
Given I have a horizontal table
| ID		|	Date		|	Number	|	Letter	|
| 01		|	17.03.17	|	999		|	a		|

Scenario: The 1st policy database entries should be
Given The policy 0 database entries should be
| Key		|	Value		|Table			|
| ID		|	01			| ID_Table		|
| Date		|	17.03.17	|Date_Table		|
| Numer		|	999			|Number_Table	|
| Letter	|	a			|Letter_Table	|

Scenario: The 2nd policy database entries should be
Given The policy 1 database entries should be
| ID		|	Date		|	Number		|	Letter		|
| 01		|	17.03.17	|	999			|	a			|
| ID_Table	|	Date_Table	|Number_Table	|Letter_Table	|