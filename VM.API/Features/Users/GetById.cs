using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.Users
{
    public static class GetById
    {
        public class GetUserByIdQuery : BaseModifiableQuery, IQuery<GetUserByIdResult>
        {
        }
        public class GetUserByIdResult
        {
            public int Id { get; set; }
            public string Email { get; set; } = string.Empty;
        }

        public class Handler(IUserRepository userRepository) : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
        {
            private readonly IUserRepository _userRepository = userRepository;
            public async Task<ApiResult<GetUserByIdResult>> Handle(GetUserByIdQuery query, CancellationToken token)
            {

                var user = await _userRepository.GetAsync(query.Id);

                if (user == null || user.IsDeleted)
                {
                    throw new BadRequestException("User_NotExist");
                }
                var result = new GetUserByIdResult()
                {
                    Id = user.Id,
                    Email = user.Email
                };
                return ApiResult<GetUserByIdResult>.SuccessResult(result);
            }
        }
    }
}
