using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.DataAccess.Repositories;

namespace VM.API.Features.Users
{
    public static class GetAll
    {
        public class GetAllUserQuery : IQuery<IEnumerable<GetAllUserResult>>
        {
        }
        public class GetAllUserResult
        {
            public int Id { get; set; }
            public string Email { get; set; } = string.Empty;
            public bool IsEmailVerified { get; set; } = false;
        }

        public class Handler(IUserRepository userRepository) : IQueryHandler<GetAllUserQuery, IEnumerable<GetAllUserResult>>
        {
            private readonly IUserRepository _userRepository = userRepository;
            public async Task<ApiResult<IEnumerable<GetAllUserResult>>> Handle(GetAllUserQuery query, CancellationToken token)
            {
                var users = await _userRepository.GetAllAsync();
                var result = users.Select(u => new GetAllUserResult()
                {
                    Id = u.Id,
                    Email = u.Email,
                    IsEmailVerified = u.EmailVerifiedAt != null
                });
                return ApiResult<IEnumerable<GetAllUserResult>>.SuccessResult(result);
            }
        }
    }
}
