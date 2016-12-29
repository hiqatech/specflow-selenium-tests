Feature: ConnectTest

Scenario: ConnectTest
	Given I connect to the GlobalWeatherSoap12 service
	When I request the cities in the Hungary country
	Then The Szeged city is in the Hungary country