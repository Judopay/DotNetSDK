namespace JudoPayDotNet.Enums
{
	/// <summary>
	/// Which judo environment should we connect to.
	/// </summary>
    public enum JudoEnvironment
    {
		/// <summary>
		/// Sandbox is our development and test environment
		/// </summary>
		/// <remarks>Only test card numbers work in our Sandbox environment, live cards will be rejected</remarks>
        Sandbox,
		
		/// <summary>
		/// And live is our live environment (unsurprisingly!)
		/// </summary>
		/// <remarks>Only real card numbers work in our live environment, test cards will be rejected</remarks>
        Live
    }
}
