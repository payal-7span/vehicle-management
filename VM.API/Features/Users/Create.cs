using FluentValidation;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories;

namespace VM.API.Features.Users
{
    public static class Create
    {
        public class CreateUserCommand : ICommand<CreateUserResult>
        {
            public string Email { get; set; } = string.Empty;
        }

        public class CreateUserResult()
        {
            public int Id { get; set; }
        }

        public class Handler(IUserRepository userRepository) : ICommandHandler<CreateUserCommand, CreateUserResult>
        {
            private readonly IUserRepository _userRepository = userRepository;
            public async Task<ApiResult<CreateUserResult>> Handle(CreateUserCommand command, CancellationToken token)
            {
                var User = await _userRepository.GetUserAsync(command.Email);
                if (User != null && !User.IsDeleted)
                {
                    throw new BadRequestException("User_exist");
                }

                User newUser = new()
                {
                    Email = command.Email
                };

                var result = await _userRepository.CreateAsync(newUser);

                return ApiResult<CreateUserResult>.SuccessResult(new CreateUserResult() { Id = result.Id });
            }
        }

        public class Validator : AbstractValidator<CreateUserCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
            }
        }
    }
}
