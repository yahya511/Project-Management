namespace AuthService.Models
{
    public class UserRoleSession
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }

        public ApplicationUser User { get; set; } // العلاقة مع المستخدم
    }
}
