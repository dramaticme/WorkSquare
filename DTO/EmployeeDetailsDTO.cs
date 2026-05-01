

namespace worksquare.DTO
{
    public class EmployeeDetailsDTO
    {
        public required int EmployeeId { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodGroup { get; set; }
        public string? MaritalStatus { get; set; }
    }
}
