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
              
    }
}