using System.Runtime.Serialization;

namespace JudoPayDotNet.Models.Internal
{
    /// <summary>
    /// Model to complete a 3DS2 transaction on internal Api-Version 6.x
    /// </summary>
    [DataContract(Name = "CompleteThreeDSecureTwoModel", Namespace = "")]
    public class InternalCompleteThreeDSecureTwoModel
    {
        /// <summary>
        /// Gets or sets the CV2.
        /// </summary>
        /// <value>
        /// The CV2.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CV2 { get; set; }
        
        /// <summary>
        /// Gets or sets the Version.
        /// </summary>
        /// <value>
        /// The CV2.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string Version { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PrimaryAccountDetailsModel PrimaryAccountDetails { get; set; }

        public static InternalCompleteThreeDSecureTwoModel From(CompleteThreeDSecureTwoModel externalModel)
        {
            return new InternalCompleteThreeDSecureTwoModel
            {
                // This version can be used for all 3DS 2.x versions
                Version = "2.0.0",
                CV2 = externalModel.CV2,
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
