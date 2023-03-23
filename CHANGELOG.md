# Change Log
All notable changes to this project will be documented in this file.

## 4.0 Changes
- Update Api-Version to 6.19
- Update PaymentModel to remove PartnerServiceFee, ConsumerLocation and DeviceCategory (use
	ThreeDSecureTwoModel.AuthenticationSource instead).   AcceptHeaders and UserAgent moved
	to ThreeDSecureTwoPaymentModel.   RelatedPaymentNetworkTransactionId added.
- Make ClientDetails only accept a Key and Value attribute (encrypted by Mobile SDK using DeviceDNA)
- Change YourPaymentMetaData signature to allow object values to be stored rather than only strings
- Update SaveCardModel to add CardHolderName, remove IssueNumber and StartDate (no longer required for Maestro transactions)
- Remove support for ConsumerToken, use YourConsumerReference instead
- Remove support for OneUseTokens
- Remove support for ThreeDSecure 1.x

## 3.4 Changes on 2022-07-18
- Allow primary account details to be set on ResumeThreeDSecureTwoModel and CompleteThreeDSecureTwoModel for MCC 6012 transactions
- Add RecurringPaymentType (RECURRING/MIT) to receipt responses
- Add PaymentNetworkTransactionId to receipt responses

## 3.3 Changes on 2022-05
- Add State to WebPaymentCardAddress
- Add 3ds2 fields to WebPaymentRequestModel
- Add CheckCard WebPayments
- Enable the SDK to perform a 3DS2 flow going directly to the Challenge step 
- Update Api-Version to 6.15

## 3.2 Changes on 2022-01
- Add ThreeDSecureMpi fields to RegisterCardModel and ThreeDSecureTwoPaymentModel
- Add State to CardAddressModel
- Update Api-Version to 6.9

## 3.1 Changes on 2021-08
- Amount is now optional on voids, collections and refunds.   If not specified, the remaining amount of the original transaction will be used.
- Update Api-Version to 6.6
- Allow WebPaymentReference to be set on PaymentModel
- Add AuthCode to initial receipt response
- Add AuthCode, WebPaymentReference and Acquirer to response of Transactions.Get(receiptId)
- New exemption flags ChallengeRequestIndicator and ScaExemption added to the ThreeDSecureTwo Model
- Add PrimaryAccountDetails to RegisterCardModel and CheckCardModel

## 3.0 Changes on 2021-03-11
- Update default base Urls with certificate pinning checks
- Update Api-Version to 6.2
- Add Address1/Address2/Address3 to CardAddressModel (deprecate Line1/Line2/Line3)
- Update CardPaymentModel, TokenPaymentModel, OneTimePaymentModel, RegisterCardModel, RegisterEncryptedCardModel, CheckCardModel, CheckEncryptedCardModel to expose additional request attribute required for 3DSecure 2.
- Add new PaymentRequiresThreeDSecureTwoModel which will be returned with MethodUrl populated if a device details check is required, or ChallengeUrl populated if a consumer challenge is required.
- Add new Resume3DSecureTwo (to be called after device details check) and Complete3DSecureTwo (to be called after consumer challenge) methods to IThreeDS

## 2.3.0 Changes on 2021-01-11
- PaymentSession added to Credentials
- Add Address1/Address2/Address3 to WebPaymentCardAddress (deprecate Line1/Line2/Line3)
- Remove Country string from WebPaymentCardAddress and add int CountryCode

## 2.2.264 Changes on 2020-12-01
#### Added
- InitialRecurringPayment, RecurringPaymentType and RelatedReceiptId added to PaymentModel
- Increase Api-Version from 5.6 to 5.7
#### Removed
- VisaCheckout APIs

## 2.1.237 Changes on 2020-10-21
- Updated the following models to allow custom values for YourPaymentReference (1 to 50 characters)
	- PaymentModel
	- SaveCardModel
	- CheckCardModel
	- RegisterCardModel

## 2.1.223 Changes on 2020-06-05
#### Added
- PostCodeCheckResult, KountTransactionId, AcquirerTransactionId, ExternalBankResponseCode and BillingAddress to PaymentReceiptModel

## Changes on 2018-08-31
- Fixed register card endpoint mapping

## Changes on 2018-03-02
- Added one time token payment

## Changes on 2018-03-01
- Removed client side validation for transactions with 0 amount

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
