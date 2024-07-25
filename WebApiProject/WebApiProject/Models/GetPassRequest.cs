using WebApiProject.Entities;

namespace WebApiProject.Models
{
    public class GetPassRequest
    {
        public User? User { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public Adress Adress { get; set; }

    }
}
