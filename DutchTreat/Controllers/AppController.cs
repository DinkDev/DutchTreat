namespace DutchTreat.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Data;
    using Services;
    using ViewModels;

    public class AppController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IDutchRepository _repository;

        public AppController(IEmailService emailService, IDutchRepository repository)
        {
            _emailService = emailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // send the email
                _emailService.SendEmail("dinkhome@hotmail.com", @"DutchTreat-" + model.Subject,
                    $"From: {model.Name} - {model.Email}, Message: {model.Message}");

                ViewBag.UserMessage = "EMail Sent";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }

        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();

            return View(results);
        }
    }
}
