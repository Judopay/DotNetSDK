namespace JudoPayDotNet.Enums
{
    public enum ThreeDSecureTwoChallengeRequestIndicator : long
    {
        NoPreference = 0,
        NoChallenge = 1,
        ChallengePreferred = 2,
        ChallengeAsMandate = 3,
        ChallengeWithWhitelistPrompt = 4
    }
}
