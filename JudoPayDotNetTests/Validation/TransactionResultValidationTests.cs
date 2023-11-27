using System.Linq;
using JudoPayDotNet.Models;
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
            var receipt = new PaymentReceiptModel();

            var validator = new PaymentReceiptValidator();

            var result = validator.Validate(receipt);

            Assert.IsNotNull(result);

            Assert.IsFalse(result.IsValid);

            var firstError = result.Errors.First();

            Assert.IsTrue(firstError.PropertyName == "ReceiptId");
        }

        [Test]
        public void ValidateAThreeDSecureTwoTransactionResult()
        {
            var transactionResult = new PaymentRequiresThreeDSecureTwoModel();

            var validator = new PaymentRequiresThreeDSecureTwoValidator();

            var result = validator.Validate(transactionResult);

            Assert.IsNotNull(result);

            Assert.IsFalse(result.IsValid);

            var firstError = result.Errors.First();

            Assert.IsTrue(firstError.PropertyName == "ReceiptId");
        }
    }
}
