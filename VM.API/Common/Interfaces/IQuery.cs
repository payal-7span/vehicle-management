using MediatR;
using VM.API.Common.Models;

namespace VM.API.Common.Interfaces
{
    public interface IQuery : IRequest<ApiResult>
    {
    }

    public interface IQueryHandler<TRequest> : IRequestHandler<TRequest, ApiResult> where TRequest : IQuery
    {
    }

    public interface IQuery<TRequest> : IRequest<ApiResult<TRequest>>
    {
    }

    public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, ApiResult<TResponse>> where TRequest : IQuery<TResponse>
    {
    }
}
