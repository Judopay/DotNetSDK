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
        public string CV2 { get; set; }

        public static InternalResumeThreeDSecureTwoModel From(ResumeThreeDSecureTwoModel externalModel)
        {
            return new InternalResumeThreeDSecureTwoModel
            {
                ThreeDSecure = new InternalThreeDSecureTwoModel
                {
                    MethodCompletion = externalModel.MethodCompletion
                },
                CV2 = externalModel.CV2
            };
        }
    }
}
