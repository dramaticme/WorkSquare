using worksquare.Enum;

namespace worksquare.Model
{
    public class SystemUser : BaseEntity
    {
        public required int UserId { get; set; }
        public RoleEnum Role { get; set; }
    }
}
