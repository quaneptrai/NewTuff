using Aris3._0.Application.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aris3._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public GmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }
        [HttpPost("{email}")]
        public IActionResult SendEmail(string email)
        {
            var msg = emailService.SenEmail(email);
            return Ok(new
            {
                Message = msg,
            });
        }
    }
}
