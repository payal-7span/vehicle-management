using FluentValidation;
using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Constants;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.FeesStructures
{
    public static class Modify
    {
        public class ModifyFeesStructureCommand : BaseModifiableCommand, ICommand
        {
            public int? TypeId { get; set; }
            public int FeesHeadId { get; set; }
            public FeesStructureTypeEnum IsFixOrPercentage { get; set; }
            public double? Value { get; set; }
            public double? MinValue { get; set; }
            public double? MaxValue { get; set; }
        }


        public class Handler(IFeesStructureRepository feesStructureRepository) : ICommandHandler<ModifyFeesStructureCommand>
        {
            private readonly IFeesStructureRepository _feesStructureRepository = feesStructureRepository;
            public async Task<ApiResult> Handle(ModifyFeesStructureCommand command, CancellationToken token)
            {

                var feesStructure = await _feesStructureRepository.GetAsync(command.Id);

                if (feesStructure == null || feesStructure.IsDeleted)
                {
                    throw new BadRequestException("FeesStructure_NotExist");
                }

                feesStructure.TypeId = command.TypeId;
                feesStructure.FeesHeadId = command.FeesHeadId;
                feesStructure.IsFixOrPercentage = command.IsFixOrPercentage.ToString();
                feesStructure.Value = command.Value;
                feesStructure.MinValue = command.MinValue;
                feesStructure.MaxValue = command.MaxValue;

                await _feesStructureRepository.ModifyAsync(feesStructure);

                return ApiResult.SuccessResult();
            }
        }

        public class Validator : AbstractValidator<ModifyFeesStructureCommand>
        {
            public Validator()
            {
				RuleFor(x => x.FeesHeadId).NotEmpty();
			}
        }
    }
}
