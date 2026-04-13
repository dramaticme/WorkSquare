namespace worksquare.Model
{
    public class EmployeeDetail: BaseEntity
    {
        public required int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodGroup { get; set; }
        public string? MaritalStatus { get; set; }
    }
}
