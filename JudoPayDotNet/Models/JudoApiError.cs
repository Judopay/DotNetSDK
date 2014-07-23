using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    // ReSharper disable UnusedMember.Global
    // ReSharper disable InconsistentNaming
    public enum JudoApiError
    {
        [Description("Sorry, an error has occurred. Please try again later.")]
        General_Error = 0,

        [Description("Sorry, we were unable to perform your request. Please check the information entered.")]
        General_Model_Error = 1,

        [Description("This device or mobile number is not registered.")]
        Consumer_Record_Not_Found = 2,

        [Description("Sorry, the temporary pin you entered is incorrect. Please request a new temporary pin. ")]
        Temporary_Pin_Invalid = 3,

        [Description("Sorry, your login details are incorrect. Please try again.")]
        Invalid_Login = 4,

        [Description("Sorry, your account has been locked. Please set up a new pin.")]
        Login_Failed = 5,

        [Description("Sorry, registration was not successful. Please re-enter your details.")]
        Unable_To_Complete_Registration = 6,

        [Description("Sorry, this action is invalid.")]
        Unauthorized = 7,

        [Description("Your mobile number is already registered.")]
        Already_Registered = 8,

        [Description("Sorry, we were unable to process your payment. Please contact judo at 0203 503 0600.")]
        Payment_System_Error = 9,

        [Description("Sorry, the security code entered is invalid. Please try again.")]
        Payment_CV2_Error = 10,

        [Description("Sorry, your card has been declined. Your card has not been charged.")]
        Payment_Declined = 11,

        [Description("Sorry, we were unable to process your payment. Please try again.")]
        Payment_Failed = 12,

        [Description("Sorry, we were unable to process your request.")]
        Device_Header_Not_Present = 13,

        [Description("Sorry, we could not find your card details. Please enter a new card.")]
        Card_Not_Found = 14,

        [Description("Your mobile number is already registered.")]
        Consumer_Already_Registered = 15,

        [Description("Sorry, the details of the payment request could not be found.")]
        Payment_Request_Not_Found = 16,

        [Description("Sorry, we could not validate the Partner id / Api Token for this request")]
        Partner_Not_Valid = 16,

        [Description("AuthCode was not found or expired")]
        Invalid_Auth_Code = 18,

        [Description("Transaction not found")]
        Transaction_Not_Found = 19,

        [Description("Your good to go!")]
        Validation_Passed = 20,

        [Description("Authentication Failure")]
        AuthenticationFailure = 403,

        [Description("You cannot perform marketplace payments directly, you must use the access token")]
        MustProcessPaymentByToken = 4001,

        [Description("You cannot perform marketplace preAuths directly, you must use the access token")]
        MustProcessPreAuthByToken = 4002,

        [Description("OK")]
        OK = 200,

        [Description("Not found")]
        Not_Found = 404
    }
    // ReSharper restore InconsistentNaming
    // ReSharper restore UnusedMember.Global
}