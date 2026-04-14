using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    public class User : BaseEntity
    {
        public required string Username { get; set; }
        public required string Password { get; set; }

        public bool IsActive { get; set; } = true;
        public CompanyUser? CompanyUser { get; set; }
        public SystemUser? SystemUser { get; set; }

        public class Configuration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Username).HasMaxLength(256).IsRequired();
                builder.Property(u => u.Password).IsRequired();
                builder.HasIndex(u => u.Username).IsUnique();

                builder.HasOne(u => u.CompanyUser)
                       .WithOne(cu => cu.User)
                       .HasForeignKey<CompanyUser>(cu => cu.UserId);

                builder.HasOne(u => u.SystemUser)
                       .WithOne(su => su.User)
                       .HasForeignKey<SystemUser>(su => su.UserId);
            }
        }
    }
}
