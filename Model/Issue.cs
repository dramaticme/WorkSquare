using worksquare.Enum;

namespace worksquare.Model
{
    public class Issue : BaseEntity
    {
        public string? IssueName { get; set; }
        public required string Description { get; set; }
        public required int ProjectId { get; set; }
        //Store ProjectID of the project to which the task belongs such that all details of the project can be accessed through Project table
        public required Project Project { get; set; }
        public required PriorityEnum Priority { get; set; }
        public required int AssignedTo { get; set; }
        //Store EmployeeID of the employee to whom the task is assigned such that all details of the employee can be accessed through Employee table

        public required DateTime DueDate { get; set; }
        //This feild is notin use for now but it can be used in future to set a deadline for the task

        public required WorkStatusEnum IssueStatus { get; set; }
        public bool IsBookmarked { get; set; } = false;
    }
}
