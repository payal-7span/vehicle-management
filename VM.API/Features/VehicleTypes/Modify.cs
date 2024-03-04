using FluentValidation;
using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.VehicleTypes
{
    public static class Modify
    {
        public class ModifyVehicleTypeCommand : BaseModifiableCommand, ICommand
        {
            public string Name { get; set; } = string.Empty;
        }


        public class Handler(IVehicleTypeRepository vehicleTypeRepository) : ICommandHandler<ModifyVehicleTypeCommand>
        {
            private readonly IVehicleTypeRepository _vehicleTypeRepository = vehicleTypeRepository;
            public async Task<ApiResult> Handle(ModifyVehicleTypeCommand command, CancellationToken token)
            {

                var vehicleType = await _vehicleTypeRepository.GetVehicalTypeAsync(command.Name);

                if (vehicleType != null && !vehicleType.IsDeleted && vehicleType.Id != command.Id)
                {
                    throw new BadRequestException("VehicleType_Exist");
                }


                vehicleType = await _vehicleTypeRepository.GetAsync(command.Id);

                if (vehicleType == null || vehicleType.IsDeleted)
                {
                    throw new BadRequestException("VehicleType_NotExist");
                }


                vehicleType.Name = command.Name;

                await _vehicleTypeRepository.ModifyAsync(vehicleType);

                return ApiResult.SuccessResult();
            }
        }

        public class Validator : AbstractValidator<ModifyVehicleTypeCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            }
        }
    }
}
