using System.Net;

namespace VM.Common.Exceptions
{
    public class HttpException(HttpStatusCode statusCode, string code, params string[] arguments) : Exception()
    {
        public HttpStatusCode StatusCode { get; } = statusCode;
        public string Code { get; set; } = code;
        public string[] Arguments { get; set; } = arguments;
    }
}
