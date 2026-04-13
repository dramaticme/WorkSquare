
using worksquare.Enum;

namespace worksquare.Model
{
    public class User : BaseEntity
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
