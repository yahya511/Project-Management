namespace AuthService.DTOs
{
    public class RegistDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "Employee"; // الدور الافتراضي عند عدم تحديده
    }
}
