using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.VehicleTypes
{
    public static class Delete
    {
        public class DeleteVehicleTypeCommand : BaseModifiableCommand, ICommand
        {
        }


        public class Handler(IVehicleTypeRepository vehicleTypeRepository) : ICommandHandler<DeleteVehicleTypeCommand>
        {
            private readonly IVehicleTypeRepository _vehicleTypeRepository = vehicleTypeRepository;
            public async Task<ApiResult> Handle(DeleteVehicleTypeCommand command, CancellationToken token)
            {

                var vehicleType = await _vehicleTypeRepository.GetAsync(command.Id);

                if (vehicleType == null || vehicleType.IsDeleted)
                {
                    throw new BadRequestException("VehicleType_NotExist");
                }

                await _vehicleTypeRepository.RemoveAsync(vehicleType);

                return ApiResult.SuccessResult();
            }
        }
    }
}
