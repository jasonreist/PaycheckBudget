namespace PB.Data.Domain.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using PB.Entities;

    public class PBUnitOfWork : IDisposable, IPBUnitOfWork
    {
        private DbContext context;
        private GenericRepository<Bill> billRepository;
        private GenericRepository<CustomBill> customBillRepository;
        private GenericRepository<Paycheck> paycheckRepository;
        private GenericRepository<CustomPaycheck> customPaycheckRepository;
        private GenericRepository<Payday> paydayRepository;
        private GenericRepository<Setting> settingsRepository;
        //private GenericRepository<UserProfile> userProfileRepository;

        public PBUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public GenericRepository<Bill> BillRepository
        {
            get
            {
                {
                    if (this.billRepository == null)
                        this.billRepository = new GenericRepository<Bill>(context);
                    return billRepository;
                }
            }
        }

        public GenericRepository<CustomBill> CustomBillRepository
        {
            get
            {
                {
                    if (this.customBillRepository == null)
                        this.customBillRepository = new GenericRepository<CustomBill>(context);
                    return customBillRepository;
                }
            }
        }

        public GenericRepository<Paycheck> PaycheckRepository
        {
            get
            {
                {
                    if (this.paycheckRepository == null)
                        this.paycheckRepository = new GenericRepository<Paycheck>(context);
                    return paycheckRepository;
                }
            }
        }

        public GenericRepository<CustomPaycheck> CustomPaycheckRepository
        {
            get
            {
                if (this.customPaycheckRepository == null)
                    this.customPaycheckRepository = new GenericRepository<CustomPaycheck>(context);
                return customPaycheckRepository;
            }
        }

        public GenericRepository<Payday> PaydayRepository
        {
            get
            {
                if (this.paydayRepository == null)
                    this.paydayRepository = new GenericRepository<Payday>(context);
                return paydayRepository;
            }
        }

        public GenericRepository<Setting> SettingsRepository
        {
            get
            {
                {
                    if (this.settingsRepository == null)
                        this.settingsRepository = new GenericRepository<Setting>(context);
                    return settingsRepository;
                }
            }
        }

        //public GenericRepository<UserProfile> UserProfileRepository
        //{
        //    get
        //    {
        //        if (this.userProfileRepository == null)
        //            this.userProfileRepository = new GenericRepository<UserProfile>(context);
        //        return userProfileRepository;
        //    }
        //}

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                throw;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

