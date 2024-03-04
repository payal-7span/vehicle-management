using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;
using static VM.API.Features.FeesStructures.GetAll;

namespace VM.API.Features.FeesStructures
{
    public static class GetByTypeId
    {
        public class GetFeesStructureByTypeIdQuery : IQuery<IEnumerable<GetFeesStructureByTypeIdResult>>
        {
            public int TypeId { get; private set; }
            public void SetTypeId(int typeId)
            {
                TypeId = typeId;
            }
        }
        public class GetFeesStructureByTypeIdResult
        {
            public int Id { get; set; }
            public int? TypeId { get; set; }
            public int FeesHeadId { get; set; }
            public string IsFixOrPercentage { get; set; } = string.Empty;
            public double? Value { get; set; }
            public double? MinValue { get; set; }
            public double? MaxValue { get; set; }
            public HeadAndTypeResult? VehicalType { get; set; } = null;
            public HeadAndTypeResult FeesHead { get; set; } = new HeadAndTypeResult();
        }
        public class Handler(IFeesStructureRepository feesStructureRepository) : IQueryHandler<GetFeesStructureByTypeIdQuery, IEnumerable<GetFeesStructureByTypeIdResult>>
        {
            private readonly IFeesStructureRepository _feesStructureRepository = feesStructureRepository;
            public async Task<ApiResult<IEnumerable<GetFeesStructureByTypeIdResult>>> Handle(GetFeesStructureByTypeIdQuery query, CancellationToken token)
            {

                var feesStructure = await _feesStructureRepository.GetAllAsync(false, query.TypeId) ?? throw new NotFoundException("FeesStructure_NotExist");

                var result = feesStructure.Select(t => new GetFeesStructureByTypeIdResult()
                {
                    Id = t.Id,
                    TypeId = t.TypeId,
                    FeesHeadId = t.FeesHeadId,
                    IsFixOrPercentage = t.IsFixOrPercentage,
                    Value = t.Value,
                    MinValue = t.MinValue,
                    MaxValue = t.MaxValue,
                    FeesHead = new HeadAndTypeResult()
                    {
                        Id = t.FeesHead.Id,
                        Name = t.FeesHead.Name
                    },
                    VehicalType = t.Type == null ? null : new HeadAndTypeResult()
                    {
                        Id = t.Type.Id,
                        Name = t.Type.Name
                    }

                });
                return ApiResult<IEnumerable<GetFeesStructureByTypeIdResult>>.SuccessResult(result);
            }
        }
    }
}
