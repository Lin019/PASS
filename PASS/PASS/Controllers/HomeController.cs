using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PASS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "登陸";

            return View();
        }

        public ActionResult Curse()
        {
            ViewBag.Title = "我的課程";
            return View();
        }
        public ActionResult Assignment()
        {
            return View();
        }
    }
}
