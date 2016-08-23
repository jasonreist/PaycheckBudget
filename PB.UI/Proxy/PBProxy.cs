using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace PB.UI.Proxy
{
    public class PBProxy: IPBProxy
    {
        private readonly string baseURI = System.Configuration.ConfigurationManager.AppSettings["PBServiceURI"];
        private HttpResponseMessage response = null;

        #region Bill Stuff
        public Entities.Bill GetBill(int id)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(string.Format("{0}/getbill/{1}", location, id)).Result;
            }
            Entities.Bill returnedItem = new Entities.Bill();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<Entities.Bill>().Result;

            return returnedItem;
        }

        public Entities.CustomBill GetCustomBill(int billid)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(string.Format("{0}/getcustombill/{1}", location, billid)).Result;
            }
            Entities.CustomBill returnedItem = new Entities.CustomBill();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<Entities.CustomBill>().Result;

            return returnedItem;
        }

        public IEnumerable<Entities.Bill> GetBills(Guid userid)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(string.Format("{0}/getbills/{1}", location, userid)).Result;
            }
            IEnumerable<Entities.Bill> returnedItems = new List<Entities.Bill>();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItems = response.Content.ReadAsAsync<IEnumerable<Entities.Bill>>().Result;

            return returnedItems;
        }

        public IEnumerable<Entities.CustomBill> GetCustomBills(int billid)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(string.Format("{0}/getcustombills/{1}", location, billid)).Result;
            }
            IEnumerable<Entities.CustomBill> returnedItems = new List<Entities.CustomBill>();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItems = response.Content.ReadAsAsync<IEnumerable<Entities.CustomBill>>().Result;

            return returnedItems;
        }

        public Entities.Bill Post(Entities.Bill bill)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsJsonAsync(location, bill).Result;
            }
            Entities.Bill returnedItem = new Entities.Bill();
            
            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<Entities.Bill>().Result;

            return returnedItem;
        }

        public Entities.CustomBill Post(Entities.CustomBill custombill)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsJsonAsync(location, custombill).Result;
            }
            Entities.CustomBill returnedItem = new Entities.CustomBill();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<Entities.CustomBill>().Result;

            return returnedItem;
        }

        public Entities.Bill Put(Entities.Bill bill)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PutAsJsonAsync(location, bill).Result;
            }
            Entities.Bill returnedItem = new Entities.Bill();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<Entities.Bill>().Result;

            return returnedItem;
        }

        public Entities.CustomBill Put(Entities.CustomBill custombill)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PutAsJsonAsync(location, custombill).Result;
            }
            Entities.CustomBill returnedItem = new Entities.CustomBill();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<Entities.CustomBill>().Result;

            return returnedItem;
        }

        public void Delete(Entities.Bill bill)
        {
            string location = string.Format("{0}{1}/{2}", baseURI, "api", "deletebill");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.DeleteAsync(location).Result;
            }

            if (response.StatusCode == HttpStatusCode.OK)
            { }
        }

        public void Delete(Entities.CustomBill custombill)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Income Stuff
        public Entities.Paycheck GetPaycheck(int id)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(string.Format("{0}/getpaycheck/{1}", location, id)).Result;
            }
            Entities.Paycheck returnedItem = new Entities.Paycheck();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<Entities.Paycheck>().Result;

            return returnedItem;
        }
        public Entities.Paycheck GetPaycheck(string userid, string type)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(string.Format("{0}/getpaycheck/{1}/{2}", location, userid, type)).Result;
            }
            Entities.Paycheck returnedItem = new Entities.Paycheck();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<Entities.Paycheck>().Result;

            return returnedItem;
        }

        public Entities.CustomPaycheck GetCustomPaycheck(int id)
        {
            throw new NotImplementedException();
        }

        public Entities.Payday GetPayday(int id)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(string.Format("{0}/getpayday/{1}", location, id)).Result;
            }
            Entities.Payday returnedItem = new Entities.Payday();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<Entities.Payday>().Result;

            return returnedItem;
        }

        public IEnumerable<Entities.Paycheck> GetPaychecks(Guid userid)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(string.Format("{0}/getpaychecks/{1}", location, userid)).Result;
            }
            IEnumerable<Entities.Paycheck> returnedItems = new List<Entities.Paycheck>();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItems = response.Content.ReadAsAsync<IEnumerable<Entities.Paycheck>>().Result;

            return returnedItems;
        }

        public IEnumerable<Entities.CustomPaycheck> GetCustomPaychecks(int paycheckid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entities.Payday> GetPaydays(Guid userid)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(string.Format("{0}/getpaydays/{1}", location, userid)).Result;
            }
            IEnumerable<Entities.Payday> returnedItem = new List<Entities.Payday>();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<IEnumerable<Entities.Payday>>().Result;

            return returnedItem;
        }

        public System.Net.Http.HttpResponseMessage Post(Entities.Paycheck paycheck)
        {
            throw new NotImplementedException();
        }

        public System.Net.Http.HttpResponseMessage Post(Entities.CustomPaycheck custompaycheck)
        {
            throw new NotImplementedException();
        }

        public System.Net.Http.HttpResponseMessage Post(Entities.Payday payday)
        {
            throw new NotImplementedException();
        }

        public System.Net.Http.HttpResponseMessage Put(Entities.Paycheck paycheck)
        {
            throw new NotImplementedException();
        }

        public System.Net.Http.HttpResponseMessage Put(Entities.CustomPaycheck custompaycheck)
        {
            throw new NotImplementedException();
        }

        public System.Net.Http.HttpResponseMessage Put(Entities.Payday payday)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entities.Paycheck paycheck)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entities.CustomPaycheck custompaycheck)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entities.Payday payday)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Settings

        public Entities.Setting GetSettings(Guid userid)
        {
            string location = string.Format("{0}{1}", baseURI, "api");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(string.Format("{0}/getsettings/{1}", location, userid)).Result;
            }
            Entities.Setting returnedItem = new Entities.Setting();

            if (response.StatusCode == HttpStatusCode.OK)
                returnedItem = response.Content.ReadAsAsync<Entities.Setting>().Result;

            return returnedItem;
        }
        #endregion
    }
}