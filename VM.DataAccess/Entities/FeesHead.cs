using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VM.DataAccess.Entities.Shared;

namespace VM.DataAccess.Entities
{
    public partial class FeesHead : IEntity, IAuditable, ISoftDeleted
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<FeesStructure> FeesStructures { get; set; } = new List<FeesStructure>();
    }

    public class FeesHeadConfiguration : IEntityTypeConfiguration<FeesHead>
    {
        public void Configure(EntityTypeBuilder<FeesHead> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_FeesMaster");

            entity.ToTable("FeesHead");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        }
    }
}