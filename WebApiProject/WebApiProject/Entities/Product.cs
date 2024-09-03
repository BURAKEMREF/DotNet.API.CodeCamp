using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Entities
{
    public class Product
    {
        //primary key (Id,<type_name>Id)
        [Key]
        public int? ProductId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }
        public string? Category { get; set; }

        public int? Price { get; set; }
        

       // public int? CategoryId { get; set; }
    }
}
