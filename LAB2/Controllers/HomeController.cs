using LAB2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LAB2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailService _emailService;

        public HomeController(ILogger<HomeController> logger, EmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

       
        public IActionResult Index()
        {
            return View();
        }

       

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ContactForm", model);
            }

            await _emailService.SendEmailAsync(model.To, model.Subject, model.Body);
            ViewBag.Message = "Email sent successfully!";
            return View("ContactForm");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
