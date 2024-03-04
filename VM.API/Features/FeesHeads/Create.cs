using FluentValidation;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories;

namespace VM.API.Features.FeesHeads
{
    public static class Create
    {
        public class CreateFeesHeadCommand : ICommand<CreateFeesHeadResult>
        {
            public string Name { get; set; } = string.Empty;
        }

        public class CreateFeesHeadResult()
        {
            public int Id { get; set; }
        }

        public class Handler(IFeesHeadRepository feesHeadRepository) : ICommandHandler<CreateFeesHeadCommand, CreateFeesHeadResult>
        {
            private readonly IFeesHeadRepository _feesHeadRepository = feesHeadRepository;
            public async Task<ApiResult<CreateFeesHeadResult>> Handle(CreateFeesHeadCommand command, CancellationToken token)
            {
                var FeesHead = await _feesHeadRepository.GetFeesHeadAsync(command.Name);
                if (FeesHead != null && !FeesHead.IsDeleted)
                {
                    throw new BadRequestException("FeesHead_exist");
                }

                FeesHead newFeesHead = new FeesHead()
                {
                    Name = command.Name
                };

                var result = await _feesHeadRepository.CreateAsync(newFeesHead);

                return ApiResult<CreateFeesHeadResult>.SuccessResult(new CreateFeesHeadResult() { Id = result.Id });
            }
        }

        public class Validator : AbstractValidator<CreateFeesHeadCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            }
        }
    }
}
