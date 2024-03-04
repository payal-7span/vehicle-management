using Microsoft.EntityFrameworkCore;
using VM.Common.Models;
using VM.DataAccess.Context;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories.Shared;

namespace VM.DataAccess.Repositories
{
    public interface IFeesHeadRepository : IBaseRepository<FeesHead>
    {
        Task<FeesHead?> GetFeesHeadAsync(string name);
    }

    public class FeesHeadRepository(DatabaseContext context, ICurrentUser currentUser) :
        BaseRepository<FeesHead>(context, currentUser), IFeesHeadRepository
    {
        public Task<FeesHead?> GetFeesHeadAsync(string name)
        {
            return Query.FirstOrDefaultAsync(u => u.Name == name);
        }
    }
}
