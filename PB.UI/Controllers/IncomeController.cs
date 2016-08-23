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
    public class IncomeController : Controller
    {
        // GET: Income
        public ActionResult Index()
        {
            Guid userGuid = new Guid(User.Identity.GetUserId());
            IncomeViewModel vm = new IncomeViewModel(userGuid);
            //ListPaychecksViewModel vm = new ListPaychecksViewModel(userGuid);
            return View(vm);
        }
    }
}