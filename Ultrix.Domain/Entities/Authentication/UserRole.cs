namespace Ultrix.Domain.Entities.Authentication
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Role Role { get; set; }
    }
}
