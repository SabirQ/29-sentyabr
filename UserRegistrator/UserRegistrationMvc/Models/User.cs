namespace UserRegistrationMvc.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Role Role { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
