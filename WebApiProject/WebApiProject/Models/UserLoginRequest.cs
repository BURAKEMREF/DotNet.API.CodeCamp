namespace WebApiProject.Models
{
    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string?   Email { get; internal set; }
        public int? UserType { get; set; }
        
    }
}
