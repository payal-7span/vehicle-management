using MediatR;
using VM.DataAccess.Context;

namespace VM.API.Pipelines
{
    public class TransactionPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly DatabaseContext _context;
        private readonly string[] _transactionableMethods = ["POST", "PUT", "DELETE", "PATCH"];
        private readonly HttpContext? _httpContext;

        public TransactionPipelineBehaviour(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_httpContext != null && _transactionableMethods.Contains(_httpContext.Request.Method))
            {
                return await HandleTransaction(next, cancellationToken);
            }

            return await next();
        }

        private async Task<TResponse> HandleTransaction(RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_context.Database.CurrentTransaction == null)
            {
                return await HandleOuterTransaction(next, cancellationToken);
            }
            else
            {
                return await HandleInnerTransaction(next, cancellationToken);
            }
        }

        private async Task<TResponse> HandleOuterTransaction(RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await next();
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private async Task<TResponse> HandleInnerTransaction(RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();
            return response;
        }
    }
}
