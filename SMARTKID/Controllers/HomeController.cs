using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMARTKID.App_Data;
using SMARTKID.Models;
using SMARTKID.Models.Entities;
using SMARTKID.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController
                                (
                                    ILogger<HomeController> logger,
                                    AppDbContext context
                                )
        {
            this._logger = logger;
            this._context = context;
        }

        [Route("~/")]
        //[Route("~/[action]")]
        //[Route("~/[controller]/[action]/[id]")]
        public IActionResult Index()
        {
            return View();
        }



        //////////////////////////////// <Contact> ////////////////////////////////////////
        [HttpGet]
        [Route("~/[action]")]
        //[Route("~/[controller]/[action]")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("~/[action]")]
        //[Route("~/[controller]/[action]")]
        public IActionResult Contact(HomeContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = new Contact()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Objective = model.Objective,
                    Message = model.Message
                };

                var result = this._context.Contacts.Add(contact);
                var count = this._context.SaveChanges();

                if (count != 1)
                {
                    ModelState.AddModelError(string.Empty, "The Message Is Not  Received!!..");
                    return View(model);
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
