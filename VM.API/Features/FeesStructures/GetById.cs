using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.FeesStructures
{
    public static class GetById
    {
        public class GetFeesStructureByIdQuery : BaseModifiableQuery, IQuery<GetFeesStructureByIdResult>
        {
        }
        public class GetFeesStructureByIdResult
        {
            public int Id { get; set; }
            public int? TypeId { get; set; }
            public int FeesHeadId { get; set; }
            public string IsFixOrPercentage { get; set; } = string.Empty;
            public double? Value { get; set; }
            public double? MinValue { get; set; }
            public double? MaxValue { get; set; }
        }

        public class Handler(IFeesStructureRepository feesStructureRepository) : IQueryHandler<GetFeesStructureByIdQuery, GetFeesStructureByIdResult>
        {
            private readonly IFeesStructureRepository _feesStructureRepository = feesStructureRepository;
            public async Task<ApiResult<GetFeesStructureByIdResult>> Handle(GetFeesStructureByIdQuery query, CancellationToken token)
            {

                var feesStructure = await _feesStructureRepository.GetAsync(query.Id);

                if (feesStructure == null || feesStructure.IsDeleted)
                {
                    throw new BadRequestException("FeesStructure_NotExist");
                }
                var result = new GetFeesStructureByIdResult()
                {
                    Id = feesStructure.Id,
                    TypeId = feesStructure.TypeId,
                    FeesHeadId = feesStructure.FeesHeadId,
                    IsFixOrPercentage = feesStructure.IsFixOrPercentage,
                    Value = feesStructure.Value,
                    MinValue = feesStructure.MinValue,
                    MaxValue = feesStructure.MaxValue,

                };
                return ApiResult<GetFeesStructureByIdResult>.SuccessResult(result);
            }
        }
    }
}
