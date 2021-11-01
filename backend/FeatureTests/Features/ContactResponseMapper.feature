Feature: ContactResponseMapper
	A feature of the MapperService that returns a contact with the correct type assigned to it

Scenario: Map unshared contact
	Given the user has a contact that isn't shared with anyone
	When this contact is mapped to a ContactResponse
	Then the retrieved contact should be of type OTHER

Scenario: Map contact that is shared, but unaccepted
	Given the user has a contact that is shared, but unaccepted
	When this contact is mapped to a ContactResponse
	Then the retrieved contact should be of type SHARED

Scenario: Map contact that is shared and accepted
	Given the user has a contact that is shared and accepted
	When this contact is mapped to a ContactResponse
	Then the retrieved contact should be of type SHARED