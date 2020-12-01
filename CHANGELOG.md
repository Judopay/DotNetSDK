# Change Log
All notable changes to this project will be documented in this file.

## 2.2 Changes on 2020-12-01
#### Added
- InitialRecurringPayment, RecurringPaymentType and RelatedReceiptId added to PaymentModel
- Increase Api-Version from 5.6 to 5.7

## Changes on 2020-06-05
#### Added
- PostCodeCheckResult, KountTransactionId, AcquirerTransactionId, ExternalBankResponseCode and BillingAddress to PaymentReceiptModel

## Changes on 2018-08-31
- Fixed register card endpoint mapping

## Changes on 2018-03-02
- Added one time token payment

## Changes on 2018-03-01
- Removed cliend side validation for transactions with 0 amount

## Changes on 2017-04-04

#### Updated
- Added certificate pinning.
- Removed Validate method from each endpoint, analysis of API requests over the last 90 days shows no live usage so no disruption is expected for any customers

## Changes on 2016-01-12

#### Updated
- [BREAKING CHANGE] Paid changed to Success in WebPaymentStatus Enum
- [BREAKING CHANGE] Field YourPaymentReference is now readonly in order to enforce uniqueness rules.

## Changes on 2015-12-09

#### Updated
- [BREAKING CHANGE] in some places ReceiptId was a string, despite validation requiring a long, this is now uniformly a long
- [BREAKING CHANGE] removed UNKNOWN from TransactionType Enum

---
