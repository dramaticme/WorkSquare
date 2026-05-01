using worksquare.Enum;
using worksquare.Model;

namespace worksquare.DTO
{
    public class IssueDTO
    {
        public string? IssueName { get; set; }
        public required string Description { get; set; }
        public required int ProjectId { get; set; }
        //Store ProjectID of the project to which the issue belongs such that all details of the project can be accessed through Project table
        public required PriorityEnum Priority { get; set; }
        public required int AssignedTo { get; set; }
        //Store EmployeeID of the employee to whom the issue is assigned such that all details of the employee can be accessed through Employee table

        public required DateTime DueDate { get; set; }
        //This field is not in use for now but it can be used in future to set a deadline for the issue

        public required WorkStatusEnum IssueStatus { get; set; }
        public bool IsBookmarked { get; set; } = false;
    }
}
