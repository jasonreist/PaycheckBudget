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
    public ActionResult NewHome()
    {
      var id = User.Identity.GetUserId();
      if (string.IsNullOrEmpty(id)) return RedirectToAction("Login", "Account");
      var userGuid = new Guid(id);
      var vm = new NewHomePageViewModel(userGuid);
      ViewBag.ShowDOW = true;
      return View(vm);
    }

    public ActionResult Index()
    {
      var id = User.Identity.GetUserId();
      if (string.IsNullOrEmpty(id)) return RedirectToAction("Login", "Account");
      var userGuid = new Guid(id);
      var vm = new HomePageViewModel(userGuid);
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