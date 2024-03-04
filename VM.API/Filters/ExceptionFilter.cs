using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VM.API.Common.Models;
using VM.Common.Exceptions;

namespace VM.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            if (context.Exception is HttpException)
            {
                HandleApiException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleApiException(ExceptionContext context)
        {
            if (context.Exception is HttpException exception)
            {
                var details = ApiResult.ErrorResult(new ServiceError(exception.Code, exception.Arguments));

                context.Result = new ObjectResult(details)
                {
                    StatusCode = (int)exception.StatusCode
                };
            }

            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = ApiResult.ErrorResult(new ServiceError(context.Exception.Message));

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
