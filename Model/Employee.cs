using worksquare.Enum;

namespace worksquare.Model
{
    public class Employee : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required JobRoleEnum JobRole { get; set; }
        public required int ManagerId { get; set; }
        public required string Address { get; set; }
        public required string PinCode { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }

        public required EmployeeDetail EmployeeDetails { get; set; }

        public required int CompanyId { get; set; }
        public required Company Company { get; set; }

        public ICollection<Project>? Projects { get; set; }





    }
}
