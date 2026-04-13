

namespace worksquare.Model
{
    public class Company : BaseEntity
    {
        public required string LegalName { get; set; }
        public required string DisplayName { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required CompanyDetails CompanyDetails { get; set; }

        public required ICollection<Employee> Employees { get; set; }
        public required ICollection<Client> Clients { get; set; }
        public required ICollection<Project> Projects { get; set; }


    }
}
