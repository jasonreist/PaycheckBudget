using PB.Entities;

namespace PB.Data.Domain.Repository
{
    public interface IPBUnitOfWork
    {
        GenericRepository<Bill> BillRepository { get; }

        GenericRepository<CustomBill> CustomBillRepository { get; }

        GenericRepository<Paycheck> PaycheckRepository { get; }

        GenericRepository<CustomPaycheck> CustomPaycheckRepository { get; }

        GenericRepository<Payday> PaydayRepository { get; }
        GenericRepository<Setting> SettingsRepository { get; }

        //GenericRepository<UserProfile> UserProfileRepository { get; }

        void Dispose();

        void Save();
    }
}

