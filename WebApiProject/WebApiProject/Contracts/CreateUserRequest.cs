using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Contracts
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string Adresses { get; set; }
    }
}
