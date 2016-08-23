using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.Entities
{
    #region Bill Stuff
    public class Bill : IBill
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int DueDay { get; set; }
        public string BackgroundColor { get; set; }
        public string ForeColor { get; set; }
    }

    public class CustomBill : ICustomBill
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
    }
    #endregion

    #region Income Stuff
    public class Paycheck : IPaycheck
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }

    public class CustomPaycheck : ICustomPaycheck
    {
        public int Id { get; set; }
        public int PaycheckId { get; set; }
        public DateTime PaycheckDate { get; set; }
        public decimal Amount { get; set; }
    }

    public class Payday : IPayday
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime Paydate { get; set; }
    }
#endregion

    #region Settings
    public class Setting : ISetting
    {
        [Key]
        public Guid UserID { get; set; }
        public int PreviousPeriodsToShow { get; set; }
        public int ChecksToShow { get; set; }
        public int WeekStartDay { get; set; }
        public int DaysInPeriod { get; set; }
        public bool AddTithe { get; set; }
        public decimal TitheMultiplier { get; set; }
        public string TitheBGColor { get; set; }
        public string TitheForeColor { get; set; }
    }
    #endregion
}
