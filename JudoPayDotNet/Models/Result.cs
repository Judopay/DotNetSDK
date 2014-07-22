using JudoPayDotNet.Errors;

namespace JudoPayDotNet.Models
{
    public class Result<T> : Result, IResult<T>
    {
        public T Response { get; private set; }

        public Result(T response, JudoApiErrorModel error) : base(error)
        {
            Response = response;
        }
    }

    public class Result : IResult
    {
        public bool HasError { get { return Error != null; } }
        public JudoApiErrorModel Error { get; private set; }

        public Result(JudoApiErrorModel error = null)
        {
            Error = error;
        }
    }
}
