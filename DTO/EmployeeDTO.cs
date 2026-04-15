using worksquare.Enum;

namespace worksquare.DTO
{
    public class EmployeeDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required JobRoleEnum JobRole { get; set; }
        public int? ManagerId { get; set; }
        public required string Address { get; set; }
        public required string PinCode { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public required int CompanyId { get; set; }
    }
}
