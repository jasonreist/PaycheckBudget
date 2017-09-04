using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PB.Business.DomainManagers;
using PB.Business.DomainManagers.Interfaces;
using PB.Entities;
using System.Runtime.Serialization;
using System.Web.Http.Description;
using System.Web.Http.Cors;

namespace PB.Services.Controllers.API
{
    [RoutePrefix("api")]
    public class ItemController : ApiController
    {
        private readonly IEntityManager context;

        public ItemController()
        {
            this.context = new EntityManager();
        }

        //#region Shared
        //[Route("GetBill/{id:int}")]
        //[Route("GetCustomBill/{id:int}")]
        //[Route("GetBillStyle/{id:int}")]
        //[Route("GetPaycheck/{id:int}")]
        //[Route("GetCustomPaycheck/{id:int}")]
        //[Route("Getpayday/{id:int}")]
        //public object GetItem(int id)
        //{
        //    if (Request.RequestUri.AbsoluteUri.Contains("bill"))
        //        return (Bill)this.context.GetBill(id);
        //    if (Request.RequestUri.AbsoluteUri.Contains("custompaybill"))
        //        return (CustomBill)this.context.GetCustomBill(id);
        //    if (Request.RequestUri.AbsoluteUri.Contains("billstyle"))
        //        return (BillStyle)this.context.GetBillStyle(id);
        //    if (Request.RequestUri.AbsoluteUri.Contains("paycheck"))
        //        return (Paycheck)this.context.GetPaycheck(id);
        //    if (Request.RequestUri.AbsoluteUri.Contains("custompaycheck"))
        //        return (CustomPaycheck)this.context.GetCustomPaycheck(id);
        //    if (Request.RequestUri.AbsoluteUri.Contains("payday"))
        //        return (Payday)this.context.GetPayday(id);
        //    return null;
        //}
        //[Route("GetBills/{userid:int}")]
        //[Route("GetCustomBills/{userid:int}")]
        //[Route("GetBillStyles/{userid:int}")]
        //[Route("GetPaychecks/{userid:int}")]
        //[Route("GetCustomPaychecks/{userid:int}")]
        //[Route("Getpaydays/{userid:int}")]
        //[Route("GetPaycheckss/{userid:int}")]
        //public IEnumerable<object> GetItems(int userid)
        //{
        //    if (Request.RequestUri.AbsoluteUri.Contains("bills"))
        //        return this.context.GetBills(userid);
        //    if (Request.RequestUri.AbsoluteUri.Contains("custompaybills"))
        //        return this.context.GetCustomBills(userid);
        //    if (Request.RequestUri.AbsoluteUri.Contains("billstyles"))
        //        return this.context.GetBillStyles(userid);
        //    if (Request.RequestUri.AbsoluteUri.Contains("paychecks"))
        //        return this.context.GetPaychecks(userid);
        //    if (Request.RequestUri.AbsoluteUri.Contains("custompaychecks"))
        //        return this.context.GetCustomPaychecks(userid);
        //    if (Request.RequestUri.AbsoluteUri.Contains("paydays"))
        //        return this.context.GetPaydays(userid);
        //    return null;
        //}
        //#endregion

        #region Bill Stuff
        [HttpGet]
        [Route("GetBill/{id:int}")]
        public Bill GetBill(int id)
        {
            return this.context.GetBill(id);
        }


        [HttpGet]
        [Route("GetCustomBill/{id:int}")]
        public CustomBill GetCustomBill(int id)
        {
            return this.context.GetCustomBill(id);
        }


        [HttpGet]
        [Route("GetBills/{userid:guid}")]
        public IEnumerable<Bill> GetBills(Guid userid)
        {
            return this.context.GetBills(userid);
        }


        [HttpGet]
        [Route("GetCustomBills/{billid:int}")]
        public IEnumerable<CustomBill> GetCustomBills(int billid)
        {
            return this.context.GetCustomBills(billid);
        }


        [HttpPost]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        [Route("CreateBill/")]
        public HttpResponseMessage CreateBill(Bill bill)
        {
            Bill tempItem = new Bill();

            if (bill == null) throw new ArgumentException("bill is null.");

            if (ModelState.IsValid)
            {
                tempItem = this.context.AddUpdateDeleteBill(bill, null, null);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tempItem);
                response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/" + tempItem.Id);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [HttpPost]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        [Route("CreateCustomBill/")]
        public HttpResponseMessage CreateCustomBill(CustomBill custombill)
        {
            CustomBill tempItem = new CustomBill();

            if (custombill == null) throw new ArgumentException("custombill is null.");

            if (ModelState.IsValid)
            {
                tempItem = this.context.AddUpdateDeleteCustomBill(custombill, null, null);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tempItem);
                //response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/" + tempItem.Id);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [HttpPut]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        [Route("UpdateBill/")]
        public HttpResponseMessage UpdateBill(Bill bill)
        {
            if (bill == null) throw new ArgumentException("bill is null.");
            if (ModelState.IsValid)
            {
                this.context.AddUpdateDeleteBill(null, bill, null);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [HttpPut]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        [Route("UpdateCustomBill/")]
        public HttpResponseMessage UpdateCustomBill(CustomBill custombill)
        {
            if (custombill == null) throw new ArgumentException("custombill is null.");
            if (ModelState.IsValid)
            {
                this.context.AddUpdateDeleteCustomBill(null, custombill, null);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [HttpDelete]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        [Route("deleteBill/")]
        public void DeleteBill(Bill bill)
        {
            if (bill == null) throw new ArgumentException("bill is null.");
            this.context.AddUpdateDeleteBill(null, null, bill);
        }


        [HttpDelete]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        [Route("DeleteCustomBill/")]
        public void DeleteCustomBill(CustomBill custombill)
        {
            if (custombill == null) throw new ArgumentException("custombill is null.");
            if (ModelState.IsValid)
            {
                this.context.AddUpdateDeleteCustomBill(null, null, custombill);
            }
        }
        #endregion

        #region Income Stuff
        [HttpGet]
        [Route("GetPaycheck/{id:int}")]
        public Paycheck GetPaycheck(int id)
        {
            return this.context.GetPaycheck(id);
        }
        [HttpGet]
        [Route("GetPaycheck/{userid:guid}/{type}")]
        public Paycheck GetPaycheck(Guid userid, string type)
        {
            return this.context.GetPaycheck(userid, type);
        }
        [HttpGet]
        [Route("GetCustomPaycheck/{id:int}")]
        public CustomPaycheck GetCustomPaycheck(int id)
        {
            return this.context.GetCustomPaycheck(id);
        }
        [HttpGet]
        [Route("GetPayday/{id:int}")]
        public Payday GetPayday(int id)
        {
            return this.context.GetPayday(id);
        }
        //--------------------------------------------------------
        [HttpGet]
        [Route("GetPaychecks/{userid:guid}")]
        public IEnumerable<Paycheck> GetPaychecks(Guid userid)
        {
            return this.context.GetPaychecks(userid);
        }
        [HttpGet]
        [Route("GetCustomPaychecks/{userid:int}")]
        public IEnumerable<CustomPaycheck> GetCustomPaychecks(int userid)
        {
            return this.context.GetCustomPaychecks(userid);
        }
        [HttpGet]
        [Route("GetPaydays/{userid:guid}")]
        public IEnumerable<Payday> GetPaydays(Guid userid)
        {
            return this.context.GetPaydays(userid);
        }
        //--------------------------------------------------------
        [HttpPost]
        [Route("CreatePaycheck/")]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        public HttpResponseMessage CreatePaycheck(Paycheck paycheck)
        {
            Paycheck tempItem = new Paycheck();

            if (paycheck == null) throw new ArgumentException("paycheck is null.");

            if (ModelState.IsValid)
            {
                tempItem = this.context.AddUpdateDeletePaycheck(paycheck, null, null);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tempItem);
                response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/" + tempItem.Id);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        [HttpPost]
        public HttpResponseMessage CreateCustomPaycheck(CustomPaycheck custompaycheck)
        {
            CustomPaycheck tempItem = new CustomPaycheck();

            if (custompaycheck == null) throw new ArgumentException("custompaycheck is null.");

            if (ModelState.IsValid)
            {
                tempItem = this.context.AddUpdateDeleteCustomPaycheck(custompaycheck, null, null);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tempItem);
                response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/" + tempItem.Id);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        [HttpPost]
        public HttpResponseMessage CreatePayday(Payday payday)
        {
            Payday tempItem = new Payday();

            if (payday == null) throw new ArgumentException("payday is null.");

            if (ModelState.IsValid)
            {
                tempItem = this.context.AddUpdateDeletePayday(payday, null, null);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tempItem);
                response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/" + tempItem.Id);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        //--------------------------------------------------------
        [HttpPut]
        [Route("UpdatePaycheck/")]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        public HttpResponseMessage UpdatePaycheck(Paycheck paycheck)
        {
            if (paycheck == null) throw new ArgumentException("paycheck is null.");
            if (ModelState.IsValid)
            {
                this.context.AddUpdateDeletePaycheck(null, paycheck, null);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateCustomPaycheck(CustomPaycheck custompaycheck)
        {
            if (custompaycheck == null) throw new ArgumentException("custompaycheck is null.");
            if (ModelState.IsValid)
            {
                this.context.AddUpdateDeleteCustomPaycheck(null, null, custompaycheck);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        [HttpPut]
        [Route("UpdatePayday/")]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        public HttpResponseMessage UpdatePayday(Payday payday)
        {
            if (payday == null) throw new ArgumentException("payday is null.");
            if (ModelState.IsValid)
            {
                this.context.AddUpdateDeletePayday(null, payday, null);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        //--------------------------------------------------------
        [HttpDelete]
        [Route("DeletePaycheck/")]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        public void DeletePaycheck(Paycheck paycheck)
        {
            if (paycheck == null) throw new ArgumentException("paycheck is null.");
            this.context.AddUpdateDeletePaycheck(null, null, paycheck);
        }
        [HttpDelete]
        [Route("deleteCustomPaycheck/")]
        public void DeleteCustomPaycheck(CustomPaycheck custompaycheck)
        {
            if (custompaycheck == null) throw new ArgumentException("custompaycheck is null.");
            this.context.AddUpdateDeleteCustomPaycheck(null, null, custompaycheck);
        }
        [HttpDelete]
        [Route("deletePayday/")]
        public void DeletePayday(Payday payday)
        {
            if (payday == null) throw new ArgumentException("payday is null.");
            this.context.AddUpdateDeletePayday(null, null, payday);
        }
        #endregion

        #region Settings

        [HttpGet]
        [Route("GetSettings/{userid:guid}")]
        public Setting GetSettings(Guid userid)
        {
            return this.context.GetSettings(userid);
        }

        [HttpPut]
        [EnableCors(origins: "http://localhost:53487,http://www.pb.com,http://jasonreist-001-site1.htempurl.com", headers: "*", methods: "*")]
        [Route("UpdateSettings/")]
        public HttpResponseMessage UpdateSettings(Setting settings)
        {
            if (settings == null) throw new ArgumentException("settings is null.");
            if (ModelState.IsValid)
            {
                this.context.AddUpdateDeleteSettings(null, settings, null);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        #endregion
    }
}