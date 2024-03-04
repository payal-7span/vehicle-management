using FluentValidation;
using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.Users
{
    public static class Modify
    {
        public class ModifyUserCommand : BaseModifiableCommand, ICommand
        {
            public string Email { get; set; } = string.Empty;
        }


        public class Handler(IUserRepository userRepository) : ICommandHandler<ModifyUserCommand>
        {
            private readonly IUserRepository _userRepository = userRepository;
            public async Task<ApiResult> Handle(ModifyUserCommand command, CancellationToken token)
            {

                var User = await _userRepository.GetUserAsync(command.Email);

                if (User != null && !User.IsDeleted && User.Id != command.Id)
                {
                    throw new BadRequestException("User_Exist");
                }


                User = await _userRepository.GetAsync(command.Id);

                if (User == null || User.IsDeleted)
                {
                    throw new BadRequestException("User_NotExist");
                }


                User.Email = command.Email;
                User.EmailVerifiedAt = null;

                await _userRepository.ModifyAsync(User);

                return ApiResult.SuccessResult();
            }
        }

        public class Validator : AbstractValidator<ModifyUserCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
            }
        }
    }
}
