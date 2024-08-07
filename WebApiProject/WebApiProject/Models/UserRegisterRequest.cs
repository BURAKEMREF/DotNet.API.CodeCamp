using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models
{
    public class UserRegisterRequest
    {
        //[Required(ErrorMessage = "First Name is required")]
        //[Display(Name = "First Name")]
        public string Username { get; set; }
       // [Required]
       // [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        //[Required]
        //[EmailAddress]
        public string? Email { get; set; }
        // [DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }
        public int? UserType { get; set; }
    }
}
