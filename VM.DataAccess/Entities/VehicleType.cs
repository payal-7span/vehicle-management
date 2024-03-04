using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VM.DataAccess.Entities.Shared;

namespace VM.DataAccess.Entities
{
    public partial class VehicleType : IEntity, IAuditable, ISoftDeleted
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

    public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(EntityTypeBuilder<VehicleType> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_VehicleTypes");

            entity.ToTable("VehicleType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
