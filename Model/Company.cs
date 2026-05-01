using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace worksquare.Model
{
    public class Company : BaseEntity
    {
        public required string LegalName { get; set; }
        public required string DisplayName { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required CompanyDetails CompanyDetails { get; set; }

        public required ICollection<Employee> Employees { get; set; }
        public required ICollection<Client> Clients { get; set; }
        public required ICollection<Project> Projects { get; set; }

        public class Configuration : IEntityTypeConfiguration<Company>
        {
            public void Configure(EntityTypeBuilder<Company> builder)
            {
                builder.HasKey(c => c.Id);

                builder.HasOne(c => c.CompanyDetails)
                       .WithOne(cd => cd.Company)
                       .HasForeignKey<CompanyDetails>(cd => cd.CompanyId);
            }
        }
    }
}
