using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    // ReSharper disable InconsistentNaming
    public enum PaymentErrorCodesPublic : long
    {
        [LocalizedDescription("Internal Server Error")]
        [Explanation("We've had an internal issue while processing your request.")]
        Unknown = 0,

        /// <summary>
        /// generic can't accept payment
        /// </summary>
        [LocalizedDescription("You payment has been declined")]
        [Explanation("The payment has been declined by the card's issuing bank.")]
        PaymentDeclined = 1001,

        /// <summary>
        /// account location unknown 
        /// </summary>
        [LocalizedDescription("Unknown Judo ID")]
        [Explanation("We can't find an account with that judo ID")]
        AccountLocationNotFound = 1002,

        [LocalizedDescription("Unable to accept payments to this account; please contact customer services.")]
        [Explanation("We can't process payments to the account identified by the judo ID.")]
        UnableToAccept = 1003,

        [LocalizedDescription("The referenced PreAuth transaction has expired.")]
        [Explanation("It's been too long since the referenced PreAuth transaction has been processed. PreAuth transactions expire after 3 days.")]
        PreAuthExpired = 1004,

        [LocalizedDescription("Our server encounter a problem when processing your transaction.")]
        [Explanation("We've had an internal issue while processing your request.")]
        ServerError = 1500,

        [LocalizedDescription("Unable to find the referenced transaction")]
        [Explanation("We can't find a transaction with the receipt ID you've supplied")]
        ReferencedTransactionNotFound = 1600,

        [LocalizedDescription("Unable to process collection as total amount collected would exceed value of original PreAuth transaction.")]
        [Explanation("You can perform as many collections as you like up to the value of the original preAuth transaction.")]
        Collection_Exceeds_PreAuth = 1700,

        [LocalizedDescription("Referenced transaction cannot be refunded as it's not a payment or collection.")]
        Refund_Transaction_Not_Valid = 1800,

        [LocalizedDescription("Unable to process refund as total amount refunded would exceed value of original transaction.")]
        Refund_Exceeds_Original_Transaction = 1900,

        [LocalizedDescription("Referenced transaction cannot be collected as it's not a PreAuth transaction.")]
        Collection_Original_Transaction_Wrong_Type = 2000,

        [LocalizedDescription("Referenced transaction cannot be refunded as it's not a Sale or Collection transaction.")]
        Refund_Original_Transaction_Wrong_Type = 2001,

        [LocalizedDescription("Duplicate payment reference, please use a unique reference for each payment.")]
        Require_Unique_Payment_Reference = 3000,

        [LocalizedDescription("The supplied card token, doesn't match the consumer reference.")]
        [Explanation("For security reasons we need you to supplied both the consumer token and the card token when making a token payment")]
        CardTokenDoesntMatchConsumer = 4000,

        [LocalizedDescription("Please check the card token.")]
        [Explanation("No saved card was found for the card token provided")]
        CardTokenInvalid = 4001,

        [LocalizedDescription("Unauthorized")]
        Unauthorized = 401,

        [LocalizedDescription("Access Forbidden")]
        AccessForbidden = 403,

        [LocalizedDescription("Internal Server Error while authenticating")]
        InternalErrorWhileAuthenticating = 5002,

        [LocalizedDescription("Test card not allowed in live environment!")]
        [Explanation("Please use a real card, or point at the sandbox environment")]
        TestCardNotAllowed = 5001,

        [LocalizedDescription("You cannot process marketplace payments directly, you must use the access token")]
        [Explanation("When using the judoPay Marketplace API, you need to process payments using the Access Token for the seller's account")]
        MustProcessPaymentByToken = 6001,

        [LocalizedDescription("You cannot process marketplace preAuths directly, you must use the access token")]
        [Explanation("When using the judoPay Marketplace API, you need to process preAuths using the Access Token for the seller's account")]
        MustProcessPreAuthByToken = 6002,

        [LocalizedDescription("The transaction identified is not a type that supports 3D secure authorization")]
        [Explanation("Only payments and preauths support 3D secure authorization")]
        TransactionTypeDoesNotNeedThreeDSecure = 7001,

        [LocalizedDescription("We have already processed the 3D secure authorization for this transaction")]
        [Explanation("You may have already submitted 3D secure authorization for this transaction")]
        TransactionAlreadyAuthorizedByThreeDSecure = 7002,


        [LocalizedDescription("This transaction does not require 3D secure authorization. Please check you're sending authorization to the correct transaction.")]
        [Explanation("This transaction didn't require 3D secure. So you may be submitted 3D secure authorization to the wrong transaction")]
        TransactionNotEnrolledInThreeDSecure = 7003,

        [LocalizedDescription("The transaction identified was not found")]
        [Explanation("Please check your referencing the correct transaction")]
        TransactionNotFound = 7004,

        [LocalizedDescription("We where unable to process the 3D Secure authorization.")]
        [Explanation("We've been unable to process your 3D Secure authorization")]
        ThreeDSecureNotSuccessful = 7005
    }
    // ReSharper restore InconsistentNaming
}