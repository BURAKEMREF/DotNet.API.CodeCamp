using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Entities
{
    public class User 
    {
        // [Required] zorunlu kılnasını sağlar.
        public long Id { get; set; }
        //[Required(ErrorMessage = "Name is required")]
        //[Display(Name = "Name")]
        public string? Name { get; set; }
        // [EmailAddress]
        //[Required(ErrorMessage = "E-mail is required")]
        //[Display(Name = "Email")]
        public string? Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        // özelliğinin en az 6 karakter ve en fazla 100 karakter uzunluğunda olmasını zorunlu kılar ve hata mesajını tanımlar.
        //[Display(Name = "Password")]
        public string? Password { get; set; }
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //  public string ConfirmPassword { get; set; }
        public string? UserName { get; set; }
        public string? Adresses { get; set; }

        public int? UserType { get; set; }
        public bool? IsActive { get; set; }

        //[DataType(DataType.Password)] bu anatosyon şifreyi giriş ekranında gizli olmasını sağlar.
        //[Display(Name = "Remember me?")] bu anastasyon giriş ekranında Remember me olarak gözükür kullancı isterse kendisini hatırlatır.


    } //public bool RememberMe { get; set; }

}
