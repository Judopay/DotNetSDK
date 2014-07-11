using JudoPayDotNet.Errors;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A result that has the actual response object typified
    /// </summary>
    /// <typeparam name="T">The actual type of response</typeparam>
    public interface IResult<T> : IResult
    {
        T Response { get; }
    }

    /// <summary>
    /// A result that has the error information
    /// </summary>
    public interface IResult
    {
        bool HasError { get; }
        JudoApiErrorModel Error { get; }
    }
}
