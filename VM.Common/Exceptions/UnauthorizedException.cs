using System.Net;

namespace VM.Common.Exceptions
{
    public class UnauthorizedException(string code, params string[] arguments) :
        HttpException(HttpStatusCode.Unauthorized, code, arguments)
    {
    }
}
