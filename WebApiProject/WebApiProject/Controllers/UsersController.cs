using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts;
using WebApiProject.Interface;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _Userservices;

        public UserController(IUserServices UserServices)
        {
            _Userservices = UserServices;
        }

        [HttpPost("CreateTodoAsync")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateTodoAsync(CreateUserRequest request)
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

                await _Userservices.UserCreateTodoAsync(request);

                return Ok(new { message = "Blog post successfully created" });


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });

            }


        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UserGetAllAsync()
        {
            try
            {
                var user = await _Userservices.UserGetAllAsync();
                if (user == null || !user.Any())
                {
                    return Ok(new { message = "No Todo Items  found" });
                }
                return Ok(new { message = "Successfully retrieved all blog posts", data = user });

            }
            /*Yapılacaklar Öğelerini Getirme : Veritabanından tüm Yapılacaklar öğelerini getirmek için arayüzden GetAllAsyncmetodu çağırıyoruz .ITodoServices

    Başarı Yanıtı : Yapılacaklar öğeleri başarıyla alınırsa, Okbir başarı mesajı ve Yapılacaklar öğelerinin listesiyle birlikte bir yanıt döndürürüz.

    Hata İşleme500 Internal Server Error : Alma işlemi sırasında bir hata oluşursa, bir hata mesajı içeren bir yanıt döndürürüz .*/
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all Tood it posts", error = ex.Message });


            }
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UserGetByIdAsync(long id)
        {
            try
            {
                var user = await _Userservices.UserGetByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = $"No Todo item with Id {id} found." });
                }
                return Ok(new { message = $"Successfully retrieved Todo item with Id {id}.", data = user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving the Todo item with Id {id}.", error = ex.Message });
            }
        }
        [HttpPut("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UserUpdateTodoAsync(long id, UpdateUserRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var user = await _Userservices.UserGetByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = $"Todo Item  with id {id} not found" });
                }

                await _Userservices.UserUpdateTodoAsync(id, request);
                return Ok(new { message = $" Todo Item  with id {id} successfully updated" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating blog post with id {id}", error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UserDeleteTodoAsync(long id)
        {
            try
            {
                await _Userservices.UserDeleteTodoAsync(id);
                return Ok(new { message = $"User  with id {id} successfully deleted" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting User Item  with id {id}", error = ex.Message });

            }
        }


    }
}
