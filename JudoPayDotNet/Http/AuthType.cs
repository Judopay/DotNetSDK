namespace JudoPayDotNet.Http
{
	/// <summary>
	/// Which authorization scheme should we use when connecting to the JudoPay API
	/// </summary>
	public enum AuthType
	{
		/// <summary>
		/// This is basic http authentication. You should supply your api token and secret as the username and password.
		/// </summary>
		Basic,
		/// <summary>
		/// OAuth 2 authorization
		/// </summary>
		Bearer,
		Unknown
	}
}