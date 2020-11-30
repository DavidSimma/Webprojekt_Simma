using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webprojekt_Simma_Fitnessstudio.Models;

namespace Webprojekt_Simma_Fitnessstudio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Datenschutz()
        {
            return View();
        }
    }
}
