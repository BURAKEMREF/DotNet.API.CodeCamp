using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using WebApiProject.Entities;
using WebApiProject.Services;




namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly MailService _mailService;

        public MailController(MailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost("send")]
        public IActionResult SendEmail([FromBody] MailRequest request)
        {
            _mailService.SendEmailAsync(request.To, request.Subject, request.Body);
            return Ok();
        }
    }

    public class MailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

