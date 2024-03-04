using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.Users
{
    public static class Delete
    {
        public class DeleteUserCommand : BaseModifiableCommand, ICommand
        {
        }


        public class Handler(IUserRepository userRepository) : ICommandHandler<DeleteUserCommand>
        {
            private readonly IUserRepository _userRepository = userRepository;
            public async Task<ApiResult> Handle(DeleteUserCommand command, CancellationToken token)
            {

                var User = await _userRepository.GetAsync(command.Id);

                if (User == null || User.IsDeleted)
                {
                    throw new BadRequestException("User_NotExist");
                }

                await _userRepository.RemoveAsync(User);

                return ApiResult.SuccessResult();
            }
        }
    }
}
