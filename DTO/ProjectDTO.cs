using worksquare.Enum;
using worksquare.Model;

namespace worksquare.DTO
{
    public class ProjectDTO
    {
        public required string ProjectName { get; set; }
        public string? Description { get; set; }
        public required DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required int ProjectManagerId { get; set; }
        //Store EmployeeID of the project manager such that all details of the manager can be accessed through Employee table

        public int? AssistantProjectManagerId { get; set; }
        //Store EmployeeID of the assistant project manager such that all details of the assistant manager can be accessed through Employee table
        public required StatusEnum Status { get; set; }
        public required int CompanyId { get; set; }
        public required int ClientId { get; set; }
    }
}
