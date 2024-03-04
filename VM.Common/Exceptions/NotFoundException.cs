using System.Net;

namespace VM.Common.Exceptions
{
    public class NotFoundException(string code, params string[] arguments) :
        HttpException(HttpStatusCode.NotFound, code, arguments)
    {
    }
}
