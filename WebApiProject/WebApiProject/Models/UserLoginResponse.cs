namespace WebApiProject.Models
{
    public class UserLoginResponse
    {
        public string Username { get; set; }
        public bool AuthenticateResult { get; set; }
        public string? AuthToken { get; set; }
        public DateTime AccessTokenExpireDate { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get;  set; }

        public string? Password { get;  set; }
        public string? Email { get; set; }
    }
}
