# JudoPay SDK

[![Build status](https://ci.appveyor.com/api/projects/status/y9mrqtjr0cf1g5li?svg=true)](https://ci.appveyor.com/project/JudoPayments/dotnetsdk) <a href="https://scan.coverity.com/projects/judopaydotnetsdk">
  <img alt="Coverity Scan Build Status"
       src="https://img.shields.io/coverity/scan/6752.svg"/>
</a>

The JudoPay SDK is a client for our JudoPay API, which provides card payment processing 
for mobile apps and websites.

## Installation
The JudoPay SDK is distributed as a [NuGet package](https://www.nuget.org/packages/JudoPay.Net/) 
using the package name of JudoPay.Net. You can install the SDK directly from within Visual Studio
either using the NuGet package manager UI, or in the Package Manager Console:

```powershell
Install-Package JudoPay.Net
```

## Configuration

You configure you JudoPay API client when invoking the JudoPaymentsFactory.Create method. This has
three parameters; environment (Sandbox for development and testing, and Live for production), and api
token and secret. You set you API token and secret up through our [management dashboard](https://portal.judopay.com)
after creating an account. You can create a testing account by clicking "Getting Started" in our [documentation](https://www.judopay.com/docs)

```c#
var client = JudoPaymentsFactory.Create(JudoPayDotNet.Enums.JudoEnvironment.Sandbox, "YOUR_API_TOKEN", "YOUR_API_SECRET");
```

## Usage - Process a payment
Once you have your API client, you can easily process a payment:

```c#
var cardPaymentModel = new CardPaymentModel
{
	//the value of the payment
	Amount = 1.01m,
	Currency = "GBP",

	// the card details
	CardNumber = "4976000000000036",
	ExpiryDate = "1215",
	CV2 = "452",

	// identify the recipient
	JudoId = "500017",

	// provide an identifier for your customer
	YourConsumerReference = "MyCustomer004",

	// provide an identifier for this payment
	YourPaymentReference = "Payment523515",
};

client.Payments.Create(cardPaymentModel).ContinueWith(result =>
{
	var paymentResult = result.Result;

	if (paymentResult.Response.Result == "Success")
	{
		Console.WriteLine("Payment successful. Transaction Reference {0}", paymentResult.Response.ReceiptId);
	}

});
```

## Usage - List transactions

You also have access to a complete feed of all transactions within your account:

```c#
client.Transactions.Get().ContinueWith(result =>
{

	if (!result.Result.HasError)
	{
		foreach (var tx in result.Result.Response.Results)
		{
			Console.WriteLine("{0} {1} {2}", tx.ReceiptId, tx.Type, tx.Amount);
		}
	}
	else
	{
		Console.WriteLine("Call returned error. {0}", result.Result.Error.ErrorMessage);
	}
});
```

