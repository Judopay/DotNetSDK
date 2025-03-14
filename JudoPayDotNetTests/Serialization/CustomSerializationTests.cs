using JudoPayDotNet.Errors;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.CustomDeserializers;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Serialization
{
    [TestFixture]
    public class CustomSerializationTests
    {
        private const string TestRequestId = "9b1a0c47931bc858a7701541c468f986";

        [Test]
        public void DeSerialize()
        {
            const string serializedMessage = @"
                        {
                            errorMessage : 'Payment not made',
                            modelErrors : [{
                                            fieldName : 'receiptId',
                                            errorMessage : 'To large',
                                            detailErrorMessage : 'This field has to be at most 20 characters'
                                          }],
                            errorType : '11',
                            requestId : '" + TestRequestId + @"'
                        }";

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new JudoApiErrorModelConverter(Substitute.For<ILog>()));

            var judoApiErrorModel = JsonConvert.DeserializeObject<JudoApiErrorModel>(serializedMessage, settings);

            Assert.IsNotNull(judoApiErrorModel);
            Assert.AreEqual(JudoApiError.Payment_Declined, judoApiErrorModel.ErrorType);
            Assert.AreEqual(TestRequestId, judoApiErrorModel.RequestId);
        }

        [Test]
        public void DeSerializeWithoutArrayPresent()
        {
            const string serializedMessage = @"
                        {
                            errorMessage : 'Payment not made',
                            errorType : '11',
                            requestId : '" + TestRequestId + @"'
                        }";

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new JudoApiErrorModelConverter(Substitute.For<ILog>()));

            var judoApiErrorModel = JsonConvert.DeserializeObject<JudoApiErrorModel>(serializedMessage, settings);

            Assert.IsNotNull(judoApiErrorModel);
            Assert.AreEqual(JudoApiError.Payment_Declined, judoApiErrorModel.ErrorType);
            Assert.IsNull(judoApiErrorModel.ModelErrors);
            Assert.AreEqual(TestRequestId, judoApiErrorModel.RequestId);
        }

        [Test]
        public void DeSerializeModelErrorWithoutFieldErrorList()
        {
            const string errorMessage = "Unable to process transaction. Card authentication failed with 3DS Server.";
            const int errorCode = 165;
            const int errorCategory = 4;
            var serializedMessage = @"
                        {
                            message : '" + errorMessage + @"',
                            code : " + errorCode + @",
                            category : " + errorCategory + @",
                            requestId : '" + TestRequestId + @"'
                        }";

            var settings = new JsonSerializerSettings();
            var errorModel = JsonConvert.DeserializeObject<ModelError>(serializedMessage, settings);

            Assert.IsNotNull(errorModel);
            Assert.AreEqual(errorMessage, errorModel.Message);
            Assert.AreEqual(errorCode, errorModel.Code);
            Assert.AreEqual(errorCategory.ToString(), errorModel.Category);
            Assert.AreEqual(TestRequestId, errorModel.RequestId);
        }
    }
}