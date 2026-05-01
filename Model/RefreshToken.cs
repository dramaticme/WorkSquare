using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace worksquare.Model
{
    /// <summary>
    /// Represents a stored refresh token (whitelist entry).
    /// Only refresh tokens are persisted in the database.
    /// Access tokens (15 min) rely solely on signature + expiry – no DB check.
    /// </summary>
    public class RefreshToken
    {
        public int Id { get; set; }

        /// <summary>Unique token identifier (GUID) — JTI claim value.</summary>
        public required string Jti { get; set; }

        /// <summary>The actual signed JWT refresh token string.</summary>
        public required string Token { get; set; }

        public required int UserId { get; set; }
        public User User { get; set; } = null!;

        public required int CompanyId { get; set; }

        public required string DeviceIp { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public class Configuration : IEntityTypeConfiguration<RefreshToken>
        {
            public void Configure(EntityTypeBuilder<RefreshToken> builder)
            {
                builder.HasKey(rt => rt.Id);

                builder.HasIndex(rt => rt.Jti).IsUnique();
                builder.Property(rt => rt.Jti).HasMaxLength(36).IsRequired();
                builder.Property(rt => rt.DeviceIp).HasMaxLength(45).IsRequired();

                builder.HasOne(rt => rt.User)
                       .WithMany()
                       .HasForeignKey(rt => rt.UserId)
                       .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}
