using Microsoft.EntityFrameworkCore;
using VM.Common.Models;
using VM.DataAccess.Context;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories.Shared;

namespace VM.DataAccess.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetCurrentUserAsync();
        Task<User?> GetUserAsync(string email);
    }

    public class UserRepository(DatabaseContext context, ICurrentUser currentUser) :
        BaseRepository<User>(context, currentUser), IUserRepository
    {
        private readonly ICurrentUser _currentUser = currentUser;

        public Task<User?> GetCurrentUserAsync()
        {
            return GetAsync(_currentUser.Id);
        }
        public Task<User?> GetUserAsync(string email)
        {
            return Query.FirstOrDefaultAsync(u => u.Email == email && u.EmailVerifiedAt != null);
        }
    }
}
