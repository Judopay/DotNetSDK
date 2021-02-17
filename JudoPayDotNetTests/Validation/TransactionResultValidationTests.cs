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
                .Add<PaymentRequiresThreeDSecureModel>(new PaymentRequiresThreeDSecureModelValidator());
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

        [Test]
        public void ValidateAResumeThreeDSecureTwoRequest()
        {
            var resumeRequest = new ResumeThreeDSecureTwoModel();

            var validator = new PolymorphicValidator<ResumeThreeDSecureTwoModel>(new ResumeThreeDSecureTwoValidator())
                // ReSharper disable RedundantTypeArgumentsOfMethod
                .Add<ResumeThreeDSecureTwoModel>(new ResumeThreeDSecureTwoValidator());
            // ReSharper restore RedundantTypeArgumentsOfMethod

            var result = validator.Validate(resumeRequest);

            Assert.IsNotNull(result);

            var inner = result.First();

            Assert.IsTrue(inner.PropertyName == "CV2");
        }

        [Test]
        public void ValidateACompleteThreeDSecureTwoRequest()
        {
            var resumeRequest = new CompleteThreeDSecureTwoModel();

            var validator = new PolymorphicValidator<CompleteThreeDSecureTwoModel>(new CompleteThreeDSecureTwoValidator())
                // ReSharper disable RedundantTypeArgumentsOfMethod
                .Add<CompleteThreeDSecureTwoModel>(new CompleteThreeDSecureTwoValidator());
            // ReSharper restore RedundantTypeArgumentsOfMethod

            var result = validator.Validate(resumeRequest);

            Assert.IsNotNull(result);

            var inner = result.First();

            Assert.IsTrue(inner.PropertyName == "CV2");
        }
    }
}
