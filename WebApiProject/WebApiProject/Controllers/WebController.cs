using Microsoft.AspNetCore.Mvc;
using WebApiProject.Context;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebController : ControllerBase
    {
        private readonly WebContext _webContext;

        public WebController(WebContext webContext)
        {
            _webContext = webContext;
        }
               //static void AddProduct()
               // {
               //     using (var db = new WebContext())

               //     {
               //         //  var products = new List<Product>()
               //        {
               //             // new Product{ Name = "samsung7", Price = 2000},
               //             //new Product{ Name = "samsung8", Price = 5000},
               //             //new Product{ Name = "samsung9", Price = 4000},
               //             //new Product{ Name = "samsung1", Price = 45000}
               //         };
               //         //foreach (var a in products) { db.Products.Add(a); }
               //         // db.Products.AddRange(products); // hepsini ekler RAnge bize bunu sağlar
               //         //var p = new Product { Name = "samsung", Price = 2000 };
               //         //db.Products.Add(p);
               //         db.SaveChanges();
               //         Console.WriteLine("Veriler yazıldı");
               //     }

               // }
    }
}