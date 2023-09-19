using System.Runtime.Serialization;

namespace JudoPayDotNet.Models.Internal
{
    /// <summary>
    /// Model to resume a 3DS2 transaction on internal Api-Version 6.x
    /// </summary>
    [DataContract(Name = "ResumeThreeDSecureTwoModel", Namespace = "")]
    public class InternalResumeThreeDSecureTwoModel
    {
        [DataMember]
        public InternalThreeDSecureTwoModel ThreeDSecure { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Cv2 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PrimaryAccountDetailsModel PrimaryAccountDetails { get; set; }

        public static InternalResumeThreeDSecureTwoModel From(ResumeThreeDSecureTwoModel externalModel)
        {
            return new InternalResumeThreeDSecureTwoModel
            {
                ThreeDSecure = new InternalThreeDSecureTwoModel
                {
                    MethodCompletion = externalModel.MethodCompletion
                },
                Cv2 = externalModel.CV2,
                PrimaryAccountDetails = !string.IsNullOrEmpty(externalModel.PrimaryAccountDetails?.AccountNumber) ? new PrimaryAccountDetailsModel()
                {
                    PostCode = externalModel.PrimaryAccountDetails?.PostCode,
                    AccountNumber = externalModel.PrimaryAccountDetails?.AccountNumber,
                    DateOfBirth = externalModel.PrimaryAccountDetails?.DateOfBirth,
                    Name = externalModel.PrimaryAccountDetails?.Name
                } : null
            };
        }
    }
}
