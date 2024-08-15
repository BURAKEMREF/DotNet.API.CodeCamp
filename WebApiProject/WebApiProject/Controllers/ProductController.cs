using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts;
using WebApiProject.Entities;
using WebApiProject.Interface;
using WebApiProject.Services;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _Productservices;
        public ProductController(IProductServices ProductServices)
        {
            _Productservices = ProductServices;
        }
        [HttpPost("CreateTodoAsync")]
        //[Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> CreateTodoAsync(CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            /*Model Doğrulaması : İstek modelinin geçerli olup olmadığını kullanarak kontrol ederiz ModelState.IsValid. Model geçerli değilse, BadRequestmodel durum hatalarıyla bir yanıt döndürürüz.
    Yapılacaklar Öğesi Oluşturma : Veritabanında yeni bir Yapılacaklar öğesi oluşturmak için arayüzden CreateTodoAsyncmetodu çağırıyoruz .ITodoServices
    Başarı Yanıtı : Yapılacaklar öğesi başarıyla oluşturulursa, Okbaşarı mesajını içeren bir yanıt döndürürüz.
    Hata İşleme500 Internal Server Error : Oluşturma işlemi sırasında bir hata oluşursa, hata mesajı içeren bir yanıt döndürüyoruz .*/
            try
            {
                await _Productservices.ProductCreateTodoAsync(request);
                return Ok(new { message = "Blog post successfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });
            }
        }
        [HttpGet("ProductGetAllAsyn")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ProductGetAllAsync()
        {
            try
            {
                var product = await _Productservices.ProductGetAllAsync();
                if (product == null || !product.Any())
                {
                    return Ok(new { message = "No Todo Items  found" });
                }
                return Ok(new { message = "Successfully retrieved all blog posts", data = product });
            }
            /*Yapılacaklar Öğelerini Getirme : Veritabanından tüm Yapılacaklar öğelerini getirmek için arayüzden GetAllAsyncmetodu çağırıyoruz .ITodoServices

    Başarı Yanıtı : Yapılacaklar öğeleri başarıyla alınırsa, Okbir başarı mesajı ve Yapılacaklar öğelerinin listesiyle birlikte bir yanıt döndürürüz.

    Hata İşleme500 Internal Server Error : Alma işlemi sırasında bir hata oluşursa, bir hata mesajı içeren bir yanıt döndürürüz .*/
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all Tood it posts", error = ex.Message });
            }
        }
        /*Bu yöntem, GetByIdAsyncarayüzden ITodoServicesbir Todo öğesini kendi yoluyla almak için yöntemi çağırır Id.
         * Bir Todo öğesi başarıyla alınırsa, bir başarı mesajı ve Todo öğesiyle bir yanıt döndürür .
         * Alma işlemi sırasında bir hata oluşursa, bir hata mesajıyla Okbir yanıt döndürür .500 Internal Server Error*/
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ProductGetByIdAsync(int id)
        {
            try
           {
                var product = await _Productservices.ProductGetByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new { message = $"No Todo item with Id {id} found." });
                }
                return Ok(new { message = $"Successfully retrieved Todo item with Id {id}.", data = product });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving the Todo item with Id {id}.", error = ex.Message });
            }
        }
        /*Model Doğrulaması : İstek modelinin geçerli olup olmadığını kullanarak kontrol ederiz ModelState.IsValid.
         * Model geçerli değilse, BadRequestmodel durum hatalarıyla bir yanıt döndürürüz.
    Belirli Bir Yapılacaklar Öğesini Getirme : Bir Yapılacaklar öğesini kendi başına getirmek için arayüzden GetByIdAsyncmetodu çağırırız .ITodoServicesId
    Yapılacaklar Öğesini Güncelleme : Yapılacaklar öğesi bulunursa, UpdateTodoAsyncarayüzden ITodoServicesYapılacaklar öğesini güncellemek için metodu çağırırız.
    Başarılı Yanıt : Yapılacaklar öğesi başarıyla güncellenirse, Okbaşarı mesajını içeren bir yanıt döndürürüz.
    Hata İşleme500 Internal Server Error : Güncelleme işlemi sırasında bir hata oluşursa, hata mesajı içeren bir yanıt döndürüyoruz*/
        [HttpPut("UpdateTodoAsync")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateTodoAsync(UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var product = await _Productservices.ProductGetByIdAsync(request.ProductId);
                if (product == null)
                {
                    return NotFound(new { message = $"Todo Item  with id {request.ProductId} not found" });
                }

                await _Productservices.ProductUpdateTodoAsync(request.ProductId, request);
                return Ok(new { message = $" Todo Item  with id {request.ProductId} successfully updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating blog post with id {request.ProductId}", error = ex.Message });
            }
        }
        [HttpDelete("ProductDeleteTodoAsync")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ProductDeleteTodoAsync(int id)
        {

            try
            {
                await _Productservices.ProductDeleteTodoAsync(id);
                return Ok(new { message = $"Product  with id {id} successfully deleted" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting Prodcut Item  with id {id}", error = ex.Message });

            }
        }


    }
}
