using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    public class CompanyDetails : BaseEntity
    {
        public string? TaxIdentificationNumber { get; set; }
        public string? RegistrationNumber { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public string? Industry { get; set; }
        public int CompanyId { get; set; }
        public required Company Company { get; set; }
        public CompanySizeEnum CompanySize { get; set; }

        //below fields are for future use
        public string? Website { get; set; }
        public string? Description { get; set; }
        public string? ServicesOffered { get; set; }

        public class Configuration : IEntityTypeConfiguration<CompanyDetails>
        {
            public void Configure(EntityTypeBuilder<CompanyDetails> builder)
            {
                builder.HasKey(cd => cd.Id);

                builder.Property(cd => cd.TaxIdentificationNumber).HasMaxLength(50);
                builder.Property(cd => cd.RegistrationNumber).HasMaxLength(50);
                builder.Property(cd => cd.Industry).HasMaxLength(100);
                builder.Property(cd => cd.Website).HasMaxLength(256);

                // Inverse side of Company → CompanyDetails (1-to-1) already
                // configured in Company.Configuration; no duplicate HasOne needed.
            }
        }
    }
}
