using FluentValidation;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.Common.Services;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories;

namespace VM.API.Features.Auth
{
    public class Login
    {
        public class LoginCommand : ICommand<LoginResult>
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class LoginResult
        {
            public string Token { get; set; } = string.Empty;
            public UserResult User { get; set; } = new UserResult();
        }

        public class UserResult
        {
            public int Id { get; set; }
            public string Email { get; set; } = string.Empty;
        }

        public class Handler(
            IUserRepository userRepository,
            ICryptoHashService cryptoHashService,
            ITokenService tokenService) : ICommandHandler<LoginCommand, LoginResult>
        {
            private readonly ICryptoHashService _cryptoHashService = cryptoHashService;
            private readonly IUserRepository _userRepository = userRepository;
            private readonly ITokenService _tokenService = tokenService;

            public async Task<ApiResult<LoginResult>> Handle(LoginCommand command, CancellationToken cancellationToken)
            {

                User? user = await _userRepository.GetUserAsync(command.Email) ?? throw new BadRequestException("Email_Password_Invalid", command.Email);

                if (!_cryptoHashService.VerifyPassword(command.Password, new CryptoHash() { Hash = user.Password, Salt = user.Salt }))
                    throw new BadRequestException("Email_Password_Invalid");

                string token = _tokenService.GenerateAccessToken(user.Id);

                LoginResult result = new LoginResult
                {
                    Token = token,
                    User = new UserResult
                    {
                        Id = user.Id,
                        Email = user.Email,
                    }
                };

                return ApiResult<LoginResult>.SuccessResult(result);
            }
        }

        public class Validator : AbstractValidator<LoginCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
}
