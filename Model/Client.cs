using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    public class Client : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required ClientTypeEnum ClientType { get; set; }
        public required string ContactPersonName { get; set; }
        public required string ContactEmail { get; set; }
        public required string ContactPhone { get; set; }
        public string? Address { get; set; }
        public required string Pincode { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }

        public required StatusEnum Status { get; set; }

        public required int CompanyId { get; set; }
        public required Company Company { get; set; }

        public ICollection<Project>? Projects { get; set; }

        public class Configuration : IEntityTypeConfiguration<Client>
        {
            public void Configure(EntityTypeBuilder<Client> builder)
            {
                builder.HasKey(c => c.Id);

                builder.Property(c => c.ContactEmail).HasMaxLength(256);
                builder.Property(c => c.ContactPhone).HasMaxLength(20);

                builder.HasOne(c => c.Company)
                       .WithMany(co => co.Clients)
                       .HasForeignKey(c => c.CompanyId)
                       .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}
