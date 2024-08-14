using Microsoft.AspNetCore.Mvc;
using WebApiProject.Entities;
using WebApiProject.Services;
using WebApiProject.Interface;
using WebApiProject.Models;
using Microsoft.Extensions.Options;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly SmtpSettings _smtpSettings;

        public MailController(IMailService mailService, 
            IOptions<SmtpSettings> smtpSettings)
        {
            _mailService = mailService;
            _smtpSettings = smtpSettings.Value;
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendEmailAsync([FromBody] MailRequest request)
        {
            RequestMail mail = new RequestMail()
            {
                Subject = "Test Konu",
                MailDisplayName = "Test Display",
                MailTemplateName = MailConsts.VirtualHRProfileUpdateMailTemplateName,
                Recipients = new Recipients()
                {
                    ListofEmailTo = new List<string>()
                    {
                        "burakemre2142@gmail.com"
                    }
                },
                ProfileUpdateDemandStructure = new ProfileUpdateDemandStructure()
                {
                    Introduction = "Merhaba Burak Bey,",
                    Conclusion = "İyi çalışmalar.",
                    MainContent = "Güncelleme talebi size ulaşmıştır.Lütfen kontrol ediniz.",
                    Footer = "Burak Emre A.Ş."
                },
                AttachmentList = new List<FormFile>()
                {
                }        
            };

            await _mailService.SendMailAsync(mail, new SmtpSettings()
            {
                Password = _smtpSettings.Password,
                Port = _smtpSettings.Port,
                Server = _smtpSettings.Server,
                Username = _smtpSettings.Username
            });

            return Ok(new
            {
                message = "Başarılı"
            });
        }
    }

    public class MailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

