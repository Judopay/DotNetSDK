using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;
using JudoPayDotNet.Validation;
using NUnit.Framework;

namespace JudoPayDotNetTests.Validation
{
    [TestFixture]
    public class TransactionResultValidationTests
    {
        [Test]
        public void ValidateATransactionResult()
        {

            ITransactionResult transactionResult = new PaymentReceiptModel();

            var validator = new PolymorphicValidator<ITransactionResult>(new TransactionResulttValidation())
                .Add<PaymentReceiptModel>(new PaymentReceiptValidation())
                .Add<PaymentRequiresThreeDSecureModel>(new PaymentRequiresThreeDSecureModelValidation());

            validator.Validate(transactionResult);
        }

    }
}
