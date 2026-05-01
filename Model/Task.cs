using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    public class Task : BaseEntity
    {
        public string? TaskName { get; set; }
        public required string Description { get; set; }
        public required int ProjectId { get; set; }
        //Store ProjectID of the project to which the task belongs such that all details of the project can be accessed through Project table
        public required Project Project { get; set; }
        public required PriorityEnum Priority { get; set; }
        public required int AssignedTo { get; set; }
        public Employee AssignedEmployee { get; set; } = null!;
        //Store EmployeeID of the employee to whom the task is assigned such that all details of the employee can be accessed through Employee table

        public required DateTime DueDate { get; set; }
        //This field is not in use for now but it can be used in future to set a deadline for the task

        public required WorkStatusEnum TaskStatus { get; set; }

        public bool IsBookmarked { get; set; } = false;

        public class Configuration : IEntityTypeConfiguration<Task>
        {
            public void Configure(EntityTypeBuilder<Task> builder)
            {
                builder.HasKey(t => t.Id);

                builder.Property(t => t.TaskName).HasMaxLength(256);

                // Task → Project (many-to-one)
                builder.HasOne(t => t.Project)
                       .WithMany(p => p.Tasks)
                       .HasForeignKey(t => t.ProjectId)
                       .OnDelete(DeleteBehavior.Cascade);

                // AssignedTo → Employee (required FK)
                builder.HasOne(t => t.AssignedEmployee)
                       .WithMany()
                       .HasForeignKey(t => t.AssignedTo)
                       .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
