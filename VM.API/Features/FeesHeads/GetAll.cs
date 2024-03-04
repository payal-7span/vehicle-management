using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.DataAccess.Repositories;

namespace VM.API.Features.FeesHeads
{
    public static class GetAll
    {
        public class GetAllFeesHeadQuery : IQuery<IEnumerable<GetAllFeesHeadResult>>
        {
        }
        public class GetAllFeesHeadResult
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class Handler(IFeesHeadRepository feesHeadRepository) : IQueryHandler<GetAllFeesHeadQuery, IEnumerable<GetAllFeesHeadResult>>
        {
            private readonly IFeesHeadRepository _feesHeadRepository = feesHeadRepository;
            public async Task<ApiResult<IEnumerable<GetAllFeesHeadResult>>> Handle(GetAllFeesHeadQuery query, CancellationToken token)
            {
                var feesHeads = await _feesHeadRepository.GetAllAsync();
                var result = feesHeads.Select(fh => new GetAllFeesHeadResult()
                {
                    Id = fh.Id,
                    Name = fh.Name
                });
                return ApiResult<IEnumerable<GetAllFeesHeadResult>>.SuccessResult(result);
            }
        }
    }
}
