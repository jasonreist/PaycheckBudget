using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Security.Claims;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PB.UI.Models;
using PB.Entities;

namespace PB.UI.DAL
{
  public class PBIinitializer : System.Data.Entity.CreateDatabaseIfNotExists<PBContext>
  {

    protected override void Seed(PBContext context)
    {
      //CREATE A DEMO USER
      ApplicationUserManager UserManager =
        System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
      ApplicationSignInManager SigninManager =
        System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
      var user = new ApplicationUser
      {
        UserName = "demo1",
        Email = "demo1@demo.com",
        FirstName = "Demo",
        EmailConfirmed = true
      };
      var result = UserManager.Create(user, "1Demo!");
      Guid newuserid = new Guid(user.Id);
      //SigninManager.SignIn(user, isPersistent: false, rememberBrowser: false);

      //CREATE SETTINGS, PAYDAYS AND PAYCHECKS
      context.Settings.Add(new Setting()
      {
        UserID = newuserid,
        AddTithe = true,
        TitheMultiplier = .1291m,
        TitheBGColor = "000000",
        TitheForeColor = "ffffff",
        ChecksToShow = 4,
        DaysInPeriod = 14,
        PreviousPeriodsToShow = 1,
        WeekStartDay = 0
      });
      context.Paydays.Add(new Payday()
      {
        UserId = newuserid,
        Paydate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
      });
      context.Paychecks.Add(new Paycheck() {UserId = newuserid, Amount = 1857.14m, Type = "A"});
      context.Paychecks.Add(new Paycheck() {UserId = newuserid, Amount = 1897.14m, Type = "B"});


      //CREATE SOME BILLS
      //NOTE: If you alter these make sure you update the custom bills below.
      var bills = new List<Bill>
      {
        new Bill
        {
          UserId = newuserid,
          Name = "Mortgage/Rent",
          Amount = 1542.24m,
          DueDay = 1,
          ForeColor = "ff00ff",
          BackgroundColor = "000000"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Cell Phone",
          Amount = 75.57m,
          DueDay = 3,
          ForeColor = "cccccc",
          BackgroundColor = "ff00ff"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Cable/Internet",
          Amount = 110.33m,
          DueDay = 7,
          ForeColor = "bababa",
          BackgroundColor = "00ff00"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Car Insurance",
          Amount = 99.94m,
          DueDay = 11,
          ForeColor = "ff0000",
          BackgroundColor = "000000"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Electricity",
          Amount = 150.15m,
          DueDay = 16,
          ForeColor = "0000ff",
          BackgroundColor = "bababa"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Water/Sewer",
          Amount = 40.44m,
          DueDay = 19,
          ForeColor = "00ff00",
          BackgroundColor = "000000"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Car Payment",
          Amount = 346.01m,
          DueDay = 22,
          ForeColor = "eeeeee",
          BackgroundColor = "cacaca"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Visa Credit Card",
          Amount = 45.00m,
          DueDay = 28,
          ForeColor = "cecece",
          BackgroundColor = "000000"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "31st Bill",
          Amount = 90.31m,
          DueDay = 31,
          ForeColor = "000000",
          BackgroundColor = "00FFFF"
        }
      };
      bills.ForEach(s => context.Bills.Add(s));

      //CREATE A FEW CUSTOM BILLS
      var cbills = new List<CustomBill>
      {
        new CustomBill()
        {
          BillId = 2,
          Amount = 95.55m,
          BillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
        },
        new CustomBill()
        {
          BillId = 2,
          Amount = 79.38m,
          BillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1)
        },
        new CustomBill() {BillId = 3, Amount = 125m, BillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7)},
        new CustomBill() {BillId = 7, Amount = 400m, BillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 22)},
        new CustomBill()
        {
          BillId = 7,
          Amount = 450m,
          BillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 2)
        }
      };
      cbills.ForEach(s => context.CustomBills.Add(s));



      context.SaveChanges();

    }
  }
}