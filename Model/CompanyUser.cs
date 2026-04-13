using worksquare.Enum;

namespace worksquare.Model
{
    public class CompanyUser : BaseEntity
    {        
        public required int UserId { get; set; }
        public required int CompanyId { get; set; }
        public RoleEnum Role { get; set; }
        public required int ManagerId { get; set; }

    }
}
