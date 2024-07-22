using Microsoft.AspNetCore.Mvc;
using WebApiProject.Context;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly WebContext _webContext;

        public ProductsController(
            WebContext webContext)
        {
            _webContext = webContext;
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            var p = _webContext.Products.FirstOrDefault(p => p.ProductId == id);

            if (p != null)
            {
                _webContext.Remove(p);
                _webContext.SaveChanges();

                return new OkObjectResult("Silme işlemi başarılı");
            }

            return new BadRequestObjectResult("Böyle bir kayıt bulunamadı!");
        }

        [HttpPost("UpdateProduct")]
        public void UpdateProduct([FromQuery]int id)
        {
            var p = _webContext.Products.Where(i => i.ProductId == 1).FirstOrDefault();
            if (p != null)
            {
                p.Price *= 1.2m;
                _webContext.SaveChanges();
            }
        }

        [HttpGet("GetProductById")]
        public void GetProductById([FromQuery]int id)
        {
            //var result = _webContext.Products.Where(p => p.Id == id);
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = _webContext.Products.ToList();
            return Ok(products);
        }
    }
}