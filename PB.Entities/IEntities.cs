using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.Entities
{
    #region Bill Stuff
    public interface IBill
    {
        int Id { get; set; }
        Guid UserId { get; set; }
        string Name { get; set; }
        decimal Amount { get; set; }
        int DueDay { get; set; }
        string BackgroundColor { get; set; }
        string ForeColor { get; set; }
    }

    public interface ICustomBill
    {
        int Id { get; set; }
        int BillId { get; set; }
        decimal Amount { get; set; }
    }
    #endregion

    #region Income Stuff
    public interface IPaycheck
    {
        int Id { get; set; }
        Guid UserId { get; set; }
        string Type { get; set; }
        decimal Amount { get; set; }
    }

    public interface ICustomPaycheck
    {
        int Id { get; set; }
        int PaycheckId { get; set; }
        decimal Amount { get; set; }
    }

    public interface IPayday
    {
        int Id { get; set; }
        Guid UserId { get; set; }
        System.DateTime Paydate { get; set; }
    }
    #endregion

    #region Settings
    public interface ISetting
    {
        Guid UserID { get; set; }
        int PreviousPeriodsToShow { get; set; }
        int ChecksToShow { get; set; }
        int WeekStartDay { get; set; }
        int DaysInPeriod { get; set; }
        bool AddTithe { get; set; }
        decimal TitheMultiplier { get; set; }
        string TitheBGColor { get; set; }
        string TitheForeColor { get; set; }
    }
    #endregion
}
