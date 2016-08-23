namespace PB.UI.Proxy
{
    using System.Collections.Generic;
    using PB.Entities;
    using System.Net.Http;
    using System;
    //using PaycheckBudget.Entities.DataTransformationObjects;

    public interface IPBProxy
    {
        #region Bill Stuff;
        Bill GetBill(int id);
        CustomBill GetCustomBill(int id);
        //--------------------------------------------------------
        IEnumerable<Bill> GetBills(Guid userid);
        IEnumerable<CustomBill> GetCustomBills(int billid);
        //--------------------------------------------------------

        Entities.Bill Post(Bill bill);
        Entities.CustomBill Post(CustomBill custombill);
        //--------------------------------------------------------

        Entities.Bill Put(Bill bill);
        Entities.CustomBill Put(CustomBill custombill);
        //--------------------------------------------------------

        void Delete(Bill bill);
        void Delete(CustomBill custombill);
        #endregion

        #region Income Stuff
        Paycheck GetPaycheck(int id);
        CustomPaycheck GetCustomPaycheck(int id);
        Payday GetPayday(int id);
        //--------------------------------------------------------
        IEnumerable<Paycheck> GetPaychecks(Guid userid);
        IEnumerable<CustomPaycheck> GetCustomPaychecks(int userid);
        IEnumerable<Payday> GetPaydays(Guid userid);
        //--------------------------------------------------------

        HttpResponseMessage Post(Paycheck paycheck);
        HttpResponseMessage Post(CustomPaycheck custompaycheck);
        HttpResponseMessage Post(Payday payday);
        //--------------------------------------------------------

        HttpResponseMessage Put(Paycheck paycheck);
        HttpResponseMessage Put(CustomPaycheck custompaycheck);
        HttpResponseMessage Put(Payday payday);
        //--------------------------------------------------------

        void Delete(Paycheck paycheck);
        void Delete(CustomPaycheck custompaycheck);
        void Delete(Payday payday);
        #endregion
    }
}

