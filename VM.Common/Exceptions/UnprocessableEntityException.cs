using System.Net;

namespace VM.Common.Exceptions
{
    public class UnprocessableEntityException() :
        HttpException(HttpStatusCode.UnprocessableEntity, "Model_Validation_Failed")
    {
    }
}