namespace PB.Business.DomainManagers
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using PB.Data.Domain;
    using PB.Data.Domain.Repository;
    using PB.Business.DomainManagers.Interfaces;
    using PB.Domain;
    using PB.Entities;
    //using PaycheckBudget.Attributes;
    //using PaycheckBudget;
    //using PaycheckBudget.Business.DomainManagers.Interfaces;
    //using PaycheckBudget.Domain.Repository;

    public class EntityManager : IEntityManager, IDisposable
    {
        private bool disposed = false;

        private readonly IPBUnitOfWork uow;
        private readonly IMappingEngine mapper;

        public EntityManager()
        {
            this.uow = new PBUnitOfWork(new PBEntities());
            this.mapper = Mapper.Engine;
        }

        public EntityManager(IPBUnitOfWork unit, IMappingEngine map)
        {
            this.uow = unit;
            this.mapper = map;
        }

        #region Bill Stuff
        public List<Bill> GetBills(Guid userid)
        {
            List<Bill> items = null;
            using (var context = new PBEntities())
            {
                items = context.Bills.AsNoTracking().ToList().Where(a => a.UserId == userid).ToList();
            }
            return items;
        }
        public Bill GetBill(int id)
        {
            Bill item = null;
            using (var context = new PBEntities())
            {
                item = context.Bills.Where(s => s.Id == id).FirstOrDefault();
            }
            return item;
        }
        public Bill AddUpdateDeleteBill(Bill AddItem, Bill UpdateItem, Bill DeleteItem)
        {
            if (AddItem != null)
            {
                Bill newItem = this.uow.BillRepository.Insert(AddItem);
                this.uow.Save();
                return newItem;
            }

            if (UpdateItem != null)
                this.uow.BillRepository.Update(UpdateItem);

            if (DeleteItem != null)
                this.uow.BillRepository.Delete(DeleteItem);

            this.uow.Save();

            return null;
        }

        public List<CustomBill> GetCustomBills(int billid)
        {
            List<CustomBill> items = null;
            using (var context = new PBEntities())
            {
                items = context.CustomBills.AsNoTracking().ToList().Where(a => a.BillId == billid).ToList();
            }
            return items;
        }
        public CustomBill GetCustomBill(int billid)
        {
            CustomBill item = null;
            using (var context = new PBEntities())
            {
                item = context.CustomBills.Where(s => s.BillId == billid).FirstOrDefault();
            }
            return item;
        }
        public CustomBill AddUpdateDeleteCustomBill(CustomBill AddItem, CustomBill UpdateItem, CustomBill DeleteItem)
        {
            if (AddItem != null)
            {
                CustomBill newItem = this.uow.CustomBillRepository.Insert(AddItem);
                this.uow.Save();
                return newItem;
            }

            if (UpdateItem != null)
                this.uow.CustomBillRepository.Update(UpdateItem);

            if (DeleteItem != null)
                this.uow.CustomBillRepository.Delete(DeleteItem);

            this.uow.Save();

            return null;
        }
        #endregion

        #region Income Stuff
        public List<Paycheck> GetPaychecks(Guid userid)
        {
            List<Paycheck> items = null;
            using (var context = new PBEntities())
            {
                items = context.Paychecks.AsNoTracking().ToList().Where(a => a.UserId == userid).ToList();
            }
            return items;
        }
        public Paycheck GetPaycheck(int id)
        {
            Paycheck item = null;
            using (var context = new PBEntities())
            {
                item = context.Paychecks.Where(s => s.Id == id).FirstOrDefault();
            }
            return item;
        }
        public Paycheck GetPaycheck(Guid userid, string type)
        {
            Paycheck item = null;
            using (var context = new PBEntities())
            {
                item = context.Paychecks.Where(s => s.Type == type && s.UserId == userid).FirstOrDefault();
            }
            return item;
        }
        public Paycheck AddUpdateDeletePaycheck(Paycheck AddItem, Paycheck UpdateItem, Paycheck DeleteItem)
        {
            if (AddItem != null)
            {
                Paycheck newItem = this.uow.PaycheckRepository.Insert(AddItem);
                this.uow.Save();
                return newItem;
            }

            if (UpdateItem != null)
                this.uow.PaycheckRepository.Update(UpdateItem);

            if (DeleteItem != null)
                this.uow.PaycheckRepository.Delete(DeleteItem);

            this.uow.Save();

            return null;
        }

        public List<CustomPaycheck> GetCustomPaychecks(int paycheckid)
        {
            List<CustomPaycheck> items = null;
            using (var context = new PBEntities())
            {
                items = context.CustomPaychecks.AsNoTracking().ToList().Where(a => a.PaycheckId == paycheckid).ToList();
            }
            return items;
        }
        public CustomPaycheck GetCustomPaycheck(int id)
        {
            CustomPaycheck item = null;
            using (var context = new PBEntities())
            {
                item = context.CustomPaychecks.Where(s => s.Id == id).FirstOrDefault();
            }
            return item;
        }
        public CustomPaycheck AddUpdateDeleteCustomPaycheck(CustomPaycheck AddItem, CustomPaycheck UpdateItem, CustomPaycheck DeleteItem)
        {
            if (AddItem != null)
            {
                CustomPaycheck newItem = this.uow.CustomPaycheckRepository.Insert(AddItem);
                this.uow.Save();
                return newItem;
            }

            if (UpdateItem != null)
                this.uow.CustomPaycheckRepository.Update(UpdateItem);

            if (DeleteItem != null)
                this.uow.CustomPaycheckRepository.Delete(DeleteItem);

            this.uow.Save();

            return null;
        }

        public List<Payday> GetPaydays(Guid userid)
        {
            List<Payday> items = null;
            using (var context = new PBEntities())
            {
                items = context.Paydays.AsNoTracking().ToList().Where(a => a.UserId == userid).ToList();
            }
            return items;
        }
        public Payday GetPayday(int id)
        {
            Payday item = null;
            using (var context = new PBEntities())
            {
                item = context.Paydays.Where(s => s.Id == id).FirstOrDefault();
            }
            return item;
        }
        public Payday AddUpdateDeletePayday(Payday AddItem, Payday UpdateItem, Payday DeleteItem)
        {
            if (AddItem != null)
            {
                Payday newItem = this.uow.PaydayRepository.Insert(AddItem);
                this.uow.Save();
                return newItem;
            }

            if (UpdateItem != null)
                this.uow.PaydayRepository.Update(UpdateItem);

            if (DeleteItem != null)
                this.uow.PaydayRepository.Delete(DeleteItem);

            this.uow.Save();

            return null;
        }
        #endregion

        #region Settings
        public Setting GetSettings(Guid userid)
        {
            Setting items = null;
            using (var context = new PBEntities())
            {
                items = context.Settings.AsNoTracking().ToList().Single(a => a.UserID == userid);
            }
            return items;
        }
        public Setting AddUpdateDeleteSettings(Setting AddItem, Setting UpdateItem, Setting DeleteItem)
        {
            if (AddItem != null)
            {
                Setting newItem = this.uow.SettingsRepository.Insert(AddItem);
                this.uow.Save();
                return newItem;
            }

            if (UpdateItem != null)
                this.uow.SettingsRepository.Update(UpdateItem);

            if (DeleteItem != null)
                this.uow.SettingsRepository.Delete(DeleteItem);

            this.uow.Save();

            return null;
        }
        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) this.uow.Dispose();
            disposed = true;
        }
    }
}


