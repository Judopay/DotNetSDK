using JudoPayDotNet.Errors;

namespace JudoPayDotNet.Models
{
    public class Result<T> : Result, IResult<T>
    {
        public T Response { get; private set; }

        public Result(T response, ModelError error) : base(error)
        {
            Response = response;
        }
    }

    public class Result : IResult
    {
        public bool HasError { get { return Error != null; } }
        public ModelError Error { get; private set; }

        public Result(ModelError error = null)
        {
            Error = error;
        }
    }
}
