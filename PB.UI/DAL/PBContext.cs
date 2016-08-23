namespace PB.UI.DAL
{
    using PB.Entities;
    using PB.UI.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class PBContext : DbContext
    {
        public PBContext()
            : base("name=PBContext")
        {
            Database.SetInitializer<PBContext>(new PBIinitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        
        public DbSet<Bill> Bills { get; set; }
        public DbSet<CustomBill> CustomBills { get; set; }
        public DbSet<Paycheck> Paychecks { get; set; }
        public DbSet<Payday> Paydays { get; set; }
        public DbSet<CustomPaycheck> CustomPaychecks { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
