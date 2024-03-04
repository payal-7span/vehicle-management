using MediatR;
using VM.API.Common.Models;

namespace VM.API.Common.Interfaces
{
    public interface ICommand : IRequest<ApiResult>
    {
    }

    public interface ICommandHandler<TRequest> : IRequestHandler<TRequest, ApiResult> where TRequest : ICommand
    {
    }

    public interface ICommand<TResponse> : IRequest<ApiResult<TResponse>>
    {
    }

    public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, ApiResult<TResponse>> where TRequest : ICommand<TResponse>
    {
    }
}
