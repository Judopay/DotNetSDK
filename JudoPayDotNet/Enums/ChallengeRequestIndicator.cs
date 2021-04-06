namespace JudoPayDotNet.Enums
{
    public enum ThreeDSecureTwoChallengeRequestIndicator : long
    {
        [LocalizedDescription("NoPreference")]
        NoPreference = 0,

        /// <summary>
        /// No Challenge
        /// </summary>
        [LocalizedDescription("NoChallenge ")]
        NoChallenge = 1,

        /// <summary>
        /// Challenge Preferred
        /// </summary>
        [LocalizedDescription("ChallengePreferred")]
        ChallengePreferred = 2,

        /// <summary>
        /// Challenge Preferred
        /// </summary>
        [LocalizedDescription("ChallengeAsMandate")]
        ChallengeAsMandate = 3
    }
}