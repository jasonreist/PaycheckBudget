using PB.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace PB.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Login", "Account");
            Guid userGuid = new Guid(id);
            HomePageViewModel vm = new HomePageViewModel(userGuid);
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}