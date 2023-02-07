using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using MixTape.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MixTape.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult signup()
        {
            return View();
        }

        [HttpPost]
        public ViewResult signup(User user)
        {
            MixTapeContext tables = new MixTapeContext();
            tables.Users.Add(user);
            tables.SaveChanges();

            return View("login", ViewBag.Message = string.Format("Logged in Successfully, Login Now!"));
        }

        [HttpGet]
        public ViewResult login() {
            return View();
        }
        [HttpPost]
        public IActionResult login(User user) 
        {

            var msg = String.Empty;
            var form_email = user.Email;
            var form_password = user.Password;
            MixTapeContext tables = new MixTapeContext();
            var fetched_user = tables.Users.Where(u => u.Email == form_email && u.Password == form_password);
            if (fetched_user.Count() > 0)
            {
                // when user gets logged in we set his email address in the cookie
                // so that we can remove it when user logs out
                // First Request: user sends request to server and server checks if it has cookie
                if (HttpContext.Request.Cookies.ContainsKey("user_email"))
                {
                    //
                }
                else
                {
                    HttpContext.Response.Cookies.Append("user_email", form_email);
                }
                return Redirect("/Home/UserHome");
            }
            return View("login", ViewBag.Message = string.Format("Invalid Credentials"));
        }

        public IActionResult albums()
        {
            return View();
        }


        [HttpGet]
        public IActionResult logout() {
            HttpContext.Response.Cookies.Delete("user_email");
            return Redirect("/Home");
        }


        [HttpGet]
        public IActionResult UserHome()
        {
            if (HttpContext.Request.Cookies.ContainsKey("user_email")) // check if user is signed up the only show Home
            {
                return View();
            }
            return View("login", ViewBag.Message = string.Format("Please login First"));
        }
    } 
}   