
using worksquare.Enum;

namespace worksquare.Model
{
    public class User : BaseEntity
    {
        public required string Username { get; set; }
        public required string Password { get; set; }

        public bool IsActive { get; set; } = true;
        public CompanyUser? CompanyUser { get; set; }
        public SystemUser? SystemUser { get; set; }
    }
}
