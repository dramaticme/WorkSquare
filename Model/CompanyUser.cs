using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    public class CompanyUser : BaseEntity
    {
        public required int UserId { get; set; }
        public required User User { get; set; }

        public required int CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public CompanyRoleEnum Role { get; set; }

        public int? ManagerId { get; set; }
        public CompanyUser? Manager { get; set; }

        public class Configuration : IEntityTypeConfiguration<CompanyUser>
        {
            public void Configure(EntityTypeBuilder<CompanyUser> builder)
            {
                builder.HasKey(cu => cu.Id);

                // One user can only belong to one company record
                builder.HasIndex(cu => cu.UserId).IsUnique();

                // CompanyId → Company (many CompanyUsers belong to one Company)
                builder.HasOne(cu => cu.Company)
                       .WithMany()
                       .HasForeignKey(cu => cu.CompanyId)
                       .OnDelete(DeleteBehavior.Restrict);

                // ManagerId → CompanyUser (self-referential, nullable — top-level admin has no manager)
                builder.HasOne(cu => cu.Manager)
                       .WithMany()
                       .HasForeignKey(cu => cu.ManagerId)
                       .IsRequired(false)
                       .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
