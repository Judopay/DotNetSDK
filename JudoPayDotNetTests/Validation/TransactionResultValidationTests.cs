using System.Linq;
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

            var validator = new PolymorphicValidator<ITransactionResult>(new TransactionResultValidation())
                // ReSharper disable RedundantTypeArgumentsOfMethod
                .Add<PaymentReceiptModel>(new PaymentReceiptValidation())
                .Add<PaymentRequiresThreeDSecureModel>(new PaymentRequiresThreeDSecureModelValidation())
                .Add<PaymentRequiresThreeDSecureTwoModel>(new PaymentRequiresThreeDSecureTwoModelValidation());
            // ReSharper restore RedundantTypeArgumentsOfMethod

            var result = validator.Validate(transactionResult);

            Assert.IsNotNull(result);

            var inner = result.First();

            Assert.IsTrue(inner.PropertyName == "ReceiptId");
        }

    }
}
