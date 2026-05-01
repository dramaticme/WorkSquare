using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    public class Project : BaseEntity
    {
        public required string ProjectName { get; set; }
        public string? Description { get; set; }
        public required DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required int ProjectManagerId { get; set; }
        public Employee ProjectManager { get; set; } = null!;
        //Store EmployeeID of the project manager such that all details of the manager can be accessed through Employee table

        public int? AssistantProjectManagerId { get; set; }
        public Employee? AssistantProjectManager { get; set; }
        //Store EmployeeID of the assistant project manager such that all details of the assistant manager can be accessed through Employee table
        public required StatusEnum Status { get; set; }
        public required int CompanyId { get; set; }
        public required Company Company { get; set; }
        public required int ClientId { get; set; }
        public required Client Client { get; set; }

        public ICollection<Employee>? Employees { get; set; }

        public ICollection<Task>? Tasks { get; set; }
        public ICollection<Issue>? Issues { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

        public class Configuration : IEntityTypeConfiguration<Project>
        {
            public void Configure(EntityTypeBuilder<Project> builder)
            {
                builder.HasKey(p => p.Id);

                builder.Property(p => p.ProjectName).HasMaxLength(256).IsRequired();

                // Project → Company (many-to-one)
                builder.HasOne(p => p.Company)
                       .WithMany(c => c.Projects)
                       .HasForeignKey(p => p.CompanyId)
                       .OnDelete(DeleteBehavior.Restrict);

                // Project → Client (many-to-one)
                builder.HasOne(p => p.Client)
                       .WithMany(c => c.Projects)
                       .HasForeignKey(p => p.ClientId)
                       .OnDelete(DeleteBehavior.Restrict);

                // ProjectManagerId → Employee (required)
                builder.HasOne(p => p.ProjectManager)
                       .WithMany()
                       .HasForeignKey(p => p.ProjectManagerId)
                       .OnDelete(DeleteBehavior.Restrict);

                // AssistantProjectManagerId → Employee (optional)
                builder.HasOne(p => p.AssistantProjectManager)
                       .WithMany()
                       .HasForeignKey(p => p.AssistantProjectManagerId)
                       .IsRequired(false)
                       .OnDelete(DeleteBehavior.Restrict);

                // Project ↔ Employee M2M is configured in Employee.Configuration.
            }
        }
    }
}
