using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OBS.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Kodlab Üniversitesi";

            return View();
        }
        public ActionResult GirisYap()
        {
            return View();
        }
    }
}