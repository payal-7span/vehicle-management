using FluentValidation;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.Common.Services;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories;

namespace VM.API.Features.Users
{
    public class SetPassword
    {
        public class SetPasswordCommand : ICommand
        {
            public string Email { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
        }

        public class Handler(IUserRepository userRepository, ICryptoHashService cryptoHashService) :
            ICommandHandler<SetPasswordCommand>
        {
            private readonly IUserRepository _userRepository = userRepository;
            private readonly ICryptoHashService _cryptoHashService = cryptoHashService;

            public async Task<ApiResult> Handle(SetPasswordCommand command, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetUserAsync(command.Email) ?? throw new NotFoundException("User_Not_Found");

                CryptoHash cryptoHash = _cryptoHashService.HashPassword(command.NewPassword);
                user.SetPassword(cryptoHash.Hash, cryptoHash.Salt);
                user.EmailVerifiedAt = DateTime.UtcNow;

                await _userRepository.ModifyAsync(user);

                return ApiResult.SuccessResult();
            }
        }

        public class Validator : AbstractValidator<SetPasswordCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.NewPassword).NotEmpty().MaximumLength(20);
            }
        }
    }
}
