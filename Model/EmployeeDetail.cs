using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace worksquare.Model
{
    public class EmployeeDetail : BaseEntity
    {
        public required int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodGroup { get; set; }
        public string? MaritalStatus { get; set; }

        public class Configuration : IEntityTypeConfiguration<EmployeeDetail>
        {
            public void Configure(EntityTypeBuilder<EmployeeDetail> builder)
            {
                builder.HasKey(ed => ed.Id);

                builder.Property(ed => ed.EmergencyContactPhone).HasMaxLength(20);
                builder.Property(ed => ed.BloodGroup).HasMaxLength(5);
                builder.Property(ed => ed.MaritalStatus).HasMaxLength(20);

                // The HasOne/WithOne/HasForeignKey relationship is configured
                // in Employee.Configuration to keep a single source of truth.
            }
        }
    }
}
