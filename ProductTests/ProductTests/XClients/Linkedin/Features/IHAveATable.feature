Feature: IHAveATable

@mytag
Scenario: I have a table to print
Given I have a vertical table
| Key		|	Value		|
| ID		|	01			|
| date		|	date		|
| numer		|	999			|
| letter	|	a			|
Given I have a horizontal table
| ID		|	Date		|	Number	|	Letter	|
| 01		|	date		|	999		|	a		|
Given I have a multi table
| Key		|	Value		|Table			|
| ID		|	01			| ID_Table		|
| date		|	date		|Date_Table		|
| numer		|	999			|Number_Table	|
| letter	|	a			|Letter_Table	|