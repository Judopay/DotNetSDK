using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Models;

namespace JudoPayDotNetTests
{
    public class UseTest
    {
        public void Semantic()
        {
            JudoPayments judoPay = new JudoPayments("MYTOKENISHERE",
                                                    "ThisIsASecret",
                                                    "api.test.judopayments");

            var card_model = new
            {

            };

            // transactions/payments
            judoPay.Payments.Create(new CardPaymentModel());
            judoPay.Payments.PayWithToken();

            // Transactions/preauths 
            judoPay.PreAuths.PayWithCard();
            judoPay.PreAuths.PayWithToken();

            judoPay.PreAuths.Collected();

            judoPay.Refunds.Refund();

            judoPay.Collections.Collect(new { receiptid = "12345", amount = 53.25m });


            judoPay.Refunds.Refund();

            judoPay.Transactions.GetTransactions();
        }
    }
}
