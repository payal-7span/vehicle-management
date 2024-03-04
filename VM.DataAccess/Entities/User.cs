using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VM.DataAccess.Entities.Shared;

namespace VM.DataAccess.Entities
{
    public partial class User : IEntity, ICreatable, ISoftDeleted
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? EmailVerifiedAt { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public void SetPassword(string password, string salt)
        {
            Password = password;
            Salt = salt;
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.EmailVerifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Salt).HasMaxLength(40);
        }
    }
}