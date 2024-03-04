using Microsoft.EntityFrameworkCore;
using VM.Common.Exceptions;
using VM.Common.Models;
using VM.DataAccess.Context;
using VM.DataAccess.Entities.Shared;

namespace VM.DataAccess.Repositories.Shared
{
    public interface IBaseRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(bool requireDeleted = false);
        Task<TEntity?> GetAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task ModifyAsync(TEntity entity);
        Task RemoveAsync(int id);
        Task RemoveAsync(TEntity entity);
        void InitCreate(TEntity entity);
        void InitModify(TEntity entity);
        Task SaveChangesAsync();
    }

    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity, ISoftDeleted
    {
        private readonly DatabaseContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IQueryable<TEntity> _dbQuery;

        public BaseRepository(DatabaseContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;

            _dbSet = _context.Set<TEntity>();
            _dbQuery = _dbSet.AsQueryable();
        }

        protected virtual IQueryable<TEntity> Query
        {
            get { return _dbQuery; }
        }

        protected virtual DatabaseContext Context
        {
            get { return _context; }
        }

        public virtual Task<List<TEntity>> GetAllAsync(bool requireDeleted = false)
        {
            if (requireDeleted)
                return Query.AsNoTracking().ToListAsync();
            else
                return Query.AsNoTracking().Where(t => !t.IsDeleted).ToListAsync();
        }

        public virtual Task<TEntity?> GetAsync(int id)
        {
            return Query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual Task<bool> ExistsAsync(int id)
        {
            return Query.AnyAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            InitCreate(entity);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task ModifyAsync(TEntity entity)
        {
            InitModify(entity);
            await SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(int id)
        {
            var obj = await GetAsync(id) ?? throw new NotFoundException("Not_Found");
            await RemoveAsync(obj);
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            InitRemove(entity);
            await SaveChangesAsync();
        }

        public virtual void InitCreate(TEntity entity)
        {
            if (entity is ICreatable)
            {
                if (entity is not ICreatable auditableEntity) throw new UnauthorizedException("Loggedin_Required");
                auditableEntity.CreatedAt = DateTime.UtcNow;
                auditableEntity.CreatedBy = _currentUser.Id;
            }
            _dbSet.Add(entity);
        }

        public virtual void InitModify(TEntity entity)
        {
            if (entity is IAuditable)
            {
                if (entity is not IAuditable auditableEntity) throw new UnauthorizedException("Loggedin_Required");
                auditableEntity.UpdatedAt = DateTime.UtcNow;
                auditableEntity.UpdatedBy = _currentUser.Id;
            }

            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        private void InitRemove(TEntity entity)
        {
            if (entity is ISoftDeleted)
            {
                if (entity is not ISoftDeleted softDeletedEntity) throw new BadRequestException("Entity_NotNull");
                softDeletedEntity.IsDeleted = true;
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    Context.Attach(entity);
                    Context.Entry(softDeletedEntity).State = EntityState.Modified;
                }
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        public virtual async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
