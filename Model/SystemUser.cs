using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    public class SystemUser : BaseEntity
    {
        public required int UserId { get; set; }
        public required User User { get; set; }
        public SystemRoleEnum Role { get; set; }

        public class Configuration : IEntityTypeConfiguration<SystemUser>
        {
            public void Configure(EntityTypeBuilder<SystemUser> builder)
            {
                builder.HasKey(su => su.Id);

                // One user can only have one system-user record
                builder.HasIndex(su => su.UserId).IsUnique();
            }
        }
    }
}
