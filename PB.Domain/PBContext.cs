namespace PB.Domain
{
    using PB.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class PBEntities : DbContext
    {
        public PBEntities()
            : base("name=PBContext")
        {
            Database.SetInitializer<PBEntities>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
