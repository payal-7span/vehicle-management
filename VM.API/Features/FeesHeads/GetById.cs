using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.FeesHeads
{
    public static class GetById
    {
        public class GetFeesHeadByIdQuery : BaseModifiableQuery, IQuery<GetFeesHeadByIdResult>
        {
        }
        public class GetFeesHeadByIdResult
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class Handler(IFeesHeadRepository feesHeadRepository) : IQueryHandler<GetFeesHeadByIdQuery, GetFeesHeadByIdResult>
        {
            private readonly IFeesHeadRepository _feesHeadRepository = feesHeadRepository;
            public async Task<ApiResult<GetFeesHeadByIdResult>> Handle(GetFeesHeadByIdQuery query, CancellationToken token)
            {

                var FeesHead = await _feesHeadRepository.GetAsync(query.Id);

                if (FeesHead == null || FeesHead.IsDeleted)
                {
                    throw new BadRequestException("FeesHead_NotExist");
                }
                var result = new GetFeesHeadByIdResult()
                {
                    Id = FeesHead.Id,
                    Name = FeesHead.Name
                };
                return ApiResult<GetFeesHeadByIdResult>.SuccessResult(result);
            }
        }
    }
}
