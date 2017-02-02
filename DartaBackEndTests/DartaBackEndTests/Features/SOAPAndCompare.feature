Feature: SOAP and Compare

Scenario: SOAP and Compare
	Given I connect to the city_service service
	When I request the cities in the Hungary country
	Then The Szeged city is in the Hungary country
	Given I connect to the city_service service
	When I request the cities in the Hungary country
	Then The Szeged city is in the Hungary country
	#When I request the cities in the Ireland country
	#Then The Dublin city is in the Ireland country
	When I compare the response1.txt and response2.txt response files
	Then The differencial comparation file should be empty