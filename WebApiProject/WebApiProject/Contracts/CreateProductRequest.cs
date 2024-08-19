using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Contracts
{
    public class CreateProductRequest
    {            
        public string Name { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
        public string Category { get; set; }


        public int Price { get; set; }

        
    }
}

