using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodServe.Models;
using System.Net.Http;
using Newtonsoft.Json;



namespace FoodServe.Controllers
{
    public class HomeController : Controller
    {  
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            Response.Cookies.Delete("userid");
            Response.Cookies.Delete("name");
            Response.Cookies.Delete("role");
            return View();
        }
        public IActionResult Main()
        {
            string userid = HttpContext.Request.Cookies["userid"];
            string name = HttpContext.Request.Cookies["name"];
            string role = HttpContext.Request.Cookies["role"];
            ViewData["userid"] = userid;
            ViewData["name"] = name;
            ViewData["role"] = role;
            return View();
        }
    }
}
