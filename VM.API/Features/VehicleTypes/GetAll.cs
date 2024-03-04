using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.DataAccess.Repositories;

namespace VM.API.Features.VehicleTypes
{
    public static class GetAll
    {
        public class GetAllVehicleTypeQuery : IQuery<IEnumerable<GetAllVehicleTypeResult>>
        {
        }
        public class GetAllVehicleTypeResult
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class Handler(IVehicleTypeRepository vehicleTypeRepository) : IQueryHandler<GetAllVehicleTypeQuery, IEnumerable<GetAllVehicleTypeResult>>
        {
            private readonly IVehicleTypeRepository _vehicleTypeRepository = vehicleTypeRepository;
            public async Task<ApiResult<IEnumerable<GetAllVehicleTypeResult>>> Handle(GetAllVehicleTypeQuery query, CancellationToken token)
            {
                var vehicleTypes = await _vehicleTypeRepository.GetAllAsync();
                var result = vehicleTypes.Select(vt => new GetAllVehicleTypeResult()
                {
                    Id = vt.Id,
                    Name = vt.Name
                });
                return ApiResult<IEnumerable<GetAllVehicleTypeResult>>.SuccessResult(result);
            }


        }
    }
}
