using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    public class Issue : BaseEntity
    {
        public string? IssueName { get; set; }
        public required string Description { get; set; }
        public required int ProjectId { get; set; }
        //Store ProjectID of the project to which the issue belongs such that all details of the project can be accessed through Project table
        public required Project Project { get; set; }
        public required PriorityEnum Priority { get; set; }
        public required int AssignedTo { get; set; }
        public Employee AssignedEmployee { get; set; } = null!;
        //Store EmployeeID of the employee to whom the issue is assigned such that all details of the employee can be accessed through Employee table

        public required DateTime DueDate { get; set; }
        //This field is not in use for now but it can be used in future to set a deadline for the issue

        public required WorkStatusEnum IssueStatus { get; set; }
        public bool IsBookmarked { get; set; } = false;

        public class Configuration : IEntityTypeConfiguration<Issue>
        {
            public void Configure(EntityTypeBuilder<Issue> builder)
            {
                builder.HasKey(i => i.Id);

                builder.Property(i => i.IssueName).HasMaxLength(256);

                // Issue → Project (many-to-one)
                builder.HasOne(i => i.Project)
                       .WithMany(p => p.Issues)
                       .HasForeignKey(i => i.ProjectId)
                       .OnDelete(DeleteBehavior.Cascade);

                // AssignedTo → Employee (required FK)
                builder.HasOne(i => i.AssignedEmployee)
                       .WithMany()
                       .HasForeignKey(i => i.AssignedTo)
                       .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
