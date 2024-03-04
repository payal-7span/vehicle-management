using FluentValidation;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories;

namespace VM.API.Features.VehicleTypes
{
    public static class Create
    {
        public class CreateVehicleTypeCommand : ICommand<CreateVehicleTypeResult>
        {
            public string Name { get; set; } = string.Empty;
        }

        public class CreateVehicleTypeResult()
        {
            public int Id { get; set; }
        }

        public class Handler(IVehicleTypeRepository vehicleTypeRepository) : ICommandHandler<CreateVehicleTypeCommand, CreateVehicleTypeResult>
        {
            private readonly IVehicleTypeRepository _vehicleTypeRepository = vehicleTypeRepository;
            public async Task<ApiResult<CreateVehicleTypeResult>> Handle(CreateVehicleTypeCommand command, CancellationToken token)
            {
                var vehicleType = await _vehicleTypeRepository.GetVehicalTypeAsync(command.Name);
                if (vehicleType != null && !vehicleType.IsDeleted)
                {
                    throw new BadRequestException("VehicleType_exist");
                }

                VehicleType newVehicleType = new VehicleType()
                {
                    Name = command.Name
                };

                var result = await _vehicleTypeRepository.CreateAsync(newVehicleType);

                return ApiResult<CreateVehicleTypeResult>.SuccessResult(new CreateVehicleTypeResult() { Id = result.Id });
            }
        }

        public class Validator : AbstractValidator<CreateVehicleTypeCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);

            }
        }
    }
}
