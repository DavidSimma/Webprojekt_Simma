using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webprojekt_Simma_Fitnessstudio.Models;

namespace Webprojekt_Simma_Fitnessstudio.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registrieren()
        {
            return View();
        }

        public IActionResult createNewArticle(User newUser)
        {
            if (newUser == null)
            {
                return RedirectToAction("Registration");
            }
            ValidateArticleData(newUser);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "User");
            }
            return View(newUser);
        }

        private void ValidateArticleData(User u)
        {
            if (u == null)
            {
                return;
            }
            if (u.UserName == null || u.UserName.Length < 4)
            {
                ModelState.AddModelError(nameof(u.UserName), "Der Benutzername muss mind. 4 Zeichen lang sein!");
            }
            if (u.Password == null || u.Password.Length < 4)
            {
                ModelState.AddModelError(nameof(u.Password), "Das Passwort muss mind. 4 Zeichen lang sein!");
            }
            if (u.Firstname == null)
            {
                ModelState.AddModelError(nameof(u.Firstname), "Bitte geben Sie einen Vornamen an!");
            }
            if (u.Lastname == null)
            {
                ModelState.AddModelError(nameof(u.Lastname), "Bitte geben Sie einen Nachnamen an!");
            }
            if(u.Age < new DateTime(1900, 1, 1))
            {
                ModelState.AddModelError(nameof(u.Age), "Bitte geben Sie ein vernünftiges Alter an!");
            }
            if (u.Age < new DateTime(DateTime.Now.Year-18, DateTime.Now.Month, DateTime.Now.Day))
            {
                ModelState.AddModelError(nameof(u.Age), "Sie sind zu jung!");
            }

        }
    }
}
