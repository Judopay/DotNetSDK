using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;

namespace JudoPayDotNet.Models
{
    public interface IResult<T> : IResult
    {
        T Response { get; }
    }

    public interface IResult
    {
        bool HasError { get; }
        JudoApiErrorModel Error { get; }
    }
}
