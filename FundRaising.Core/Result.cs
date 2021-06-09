namespace FundRaising.Core
{
    public class Result<T>
    {
        public T Data { get; set; }
        public string ErrorText { get; set; }
        public StatusCode ErrorCode { get; set; }
        public bool Exists => ErrorCode == StatusCode.OK;

        public static Result<T> ServiceSuccessful(T data)
        {
            return new Result<T>
            {
                ErrorCode = StatusCode.OK,
                Data = data
            };
        }

        public static Result<T> ServiceFailed(StatusCode code, string text)
        {
            return new Result<T>
            {
                ErrorCode = code,
                ErrorText = text
            };
        }
    }
    public enum StatusCode
    {
        OK = 200,
        BadRequest = 400,
        NotFound = 404,
        InternalServerError = 500
    }
}









