using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.VehicleTypes
{
    public static class GetById
    {
        public class GetVehicleTypeByIdQuery : BaseModifiableQuery, IQuery<GetVehicleTypeByIdResult>
        {
        }
        public class GetVehicleTypeByIdResult
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class Handler(IVehicleTypeRepository vehicleTypeRepository) : IQueryHandler<GetVehicleTypeByIdQuery, GetVehicleTypeByIdResult>
        {
            private readonly IVehicleTypeRepository _vehicleTypeRepository = vehicleTypeRepository;
            public async Task<ApiResult<GetVehicleTypeByIdResult>> Handle(GetVehicleTypeByIdQuery query, CancellationToken token)
            {

                var vehicleType = await _vehicleTypeRepository.GetAsync(query.Id);

                if (vehicleType == null || vehicleType.IsDeleted)
                {
                    throw new BadRequestException("VehicleType_NotExist");
                }
                var result = new GetVehicleTypeByIdResult()
                {
                    Id = vehicleType.Id,
                    Name = vehicleType.Name
                };
                return ApiResult<GetVehicleTypeByIdResult>.SuccessResult(result);
            }
        }
    }
}
