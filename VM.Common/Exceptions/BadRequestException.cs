using System.Net;

namespace VM.Common.Exceptions
{
    public class BadRequestException(string code, params string[] arguments) :
        HttpException(HttpStatusCode.BadRequest, code, arguments)
    {
    }
}
