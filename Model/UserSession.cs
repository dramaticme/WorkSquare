using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace worksquare.Model
{
    /// <summary>
    /// Tracks active login sessions per user / device.
    /// Linked 1-to-1 with a RefreshToken row via Jti.
    /// </summary>
    public class UserSession
    {
        public int Id { get; set; }

        public required int UserId { get; set; }
        public User User { get; set; } = null!;

        public required int CompanyId { get; set; }

        /// <summary>The JTI of the active refresh token for this session.</summary>
        public required string RefreshTokenJti { get; set; }

        public required string DeviceIp { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastSeenAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public class Configuration : IEntityTypeConfiguration<UserSession>
        {
            public void Configure(EntityTypeBuilder<UserSession> builder)
            {
                builder.HasKey(s => s.Id);

                builder.Property(s => s.DeviceIp).HasMaxLength(45).IsRequired();
                builder.Property(s => s.RefreshTokenJti).HasMaxLength(36).IsRequired();

                builder.HasOne(s => s.User)
                       .WithMany()
                       .HasForeignKey(s => s.UserId)
                       .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}
