using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    /// <summary>
    /// Default permissions for a given CompanyRoleEnum.
    /// CompanyId = 0  → platform-wide default (seeded once, applies to all companies).
    /// CompanyId > 0  → company-specific override of the platform default.
    /// </summary>
    public class RolePermission : BaseEntity
    {   
        /// <summary>Role this default applies to.</summary>
        public CompanyRoleEnum Role { get; set; }

        /// <summary>Permission key, e.g. "Projects.View.Own".</summary>
        public required string Permission { get; set; }

        /// <summary>
        /// 0 = platform default (all companies).
        /// Greater than 0 = company-specific override.
        /// </summary>
        public int CompanyId { get; set; } = 0;

        public class Configuration : IEntityTypeConfiguration<RolePermission>
        {
            public void Configure(EntityTypeBuilder<RolePermission> builder)
            {
                builder.HasKey(rp => rp.Id);

                builder.Property(rp => rp.Permission)
                       .HasMaxLength(128)
                       .IsRequired();

                // Ensure no duplicate role+permission combo within the same company scope
                builder.HasIndex(rp => new { rp.Role, rp.Permission, rp.CompanyId })
                       .IsUnique();
            }
        }
    }
}
