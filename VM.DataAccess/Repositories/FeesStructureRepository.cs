using Microsoft.EntityFrameworkCore;
using VM.Common.Models;
using VM.DataAccess.Context;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories.Shared;

namespace VM.DataAccess.Repositories
{
    public interface IFeesStructureRepository : IBaseRepository<FeesStructure>
    {
        Task<List<FeesStructure>> GetAllAsync(bool requireDeleted = false, int? typeId = null);
    }

    public class FeesStructureRepository(DatabaseContext context, ICurrentUser currentUser) :
        BaseRepository<FeesStructure>(context, currentUser), IFeesStructureRepository
    {

        public virtual Task<List<FeesStructure>> GetAllAsync(bool requireDeleted = false, int? typeId = null)
        {
            var query = Query.AsNoTracking();
            if (!requireDeleted)
                query = query.Where(t => !t.IsDeleted);

            if (typeId != null)
                query = query.Where(t => t.TypeId == typeId || t.TypeId == null);


            return query
             .Include(t => t.Type)
             .Include(t => t.FeesHead)
             .Where(t => (t.Type == null || !t.Type.IsDeleted) && !t.FeesHead.IsDeleted)
             .ToListAsync();
        }
    }
}
