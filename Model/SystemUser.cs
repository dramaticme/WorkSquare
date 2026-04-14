using worksquare.Enum;

namespace worksquare.Model
{
    public class SystemUser : BaseEntity
    {
        public required int UserId { get; set; }
        public required User User { get; set; }
        public SystemRoleEnum Role { get; set; }
    }
}
