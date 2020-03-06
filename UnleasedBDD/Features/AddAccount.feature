Feature: AddAccount
 As a Xero User
 In order to manage my business successfully,
 I want to be able to add an "ANZ (NZ)" bank inside my Xero Organisation

Scenario: Add an account to a Xero account
	Given I have logged in to xero prod<AccountName>
    And I input my bank account number<Bank>,<BankAccountName>,<AccountNumber>,<Type>
	Then the <BankAccountName> should get added successfully
	Examples: 
	| Bank     | AccountName  | AccountNumber  | Type                  | BankAccountName |
	| ANZ (NZ) | Rajesh Tech | 0301044678780000| Everyday (day-to-day) | Test4            |

