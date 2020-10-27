using System;
using System.Threading;
using JudoPayDotNet;
using JudoPayDotNet.Models;

namespace JudoPayDotNetSampleApp
{
    class Program
    {
        private static string ApiToken = "Izx9omsBR15LatAl";
        private static readonly string ApiSecret = "b5787124845533d8e68d12a586fa3713871b876b528600ebfdc037afec880cd6";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting CardPayment call");

            var client = JudoPaymentsFactory.Create(JudoPayDotNet.Enums.JudoEnvironment.Sandbox, ApiToken, ApiSecret);
            var cardPaymentModel = new CardPaymentModel
            {
                JudoId = "100915867",

                // value of the payment
                Amount = 1.01m,
                Currency = "GBP",

                // card details
                CardNumber = "4976000000003436",
                ExpiryDate = "1220",
                CV2 = "452",

                // an identifier for your customer
                YourConsumerReference = "MyCustomer004",
            };

            client.Payments.Create(cardPaymentModel).ContinueWith(result =>
            {
                var paymentResult = result.Result;

                if (!paymentResult.HasError && paymentResult.Response.Result == "Success")
                {
                    Console.WriteLine($"Payment successful. Transaction Reference {paymentResult.Response.ReceiptId}");
                }
                else
                {
                    Console.WriteLine("Payment failed");
                }
                Console.WriteLine("Finished!");

            });

            Thread.Sleep(5000);
        }

    }
}
