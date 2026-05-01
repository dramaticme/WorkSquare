using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    public class Employee : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required JobRoleEnum JobRole { get; set; }
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public required string Address { get; set; }
        public required string PinCode { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }

        public required EmployeeDetail EmployeeDetails { get; set; }

        public required int CompanyId { get; set; }
        public required Company Company { get; set; }

        public ICollection<Project>? Projects { get; set; }

        public class Configuration : IEntityTypeConfiguration<Employee>
        {
            public void Configure(EntityTypeBuilder<Employee> builder)
            {
                builder.HasKey(e => e.Id);

                // Employee → Company (many-to-one)
                builder.HasOne(e => e.Company)
                       .WithMany(c => c.Employees)
                       .HasForeignKey(e => e.CompanyId)
                       .OnDelete(DeleteBehavior.Restrict);

                // Employee → EmployeeDetail (one-to-one)
                builder.HasOne(e => e.EmployeeDetails)
                       .WithOne(ed => ed.Employee)
                       .HasForeignKey<EmployeeDetail>(ed => ed.EmployeeId)
                       .OnDelete(DeleteBehavior.Cascade);

                // Employee ↔ Project (many-to-many via implicit join table)
                builder.HasMany(e => e.Projects)
                       .WithMany(p => p.Employees)
                       .UsingEntity("ProjectEmployee");

                // ManagerId → Employee (self-referential, nullable — top-level employees have no manager)
                builder.HasOne(e => e.Manager)
                       .WithMany()
                       .HasForeignKey(e => e.ManagerId)
                       .IsRequired(false)
                       .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
