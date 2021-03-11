# Judo .NET SDK
[![Build status](https://ci.appveyor.com/api/projects/status/63dbbef4dxrual5f/branch/master?svg=true)](https://ci.appveyor.com/project/JudoPayments/dotnetsdk/branch/master)

The .NET SDK is a client for our Judopay API, which provides card payment processing for mobile apps and websites.

## Requirements

#### NB: Due to industry requirements only TLS 1.2 is supported

## Getting started
The Judopay SDK is distributed as a [NuGet package](https://www.nuget.org/packages/JudoPay.Net/)
using the package name of JudoPay.Net.

#### 1. Integration
You can install the SDK directly from within Visual Studio either using the NuGet package manager UI, or in the Package Manager Console:

```powershell
Install-Package JudoPay.Net
```

#### 2. Setup

You configure you Judopay API client when invoking the JudoPaymentsFactory.Create method. This has
three parameters; environment (Sandbox for development and testing, and Live for production), and api
token and secret. You set you API token and secret up through our [management dashboard](https://portal.judopay.com)
after creating an account. You can create a testing account from [Apply for a Sandbox Account](https://www.judopay.com/apply-sandbox-account)

```c#
var client = JudoPaymentsFactory.Create(JudoPayDotNet.Enums.JudoEnvironment.Sandbox, "<TOKEN>", "<SECRET>");
```

#### 3. Make a payment
Once you have your API client, you can easily process a payment:

```c#
var cardPaymentModel = new CardPaymentModel
{
	JudoId = "<JUDO_ID>",

	// value of the payment
	Amount = 1.01m,
	Currency = "GBP",

	// card details
	CardNumber = "4976000000000036",
	ExpiryDate = "12/25",
	CV2 = "452",

	// a unique identifier for your customer
	YourConsumerReference = "MyCustomer004",
};
```
**Note:** Please make sure that you are using a unique Consumer Reference for each different consumer.

#### 4. Check the payment result
```c#
client.Payments.Create(cardPaymentModel).ContinueWith(result =>
{
	var paymentResult = result.Result;

	if (!paymentResult.HasError && paymentResult.Response.Result == "Success")
	{
		Console.WriteLine($"Payment successful. Transaction Receipt ID {paymentResult.Response.ReceiptId}");
	}

});
```
## Making a payment with 3DSecure Two

Confirm with Judopay support that the ApiToken you are using is enabled for 3DS2

Some additional attributes are needed on the CardPaymentModel

```c#
var client = JudoPaymentsFactory.Create(JudoPayDotNet.Enums.JudoEnvironment.Sandbox, "<TOKEN>", "<SECRET>");
var paymentModel = new CardPaymentModel
{
    JudoId = "<JUDO_ID>",
    Amount = 1.01m,
    Currency = "GBP",
    CardNumber = "4976000000000036",
    ExpiryDate = "12/25",
    CV2 = "452",

    YourConsumerReference = "d6e6f5b8-e8f4-480e-8787-2a2a47239434",
    YourPaymentReference = "c01b86f8-7b53-410e-91db-7c8f7c116d34",
    CardHolderName = "John Doe",
    EmailAddress = "example@judopay.com",
    MobileNumber = "07999999999",
    PhoneCountryCode = "44",
    ThreeDSecure= new ThreeDSecureTwoModel
    {
        AuthenticationSource = ThreeDSecureTwoAuthenticationSource.Browser,
        MethodNotificationUrl = "https://your.method.notification.url",
        ChallengeNotificationUrl = "https://your.challenge.notification.url"
    }
};

long receiptId = 0l;
await client.Payments.Create(paymentModel).ContinueWith(response =>
{
    if (response.Result.HasError)
    {
        // Error handling
    }
    else
    {
        switch (response.Result)
        {
            case PaymentRequiresThreeDSecureTwoModel threeDSecureRequired
                when threeDSecureRequired.MethodUrl != null:
            {
                // A device details check is required - make a call to MethodUrl and 
                // you will receive a POST to the supplied MethodNotificationUrl once 
                // this is acknowledged, then follow up with a call to
                // client.ThreeDs.Resume3DSecureTwo

                // threeDSecureRequired.result and message give context
                // result: "Additional device data is needed for 3D Secure 2.0"
                // message: "Issuer ACS has requested additional device data gathering"
                break;
            }
            case PaymentRequiresThreeDSecureTwoModel threeDSecureRequired
                when threeDSecureRequired.ChallengeUrl != null:
            {
                var cReq = threeDSecureRequired.CReq;
                // A consumer challenge is required - direct the consumer through a
                // POST call to ChallengeUrl with creq value in the body, and you
                // will receive a POST to the supplied ChallengeNotificationUrl once 
                // this is acknowledged, then follow up with a call to
                // client.ThreeDs.Complete3DSecureTwo

                // threeDSecureRequired.result and message give context
                // result: "Challenge completion is needed for 3D Secure 2.0"
                // message: "Issuer ACS has responded with a Challenge URL"
                break;
            }
            case PaymentReceiptModel paymentReceipt:
            {
                receiptId = paymentReceipt.ReceiptId;
                // No additional authentication required - check receipt details
                if (paymentReceipt.Result == "Success")
                {
                    // Transaction was successful
                }
                else
                {
                    // Transaction not completed
                }
                break;
            }
        }
    }
});
```

Resume authentication after a device details check
```c#
// CV2 required again as we do not store this information
var resumeModel = new ResumeThreeDSecureTwoModel
{
    CV2 = "452",
    MethodCompletion = MethodCompletion.Yes
};

await client.ThreeDs.Resume3DSecureTwo(receiptId, resumeModel).ContinueWith(resumeResponse =>
{
    if (resumeResponse.Result.HasError)
    {
        // Error handling
    }
    else
    {
        switch (resumeResponse.Result)
        {
            case PaymentRequiresThreeDSecureTwoModel threeDSecureRequired
                when threeDSecureRequired.ChallengeUrl != null:
            {
                var cReq = threeDSecureRequired.CReq;
                // A consumer challenge is required - direct the consumer through a
                // POST call to ChallengeUrl with creq value in the body, and you
                // will receive a POST to the supplied ChallengeNotificationUrl once 
                // this is acknowledged, then follow up with a call to
                // client.ThreeDs.Complete3DSecureTwo
                
                // threeDSecureRequired.result and message give context
                // result: "Challenge completion is needed for 3D Secure 2.0"
                // message: "Issuer ACS has responded with a Challenge URL"
                break;
            }
            case PaymentReceiptModel paymentReceipt:
            {
                receiptId = paymentReceipt.ReceiptId;
                // No additional authentication required - check receipt details
                if (paymentReceipt.Result == "Success")
                {
                    // Transaction was successful
                }
                else
                {
                    // Transaction not completed
                }

                break;
            }
        }
    }
});
```

Complete transaction after a challenge authentication
```c#
// CV2 required again as we do not store this information
var completeModel = new CompleteThreeDSecureTwoModel()
{
    CV2 = "452"
};

await client.ThreeDs.Complete3DSecureTwo(receiptId, completeModel).ContinueWith(completeResponse =>
{
    if (completeResponse.Result.HasError)
    {
        // Error handling
    }
    else
    {
        if (completeResponse.Result is PaymentReceiptModel paymentReceipt)
        {
            receiptId = paymentReceipt.ReceiptId;
            // No additional authentication required - check receipt details
            if (paymentReceipt.Result == "Success")
            {
                // Transaction was successful
            }
            else
            {
                // Transaction not completed
            }
        }
    }
});

```

## Next steps
The Judo .NET library supports additional features and a range of customization options. For more information about this SDK see our wiki documentation as well as our public documentation.


