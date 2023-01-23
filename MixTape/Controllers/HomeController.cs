using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public ViewResult login_signup()
        {
            return View();
        }

    }
}
