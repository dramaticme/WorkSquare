using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace worksquare.Model
{
    /// <summary>
    /// Per-user explicit permission grant or deny.
    /// Overrides the role default from RolePermission.
    ///
    /// Evaluation order:
    ///   1. UserPermission.IsGranted = false  → DENY  (always wins)
    ///   2. UserPermission.IsGranted = true   → GRANT (per-user override)
    ///   3. RolePermission default             → role default
    ///   4. No match                           → DENY
    /// </summary>
    public class UserPermission
    {
        public int Id { get; set; }

        public required int UserId { get; set; }
        public User User { get; set; } = null!;

        public required int CompanyId { get; set; }

        /// <summary>Permission key, e.g. "Projects.Create".</summary>
        public required string Permission { get; set; }

        /// <summary>true = explicit grant; false = explicit deny.</summary>
        public bool IsGranted { get; set; }

        /// <summary>
        /// Optional expiry for time-limited overrides.
        /// Null = permanent. Expired grants/denies are ignored (treated as no override).
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>UserId of the Admin who created this override.</summary>
        public required int GrantedByUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public class Configuration : IEntityTypeConfiguration<UserPermission>
        {
            public void Configure(EntityTypeBuilder<UserPermission> builder)
            {
                builder.HasKey(up => up.Id);

                builder.Property(up => up.Permission)
                       .HasMaxLength(128)
                       .IsRequired();

                // One explicit override per user+permission within a company
                builder.HasIndex(up => new { up.UserId, up.Permission, up.CompanyId })
                       .IsUnique();

                builder.HasOne(up => up.User)
                       .WithMany()
                       .HasForeignKey(up => up.UserId)
                       .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}
