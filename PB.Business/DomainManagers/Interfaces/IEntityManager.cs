namespace PB.Business.DomainManagers.Interfaces
{
    using PB.Entities;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    //[ContractClass(typeof(BillContract))]
    public interface IEntityManager
    {
        #region Bill Stuff
        List<Bill> GetBills(Guid userid);
        Bill GetBill(int id);
        Bill AddUpdateDeleteBill(Bill AddItem, Bill UpdateItem, Bill DeleteItem);

        List<CustomBill> GetCustomBills(int billid);
        CustomBill GetCustomBill(int billid);
        CustomBill AddUpdateDeleteCustomBill(CustomBill AddItem, CustomBill UpdateItem, CustomBill DeleteItem);
        #endregion 

        #region Income Stuff
        List<Paycheck> GetPaychecks(Guid userid);
        Paycheck GetPaycheck(int id);
        Paycheck GetPaycheck(Guid userid, string type);
        Paycheck AddUpdateDeletePaycheck(Paycheck AddItem, Paycheck UpdateItem, Paycheck DeleteItem);

        List<CustomPaycheck> GetCustomPaychecks(int paycheckid);
        CustomPaycheck GetCustomPaycheck(int id); //????
        CustomPaycheck AddUpdateDeleteCustomPaycheck(CustomPaycheck AddItem, CustomPaycheck UpdateItem, CustomPaycheck DeleteItem);

        List<Payday> GetPaydays(Guid userid);
        Payday GetPayday(int id); //????
        Payday AddUpdateDeletePayday(Payday AddItem, Payday UpdateItem, Payday DeleteItem);
        #endregion

        #region Settings
        Setting GetSettings(Guid userid);

        Setting AddUpdateDeleteSettings(Setting AddItem, Setting UpdateItem, Setting DeleteItem);
        #endregion
    }
}

