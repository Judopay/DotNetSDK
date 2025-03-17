using System.Collections.Generic;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Errors
{
	/// <summary>
	/// This model represents a validation or processing error using Api-Version 4.x. See the <see cref="ErrorMessage"/> for more information
	/// </summary>
    public class JudoApiErrorModel
    {
        public string ErrorMessage { get; set; }
        public List<JudoModelError> ModelErrors { get; set; }
        public JudoApiError ErrorType { get; set; }
    }
}
