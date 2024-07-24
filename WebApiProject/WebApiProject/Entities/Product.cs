using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Entities
{
    public class Product
    {
        //primary key (Id,<type_name>Id)
        public int? ProductId { get; set; }

        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }

        public decimal? Price { get; set; }
        
        public int? CategoryId { get; set; }
    }
}
