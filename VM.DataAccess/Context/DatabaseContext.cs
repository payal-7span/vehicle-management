using Microsoft.EntityFrameworkCore;
using VM.DataAccess.Entities;

namespace VM.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public virtual DbSet<FeesHead> FeesHeads { get; set; }
        public virtual DbSet<FeesStructure> FeesStructures { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FeesHeadConfiguration).Assembly);
        }
    }
}
