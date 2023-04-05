﻿using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// Provides the ability to reserve funds on your consumers cards. Those funds can later 
    /// be captured using a collection transaction.
    /// </summary>
    // ReSharper disable UnusedMember.Global
    public interface IPreAuths
    {
        /// <summary>
        /// Creates the specified card pre authorization.
        /// </summary>
        /// <param name="cardPreAuth">The card pre authorization.</param>
        /// <returns>The receipt for the created card pre authorization</returns>
        Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPreAuth);

        /// <summary>
        /// Creates the specified token pre authorization.
        /// </summary>
        /// <param name="tokenPreAuth">The token pre authorization.</param>
        /// <returns>The receipt for the created token pre authorization</returns>
        Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPreAuth);

        /// <summary>
        /// Creates the specified apple Pay pre authorization.
        /// </summary>
        /// <param name="pkPreAuth">The apple pay pre authorization.</param>
        /// <returns>The receipt for the created apple pre authorization</returns>
        Task<IResult<ITransactionResult>> Create(ApplePayPaymentModel pkPreAuth);

        /// <summary>
        /// Creates the specified Google Pay pre authorization.
        /// </summary>
        /// <param name="googlePayPayment">The Google Pay pre authorization.</param>
        /// <returns>The transaction result for the created Google Pay pre authorization</returns>
        Task<IResult<ITransactionResult>> Create(GooglePayPaymentModel googlePayPayment);
    }
    // ReSharper restore UnusedMember.Global
}