using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VM.DataAccess.Entities.Shared;

namespace VM.DataAccess.Entities
{
    public partial class FeesStructure : IEntity, IAuditable, ISoftDeleted
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public int FeesHeadId { get; set; }
        public string IsFixOrPercentage { get; set; } = null!;
        public double? Value { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual FeesHead FeesHead { get; set; } = null!;
        public virtual VehicleType? Type { get; set; }
    }

    public class FeesStructureConfiguration : IEntityTypeConfiguration<FeesStructure>
    {
        public void Configure(EntityTypeBuilder<FeesStructure> entity)
        {
            entity.ToTable("FeesStructure");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.IsFixOrPercentage).HasMaxLength(10);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.FeesHead).WithMany(p => p.FeesStructures)
                .HasForeignKey(d => d.FeesHeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FeesStructure_FeesHead");

            entity.HasOne(d => d.Type).WithMany(p => p.FeesStructures)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_FeesStructure_FeesStructure");
        }
    }
}