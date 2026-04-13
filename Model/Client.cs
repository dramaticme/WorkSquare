using worksquare.Enum;

namespace worksquare.Model
{
    public class Client : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required ClientTypeEnum ClientType { get; set; }
        public required string ContactPersonName { get; set; }
        public required string ContactEmail { get; set; }
        public required string ContactPhone { get; set; }
        public string? Address { get; set; }
        public required string Pincode { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }

        public required StatusEnum Status { get; set; }

        public required int CompanyId { get; set; }
        public required Company Company { get; set; }

        public ICollection<Project>? Projects { get; set; }

    }
}
