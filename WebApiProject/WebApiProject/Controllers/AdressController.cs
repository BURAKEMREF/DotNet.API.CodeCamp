using Microsoft.AspNetCore.Mvc;
using WebApiProject.Interface;

using WebApiProject.Contracts;
using WebApiProject.Entities;
namespace WebApiProject.Controllers
 
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdressController : ControllerBase
    {
        private readonly IAdressServices _adressServices;

        public AdressController(IAdressServices adressServices)
        {
            _adressServices = adressServices;
       }
        [HttpPost]
        public async Task<IActionResult> AdressCreateAsync(CreateAdressRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _adressServices.AdressCreateAsync(request);
                return Ok(new { message = "Blog post successfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> AdressGetAllAsync()
        {
            try
            {
                var adress = await _adressServices.AdressGetAllAsync();
                if (adress == null || !adress.Any())
                {
                    return Ok(new { message = "No Adress Items  found" });
                }
                return Ok(new { message = "Successfully retrieved all blog posts", data = adress });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all Tood it posts", error = ex.Message });


            }
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> AdressGetByIdAsync(Guid id)
        {
            try
            {
                var adress = await _adressServices.AdressGetByIdAsync(id);
                if (adress == null)
                {
                    return NotFound(new { message = $"No Adress item with Id {id} found." });
                }
                return Ok(new { message = $"Successfully retrieved Adress item with Id {id}.", data = adress });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving the Adress item with Id {id}.", error = ex.Message });
            }
        }
        [HttpPut("{id:guid}")]

        public async Task<IActionResult> AdressUpdateAsync(Guid id, UpdateAdressRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var adress = await _adressServices.AdressGetByIdAsync(id);
                if (adress == null)
                {
                    return NotFound(new { message = $"Adress Item  with id {id} not found" });
                }

                await _adressServices.AdressUpdateAsync(id, request);
                return Ok(new { message = $" Adress Item  with id {id} successfully updated" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating blog post with id {id}", error = ex.Message });


            }


        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> AdressDeleteAsync(Guid id)
        {
            try
            {
                await _adressServices.AdressDeleteAsync(id);
                return Ok(new { message = $"Adress  with id {id} successfully deleted" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting Adress Item  with id {id}", error = ex.Message });

            }
        }
    }
}
