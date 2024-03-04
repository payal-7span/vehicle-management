using FluentValidation;
using MediatR;
using VM.Common.Exceptions;

namespace VM.API.Pipelines
{
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<object>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var validationFailed = validationResults.Any(x => x.IsValid == false);

                if (validationFailed)
                    throw new UnprocessableEntityException();
            }
            return await next();
        }
    }
}
