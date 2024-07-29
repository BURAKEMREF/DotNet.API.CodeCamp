namespace WebApiProject.Models
{
    public class UserRegisterResponse
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string? Email { get; set; }
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
