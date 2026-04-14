using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worksquare.Enum;

namespace worksquare.Model
{
    public class Ticket : BaseEntity
    {
        public string? TicketName { get; set; }
        public required string Description { get; set; }
        public required int ProjectId { get; set; }
        //Store ProjectID of the project to which the ticket belongs such that all details of the project can be accessed through Project table
        public required Project Project { get; set; }
        public required PriorityEnum Priority { get; set; }
        public required int AssignedTo { get; set; }
        public Employee AssignedEmployee { get; set; } = null!;
        //Store EmployeeID of the employee to whom the ticket is assigned such that all details of the employee can be accessed through Employee table

        public required DateTime DueDate { get; set; }
        //This field is not in use for now but it can be used in future to set a deadline for the ticket

        public required WorkStatusEnum TicketStatus { get; set; }
        public bool IsBookmarked { get; set; } = false;

        public class Configuration : IEntityTypeConfiguration<Ticket>
        {
            public void Configure(EntityTypeBuilder<Ticket> builder)
            {
                builder.HasKey(t => t.Id);

                builder.Property(t => t.TicketName).HasMaxLength(256);

                // Ticket → Project (many-to-one)
                builder.HasOne(t => t.Project)
                       .WithMany(p => p.Tickets)
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
