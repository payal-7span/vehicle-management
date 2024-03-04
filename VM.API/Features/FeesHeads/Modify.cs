using FluentValidation;
using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.FeesHeads
{
    public static class Modify
    {
        public class ModifyFeesHeadCommand : BaseModifiableCommand, ICommand
        {
            public string Name { get; set; } = string.Empty;
        }


        public class Handler(IFeesHeadRepository feesHeadRepository) : ICommandHandler<ModifyFeesHeadCommand>
        {
            private readonly IFeesHeadRepository _feesHeadRepository = feesHeadRepository;
            public async Task<ApiResult> Handle(ModifyFeesHeadCommand command, CancellationToken token)
            {

                var FeesHead = await _feesHeadRepository.GetFeesHeadAsync(command.Name);

                if (FeesHead != null && !FeesHead.IsDeleted && FeesHead.Id != command.Id)
                {
                    throw new BadRequestException("FeesHead_Exist");
                }


                FeesHead = await _feesHeadRepository.GetAsync(command.Id);

                if (FeesHead == null || FeesHead.IsDeleted)
                {
                    throw new BadRequestException("FeesHead_NotExist");
                }


                FeesHead.Name = command.Name;

                await _feesHeadRepository.ModifyAsync(FeesHead);

                return ApiResult.SuccessResult();
            }
        }

        public class Validator : AbstractValidator<ModifyFeesHeadCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            }
        }
    }
}
