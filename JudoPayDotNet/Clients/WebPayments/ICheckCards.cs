﻿using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    /// <summary>
    /// Allows you to create a webpayment before passing Judo your customer to complete the payment.
    /// </summary>
    /// <remarks>This is the CheckCard variation of a webpayment, to check if a card is valid.</remarks>
    public interface ICheckCards
    {
        /// <summary>
        /// Creates the webpayment preauth.
        /// </summary>
        /// <param name="model">The webpayment preauth.</param>
        /// <returns>The information required to finalize the webpayment preauth</returns>
        Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model);

        /// <summary>
        /// Cancels the specific payment session
        /// </summary>
        /// <param name="model">The Judopay reference for the payment session returned on creation</param>
        /// <returns>The webpayment payment updated</returns>
        Task<IResult<CancelWebPaymentResponseModel>> Cancel(string reference);
    }
}