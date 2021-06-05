using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core
{
    public class Result<T>
    {
        public Error Error { get; set; }
        public T Data { get; set; }
        public Result()
        {

        }
        public Result(ErrorCode _errorCode, string _errorText)
        {
            Error = new Error
            {
                ErrorCode = _errorCode,
                Message = _errorText
            };
        }
    }

    public class Error
    {
        public ErrorCode ErrorCode { get; set; }
        public string Message { get; set; }
    }

    public enum ErrorCode
    {
        Unspecified = 0,
        NotFound = 1,
        BadRequest = 2,
        Conflict = 3,
        InternalServerError = 4
    }









}
