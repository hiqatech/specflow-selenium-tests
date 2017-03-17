Feature: IHAveATable

@mytag
Scenario: I have a table to print
Given I have a vertical table
| Key		|	Value		|
| ID		|	01			|
| date		|	17.03.17	|
| numer		|	999			|
| letter	|	a			|
Given I have a horizontal table
| ID		|	Date		|	Number	|	Letter	|
| 01		|	17.03.17	|	999		|	a		|
Given I have a multi table
| Key		|	Value		|Table			|
| ID		|	01			| ID_Table		|
| Date		|	17.03.17	|Date_Table		|
| Numer		|	999			|Number_Table	|
| Letter	|	a			|Letter_Table	|