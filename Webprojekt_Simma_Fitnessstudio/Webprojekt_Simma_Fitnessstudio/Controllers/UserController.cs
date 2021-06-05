using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webprojekt_Simma_Fitnessstudio.Models;
using Webprojekt_Simma_Fitnessstudio.Models.DB;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Webprojekt_Simma_Fitnessstudio.Controllers
{
    public class UserController : Controller
    {
        private IRepositoryUsers rep = new RepositoryUserDB();
        User u = null;
        public IActionResult Index()
        {
            try
            {
                rep.Open();
                return View();
            }
            catch (Exception ex)
            {
                return View("Message", new Message("Datenbankfehler", ex.Message));
            }
            finally
            {
                rep.Close();
            }
        }

        public JsonResult GetUser()
        {
            try
            {
                //rep = new RepositoryUserDB();
                //rep.Open();
                //HttpContext.Session.GetObjectFromJson<User>("username");

                Console.WriteLine("user:    " + HttpContext.Session.GetString("username"));
                //Console.WriteLine("user:    " + rep.getUserByUsername("admin"));
                if(HttpContext.Session.GetString("username") != null)
                {
                    return Json(HttpContext.Session.GetString("username"));
                }
                else
                {
                    return Json("nicht angemeldet");
                }

            }
            catch (Exception)
            {
                return Json("Error");
            }
            /*finally
            {
                rep.Close();
            }
            */
        }


        [HttpGet]
        public IActionResult Registrieren()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Registrieren(User newUser)
        {
            if (newUser == null)
            {
                return RedirectToAction("Registration");
            }
            ValidateUserData(newUser);
            if (ModelState.IsValid)
            {
                try
                {
                    rep.Open();
                    if (rep.Insert(newUser))
                    {
                        u = newUser;
                        return View("Message", new Message("Datenbank-Erfolg", "Der Artikel wurde erfolgreich abgespeichert"));
                    }
                }
                catch (DbException)
                {
                    return View("Message", new Message("Datenbank-Fehler", "Der Benutzer konnte nicht abgespeichert werden", "Probieren sie es bitte später erneut"));
                }
                finally
                {
                    rep.Close();
                }
            }

            return View(newUser);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new User());
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            ValidateLoginData(user);
            if (ModelState.IsValid)
            {
                try
                {
                    rep.Open();
                    if (rep.Login(user.UserName, user.Password)) 
                    {
                        HttpContext.Session.SetString("username", user.UserName);
                        User u = rep.getUserByUsername(user.UserName);
                        HttpContext.Session.SetObjectAsJson("username", u);
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Login");

                }
                catch (DbException)
                {
                    return View("Message", new Message("Datenbank-Fehler", "Der User konnte nicht erkannt werden", "Probieren sie es bitte später erneut"));
                }
                finally
                {
                    rep.Close();
                }
            }
            return View(user);
        }

        public IActionResult Delete(string username)
        {
            try
            {
                rep.Open();
                rep.Delete(username);
                HttpContext.Session.Clear();
                return RedirectToAction("Index");

            }
            catch (DbException)
            {
                return View("Message", new Message("Datenbank-Fehler", "Der User konnte nicht erkannt werden", "Probieren sie es bitte später erneut"));
            }
            finally
            {
                rep.Close();
            }

            return View();
        }

        public IActionResult Abmelden()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        private void ValidateUserData(User u)
        {
            if (u == null)
            {
                return;
            }
            if (u.UserName == null || u.UserName.Length < 4)
            {
                ModelState.AddModelError(nameof(Models.User.UserName), "Der Benutzername muss mind. 4 Zeichen lang sein!");
            }
            if (u.Password == null || u.Password.Length < 4)
            {
                ModelState.AddModelError(nameof(Models.User.Password), "Das Passwort muss mind. 4 Zeichen lang sein!");
            }
            if (u.Firstname == null)
            {
                ModelState.AddModelError(nameof(Models.User.Firstname), "Bitte geben Sie einen Vornamen an!");
            }
            if (u.Lastname == null)
            {
                ModelState.AddModelError(nameof(Models.User.Lastname), "Bitte geben Sie einen Nachnamen an!");
            }
            if(u.Age < new DateTime(1900, 1, 1))
            {
                ModelState.AddModelError(nameof(Models.User.Age), "Bitte geben Sie ein vernünftiges Alter an!");
            }
            if (u.Age < new DateTime(DateTime.Now.Year-18, DateTime.Now.Month, DateTime.Now.Day))
            {
                ModelState.AddModelError(nameof(Models.User.Age), "Sie sind zu jung!");
            }

        }
        private void ValidateLoginData(User u)
        {

            if (u == null)
            {
                return;
            }
            if (u.UserName == null || u.UserName.Length < 4)
            {
                ModelState.AddModelError(nameof(Models.User.UserName), "Der Benutzername muss mind. 4 Zeichen lang sein!");
            }
            if (u.Password == null || u.Password.Length < 4)
            {
                ModelState.AddModelError(nameof(Models.User.Password), "Das Passwort muss mind. 4 Zeichen lang sein!");
            }
        }
            
    }
}
