using System.Linq;
using JudoPayDotNet.Models;
using JudoPayDotNet.Validation;
using JudoPayDotNetTests.Model.Validations;
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
                .Add<PaymentReceiptModel>(new PaymentReceiptValidation());
            // ReSharper restore RedundantTypeArgumentsOfMethod

            var result = validator.Validate(transactionResult);

            Assert.IsNotNull(result);

            var inner = result.First();

            Assert.IsTrue(inner.PropertyName == "ReceiptId");
        }

        [Test]
        public void ValidateAThreeDSecureTwoTransactionResult()
        {
            ITransactionResult transactionResult = new PaymentRequiresThreeDSecureTwoModel();

            var validator = new PolymorphicValidator<ITransactionResult>(new TransactionResultValidation())
                // ReSharper disable RedundantTypeArgumentsOfMethod
                .Add<PaymentRequiresThreeDSecureTwoModel>(new PaymentRequiresThreeDSecureTwoModelValidator());
            // ReSharper restore RedundantTypeArgumentsOfMethod

            var result = validator.Validate(transactionResult);

            Assert.IsNotNull(result);

            var inner = result.First();

            Assert.IsTrue(inner.PropertyName == "ReceiptId");
        }
    }
}
