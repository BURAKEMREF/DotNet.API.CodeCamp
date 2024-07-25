using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Contracts
{
    public class CreateProductRequest
    {
        [Required]
      
        public int ProductId { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int CategoryId { get; set; }
    }
}

