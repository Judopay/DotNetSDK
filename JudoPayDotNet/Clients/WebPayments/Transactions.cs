﻿using System;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    // ReSharper disable UnusedMember.Global
	/// <summary>
	/// This entity allows you to fetch details of an individual webpayment (either by receipt id or reference)
	/// </summary>
    internal class Transactions : JudoPayClient, ITransactions
    {
        private const string Baseaddress = "webpayments";
        private const string Baseaddressgetbyreceipt = "transactions";

        public Transactions(ILog logger, IClient client) : base(logger, client)
        {
        }

		/// <summary>
		/// Gets a webpayment transaction by it's reference.
		/// </summary>
		/// <param name="reference">The reference.</param>
		/// <returns>The webpayment transaction</returns>
		public Task<IResult<GetWebPaymentResponseModel>> Get(string reference)
        {
            var address = $"{Baseaddress}/{reference}";

            return GetInternal<GetWebPaymentResponseModel>(address);
        }

        /// <summary>
        /// Gets a webpayment transaction by it's reference filtering by type
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="type">The type of transaction <see cref="TransactionType"/></param>
        /// <returns>The webpayment transaction</returns>
        public Task<IResult<GetWebPaymentResponseModel>> Get(string reference, TransactionType type)

        {
            if (type == TransactionType.PREAUTH || type == TransactionType.PAYMENT)
            {
                var address = $"{Baseaddress}/{type}/{reference}";

                return GetInternal<GetWebPaymentResponseModel>(address);
            }

            //Transactions can only be fetched by transation types PAYMENTS or PREAUTH
            return Task.FromResult<IResult<GetWebPaymentResponseModel>>(new Result<GetWebPaymentResponseModel>(null,
                new ModelError()
                {
                    Message = "Sorry, it looks like you're trying to make a collection on an invalid transaction type. Collections can only be performed on PreAuths.",
                    Code = 43//JudoApiError.General_Error TODO make enum to represent these code numbers
                }));
        }

		/// <summary>
		/// Gets a webpayment transaction by transaction identifier (ReceiptId).
		/// </summary>
		/// <param name="receiptId">The transaction identifier.</param>
		/// <exception cref="ArgumentNullException">If receiptId is empty</exception>
		/// <returns>The webpayment transaction</returns>
		public Task<IResult<GetWebPaymentResponseModel>> GetByReceipt(string receiptId)
        {
            if (string.IsNullOrWhiteSpace(receiptId))
                throw new ArgumentNullException(nameof(receiptId));

            var address = $"{Baseaddressgetbyreceipt}/{receiptId}/webpayment";

            return GetInternal<GetWebPaymentResponseModel>(address);
        }
    }
    // ReSharper restore UnusedMember.Global
}
