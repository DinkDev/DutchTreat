﻿using DutchTreat.Services;
using Microsoft.AspNetCore.Mvc;
using DutchTreat.ViewModels;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IEmailService _emailService;

        public AppController(IEmailService emailService)
        {
            _emailService = emailService;
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
    }
}
