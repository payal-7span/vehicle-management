using FluentValidation;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories;

namespace VM.API.Features.Users
{
    public class VerifyEmail
    {
        public class VerifyEmailCommand : ICommand
        {
            public string Email { get; set; } = string.Empty;
        }

        public class Handler(IUserRepository userRepository) :
            ICommandHandler<VerifyEmailCommand>
        {
            private readonly IUserRepository _userRepository = userRepository;

            public async Task<ApiResult> Handle(VerifyEmailCommand command, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetUserAsync(command.Email) ?? throw new NotFoundException("User_Not_Found");

                user.EmailVerifiedAt = DateTime.UtcNow;
                await _userRepository.ModifyAsync(user);

                return ApiResult.SuccessResult();
            }
        }

        public class Validator : AbstractValidator<VerifyEmailCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
            }
        }
    }
}
