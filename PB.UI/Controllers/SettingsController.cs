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
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Edit()
        {
            Guid userGuid = new Guid(User.Identity.GetUserId());
            SettingsViewModel vm = SettingsViewModel.GetSettings(userGuid);

            return View(vm);
        }
    }
}