using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
using CityInfo.API.ViewModels;
using CityInfo.API.Services;

namespace CityInfo.API.Controllers
{
    public class AppController: Controller
    {
        private readonly IMailService _mailService;
        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact US";
            
            //throw new InvalidOperationException("Bad thing mis coming");
            return View();
        }
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (string.IsNullOrEmpty(model.Member1) && string.IsNullOrEmpty(model.Member2))
            {
                var validationMessage = "Please provide at least one member.";
                ModelState.AddModelError("Member1", validationMessage);
                ModelState.AddModelError("Member2", validationMessage);                
            }
            if (model.Member1 == model.Member2)
            {
                var validationMessage = "Members can't be the same person";
                ModelState.AddModelError("Member1", validationMessage);
                ModelState.AddModelError("Member2", validationMessage);
            }



            if (model.Reason=="Other" && string.IsNullOrEmpty(model.Comment))
            {
                var validationMessage = "Please provide a comment reason 'Other'";
                ModelState.AddModelError("Comment", validationMessage);               

            }

            if (ModelState.IsValid)
            {
                //send email
                _mailService.Send("ddd@ddd.com", model.Reason, $"From: {model.Boss} - {model.Email}, Message: {model.Comment}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About US";
            return View();
        }
    }
}
