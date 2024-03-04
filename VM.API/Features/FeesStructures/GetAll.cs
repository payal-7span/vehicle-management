using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.DataAccess.Repositories;

namespace VM.API.Features.FeesStructures
{
    public static class GetAll
    {
        public class GetAllFeesStructureQuery : IQuery<IEnumerable<GetAllFeesStructureResult>>
        {
        }
        public class GetAllFeesStructureResult
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
        public class HeadAndTypeResult
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
        public class Handler(IFeesStructureRepository feesStructureRepository) : IQueryHandler<GetAllFeesStructureQuery, IEnumerable<GetAllFeesStructureResult>>
        {
            private readonly IFeesStructureRepository _feesStructureRepository = feesStructureRepository;
            public async Task<ApiResult<IEnumerable<GetAllFeesStructureResult>>> Handle(GetAllFeesStructureQuery query, CancellationToken token)
            {
                var feesStructures = await _feesStructureRepository.GetAllAsync();
                var result = feesStructures.Select(fs => new GetAllFeesStructureResult()
                {
                    Id = fs.Id,
                    TypeId = fs.TypeId,
                    FeesHeadId = fs.FeesHeadId,
                    IsFixOrPercentage = fs.IsFixOrPercentage,
                    Value = fs.Value,
                    MinValue = fs.MinValue,
                    MaxValue = fs.MaxValue,
                    FeesHead = new HeadAndTypeResult()
                    {
                        Id = fs.FeesHead.Id,
                        Name = fs.FeesHead.Name
                    },
                    VehicalType = fs.Type == null ? null : new HeadAndTypeResult()
                    {
                        Id = fs.Type.Id,
                        Name = fs.Type.Name
                    }
                });
                return ApiResult<IEnumerable<GetAllFeesStructureResult>>.SuccessResult(result);
            }


        }
    }
}
