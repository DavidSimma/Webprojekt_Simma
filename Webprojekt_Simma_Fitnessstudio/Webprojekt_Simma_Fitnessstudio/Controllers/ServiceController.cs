using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Webprojekt_Simma_Fitnessstudio.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Map()
        {
            return View();
        }
        public IActionResult Prices()
        {
            return View();
        }
    }
}
