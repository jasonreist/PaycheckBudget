using AutoMapper;
using PB.Common.Helpers;
using PB.UI.Models;
using PB.UI.Proxy;
using PB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using PB.UI.DAL;

namespace PB.UI.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private PBContext db = new PBContext();

        public BillsController()
        {
            this.db = new PBContext();
        }

        public ActionResult Index()
        {
            Guid userGuid = new Guid(User.Identity.GetUserId());

            ListBillsViewModel vm = new ListBillsViewModel(userGuid);

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            Guid userGuid = new Guid(User.Identity.GetUserId());
            EditBillViewModel vm = new EditBillViewModel(id);
            return View(vm);
        }
        [HttpPost]
        [Route("Bills/Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditBillViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            Guid userGuid = new Guid(User.Identity.GetUserId());
            model.Bill.UserId = userGuid;

            Bill bill = Mapper.Map<BillViewModel, Bill>(model.Bill);
            bill.BackgroundColor = bill.BackgroundColor.Replace("#", "");
            bill.ForeColor = bill.ForeColor.Replace("#", "");

            db.Entry(bill).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            Guid userGuid = new Guid(User.Identity.GetUserId());
            CreateBillViewModel vm = new CreateBillViewModel();
            vm.UserId = userGuid;
            return View(vm);
        }
        [HttpPost]
        [Route("Bills/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBillViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            Guid userGuid = new Guid(User.Identity.GetUserId());
            model.UserId = userGuid;

            Bill newbill = Mapper.Map<CreateBillViewModel, Bill>(model);
            newbill.BackgroundColor = newbill.BackgroundColor.Replace("#", "");
            newbill.ForeColor = newbill.ForeColor.Replace("#", "");

            db.Bills.Add(newbill);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        [Route("Bills/Custom/{id:int}")]
        public ActionResult Custom(int id)
        {
            CustomBillsViewModel vm = new CustomBillsViewModel(id);

            DateTime ThisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, vm.Bill.DueDay);

            for (int i = 0; i < 6; i++)
            {
                DateTime temp = ThisMonth.AddMonths(i);
                if (!vm.CustomBills.Any(a => a.BillDate == temp))
                {
                    vm.CustomBills.Add(new CustomBillViewModel() { Amount = 0, BillDate = temp, BillId = vm.Bill.Id, Exists = false });
                }
            }

            vm.CustomBills.Sort((x, y) => x.BillDate.CompareTo(y.BillDate));

            return View(vm);
        }
        [HttpPost]
        [Route("Bills/Custom")]
        [ValidateAntiForgeryToken]
        public ActionResult Custom(CreateBillViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            Guid userGuid = new Guid(User.Identity.GetUserId());
            model.UserId = userGuid;

            Bill newbill = Mapper.Map<CreateBillViewModel, Bill>(model);
            newbill.BackgroundColor = newbill.BackgroundColor.Replace("#", "");
            newbill.ForeColor = newbill.ForeColor.Replace("#", "");

            db.Bills.Add(newbill);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        private string padNumber(int i)
        {
            return (i < 10) ? "0" + i.ToString() : i.ToString();
        }
    }
}