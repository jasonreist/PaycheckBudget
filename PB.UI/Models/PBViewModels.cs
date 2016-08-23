using AutoMapper;
using PB.UI.Proxy;
using PB.Entities;
using PB.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace PB.UI.Models
{
    public class BillViewModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        [DisplayName("Due Day")]
        public int DueDay { get; set; }
        public string DueDaySuffix { get; set; }
        public int CustomBillCount { get; set; }
        [DisplayName("Background Color")]
        public string BackgroundColor { get; set; }
        [DisplayName("Fore Color")]
        public string ForeColor { get; set; }

        public BillViewModel()
        {
        }
        public BillViewModel(BillViewModel other)
        {
            this.UserId = other.UserId;
            this.Name = other.Name;
            this.Amount = other.Amount;
            this.DueDay = other.DueDay;
            this.DueDaySuffix = other.DueDaySuffix;
            this.CustomBillCount = other.CustomBillCount;
            this.BackgroundColor = other.BackgroundColor;
            this.ForeColor = other.ForeColor;
        }
    }

    public class ListBillsViewModel
    {
        [Key]
        public int Id { get; set; }

        public List<BillViewModel> Bills { get; set; }

        public ListBillsViewModel(Guid userid)
        {
            this.Bills = new List<BillViewModel>();

            PBProxy p = new PBProxy();
            foreach (Bill b in p.GetBills(userid).OrderBy(a => a.DueDay))
            {
                BillViewModel bill = Mapper.Map<BillViewModel>(b);
                bill.DueDaySuffix = Utility.IntSuffix(bill.DueDay);

                IEnumerable<CustomBill> cbills = p.GetCustomBills(b.Id).OrderBy(a => a.BillDate);
                if (cbills != null)
                    bill.CustomBillCount = cbills.Count();

                this.Bills.Add(bill);
            }
            p = null;
        }
    }

    public class CreateBillViewModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        [DisplayName("Due Day")]
        public int DueDay { get; set; }
        public string DueDaySuffix { get; set; }
        [DisplayName("Fore Color")]
        public string ForeColor { get; set; }
        [DisplayName("Background Color")]
        public string BackgroundColor { get; set; }
        public List<BootstrapLI> DOTM { get; set; }

        public CreateBillViewModel()
        {
            this.DueDay = 1;
            this.DueDaySuffix = "st";
            this.DOTM = new List<BootstrapLI>();
            for (int i = 1; i < 32; i++)
            {
                this.DOTM.Add(new BootstrapLI() { Text = i.ToString() + Utility.IntSuffix(i), Value = i.ToString() });
            }
        }

    }

    public class EditBillViewModel
    {
        public EditBillViewModel() { }

        public BillViewModel Bill { get; set; }

        [DisplayName("Custom Bills")]
        public List<CustomBillViewModel> CustomBills { get; set; }

        public List<BootstrapLI> DOTM { get; set; }

        public EditBillViewModel(int id)
        {
            this.CustomBills = new List<CustomBillViewModel>();
            this.DOTM = new List<BootstrapLI>();

            PBProxy p = new PBProxy();
            Bill b = p.GetBill(id);
            this.Bill = Mapper.Map<Bill, BillViewModel>(b);
            this.Bill.DueDaySuffix = Utility.IntSuffix(this.Bill.DueDay);
            for (int i = 1; i < 32; i++)
            {
                this.DOTM.Add(new BootstrapLI() { Text = i.ToString() + Utility.IntSuffix(i), Value = i.ToString(), Selected = (i == b.DueDay) });
            }

            IEnumerable<CustomBill> cbills = p.GetCustomBills(b.Id);
            if (cbills != null)
            {
                this.Bill.CustomBillCount = cbills.Count();
                //this.CustomBills = Mapper.Map<List<CustomBillViewModel>>(cbills);
            }

            p = null;
        }
    }

    //---------------------------------------------------------------------------------

    public class CustomBillViewModel
    {
        public int Id { get; set; }

        public int BillId { get; set; }

        [DisplayName("Due Date")]
        public DateTime BillDate { get; set; }

        public decimal Amount { get; set; }

        public bool Exists { get; set; }
    }

    public class CustomBillsViewModel
    {
        public BillViewModel Bill { get; set; }
        public List<CustomBillViewModel> CustomBills { get; set; }

        public CustomBillsViewModel(int id)
        {
            this.CustomBills = new List<CustomBillViewModel>();

            PBProxy p = new PBProxy();
            Bill b = p.GetBill(id);
            this.Bill = Mapper.Map<Bill, BillViewModel>(b);
            this.Bill.DueDaySuffix = Utility.IntSuffix(this.Bill.DueDay);

            IEnumerable<CustomBill> cbills = p.GetCustomBills(b.Id);
            if (cbills != null)
            {
                this.CustomBills = Mapper.Map<List<CustomBillViewModel>>(cbills);
            }

            p = null;
        }
    }

    //public class ListCustomBillsViewModel
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    [DisplayName("Custom Bills")]
    //    public List<CustomBillViewModel> CustomBills { get; set; }

    //    public ListCustomBillsViewModel()
    //    {
    //        this.CustomBills = new List<CustomBillViewModel>();
    //    }
    //}

    //public class CreateCustomBillViewModel
    //{
    //    public int Id { get; set; }

    //    public int BillId { get; set; }

    //    [DisplayName("Due Date")]
    //    public DateTime BillDate { get; set; }

    //    public decimal Amount { get; set; }
    //}

    //public class EditCustomBillViewModel
    //{
    //    public int Id { get; set; }

    //    public int BillId { get; set; }

    //    [DisplayName("Due Date")]
    //    public DateTime BillDate { get; set; }

    //    public decimal Amount { get; set; }
    //}


    //---------------------------------------------------------------------------------

    public class PaycheckSummaryViewModel
    {
        public string CurrentClass { get; set; }
        public DateTime Payday { get; set; }
        public decimal Credits { get; set; }
        public decimal Debits { get; set; }
        public decimal Remaining { get { return this.Credits - this.Debits; } set { } }

        public PaycheckSummaryViewModel() { }
    }
    public class PaycheckViewModel
    {
        public Int32 ID { get; set; }
        public Guid UserID { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public bool Exists { get; set; }

        public PaycheckViewModel()
        {
        }
        public PaycheckViewModel(Guid userid, string type)
        {
            this.Type = type;
            this.UserID = userid;
            PBProxy p = new PBProxy();
            this.Amount = p.GetPaycheck(userid.ToString(), type).Amount;
            this.Exists = true;
            p = null;
        }
    }

    public class ListPaychecksViewModel
    {
        [Key]
        public int Id { get; set; }

        public List<PaycheckViewModel> Paychecks { get; set; }

        public ListPaychecksViewModel(Guid userid)
        {
            this.Paychecks = new List<PaycheckViewModel>();


        }
    }

    public class IncomeViewModel
    {
        [Key]
        public int Id { get; set; }

        public Payday PayDay { get; set; }

        public List<PaycheckViewModel> Paychecks { get; set; }

        public IncomeViewModel(Guid userid)
        {
            this.Paychecks = new List<PaycheckViewModel>();

            PBProxy p = new PBProxy();
            this.Paychecks = Mapper.Map<List<PaycheckViewModel>>(p.GetPaychecks(userid).OrderBy(a => a.Type));

            int index = 0;
            foreach (string type in "A,B,C,D".Split(','))
            {
                if (!this.Paychecks.Any(a => a.Type == type))
                {
                    this.Paychecks.Insert(index, new PaycheckViewModel() { Type = type, Exists = false, Amount = 0, UserID = userid });
                }
                index++;
            }

            this.PayDay = p.GetPaydays(userid).FirstOrDefault();
        }
    }

    public class PaycheckDetailsViewModel
    {
        public PaycheckSummaryViewModel Summary { get; set; }
        public List<DayViewModel> Days { get; set; }

        public PaycheckDetailsViewModel()
        {
            this.Days = new List<DayViewModel>();
        }
    }
    public class DayViewModel
    {
        public DateTime Date { get; set; }
        public List<BillViewModel> Bills { get; set; }
        public DayViewModel() { this.Bills = new List<BillViewModel>(); }
    }

    public class HomePageViewModel
    {
        public List<PaycheckDetailsViewModel> Paychecks { get; set; }

        public HomePageViewModel(Guid userid)
        {
            SettingsViewModel settings = SettingsViewModel.GetSettings(userid);
            this.Paychecks = new List<PaycheckDetailsViewModel>();

            DayViewModel day = null;
            PaycheckSummaryViewModel summary = null;

            PBProxy p = new PBProxy();
            Payday PayDaySeed = p.GetPaydays(userid).FirstOrDefault();
            SetSeed(ref PayDaySeed, settings.PreviousPeriodsToShow);
            
            ListBillsViewModel AllBills = new ListBillsViewModel(userid);

            int CurrentMonth = 0;
            bool WillMonthChange = false;

            for (int checkIndex = 0; checkIndex < settings.ChecksToShow; checkIndex++)
            {
                DateTime thisdate = PayDaySeed.Paydate.AddDays(checkIndex * 14);
                string PaycheckType = thisdate.Day <= 14 ? "A" : "B";

                PaycheckDetailsViewModel paycheckdetails = new PaycheckDetailsViewModel();
                PaycheckViewModel Paycheck = new PaycheckViewModel(userid, PaycheckType);

                summary = new PaycheckSummaryViewModel();
                summary.Payday = thisdate;
                summary.Credits = Paycheck.Amount;

                #region For Each Day In Pay Period
                for (int dayIndex = 0; dayIndex < 14; dayIndex++)
                {
                    DateTime tempdate = thisdate.AddDays(dayIndex);
                    if (CurrentMonth == 0 | tempdate.Day == 31 | tempdate.Day < 28)
                        WillMonthChange = false;
                    else
                        WillMonthChange = tempdate.AddDays(1).Month != CurrentMonth;
                    CurrentMonth = tempdate.Month;

                    day = new DayViewModel();
                    day.Date = tempdate;

                    #region Tithe
                    if (dayIndex == 0)
                    {
                        if (settings.AddTithe)
                        {
                            BillViewModel Tithe = new BillViewModel();
                            Tithe.Name = "Tithe";
                            Tithe.Amount = Paycheck.Amount * settings.TitheMultiplier;
                            Tithe.BackgroundColor = settings.TitheBGColor;
                            Tithe.ForeColor = settings.TitheForeColor;
                            summary.Debits += Tithe.Amount;
                            Tithe.DueDay = tempdate.Day;
                            day.Bills.Add(Tithe);
                        }
                    }
                    #endregion

                    if (tempdate.ToShortDateString() == DateTime.Now.ToShortDateString()) summary.CurrentClass = "list-group-item-info";
                    if (AllBills.Bills.Any(b => b.DueDay == tempdate.Day))
                    {
                        List<BillViewModel> todaysbills = AllBills.Bills.Where(b => b.DueDay == tempdate.Day).ToList();
                        foreach (BillViewModel todaysbill in todaysbills)
                        {
                            CustomBillsViewModel CustomBills = new CustomBillsViewModel(todaysbill.Id);
                            CustomBillViewModel thiscustombill = CustomBills.CustomBills.FirstOrDefault(cb => cb.BillDate == tempdate);

                            if (thiscustombill != null)
                            {
                                BillViewModel billcopy = new BillViewModel(todaysbill);
                                //billcopy.Name = "* " + billcopy.Name;
                                billcopy.Amount = thiscustombill.Amount;
                                day.Bills.Add(billcopy);
                                summary.Debits += billcopy.Amount;
                            }
                            else
                            {
                                day.Bills.Add(todaysbill);
                                summary.Debits += todaysbill.Amount;
                            }
                        }
                    }

                    #region Month Changed
                    if (WillMonthChange)
                    {
                        for (int d = tempdate.Day + 1; d < 32; d++)
                        {
                            if (AllBills.Bills.Any(b => b.DueDay == d))
                            {
                                List<BillViewModel> todaysbills = AllBills.Bills.Where(b => b.DueDay == d).ToList();
                                foreach (BillViewModel todaysbill in todaysbills)
                                {
                                    CustomBillsViewModel CustomBills = new CustomBillsViewModel(todaysbill.Id);
                                    CustomBillViewModel thiscustombill = CustomBills.CustomBills.FirstOrDefault(cb => cb.BillDate.Month == CurrentMonth);

                                    if (thiscustombill != null)
                                    {
                                        BillViewModel billcopy = new BillViewModel(todaysbill);
                                        //billcopy.Name = "* " + billcopy.Name + " (" + d.ToString() + ")";
                                        billcopy.Amount = thiscustombill.Amount;
                                        day.Bills.Add(billcopy);
                                        summary.Debits += billcopy.Amount;
                                    }
                                    else
                                    {
                                        //todaysbill.Name = todaysbill.Name + " (" + d.ToString() + ")";
                                        day.Bills.Add(todaysbill);
                                        summary.Debits += todaysbill.Amount;
                                    }
                                }
                            }

                        }
                    }
                    #endregion

                    paycheckdetails.Days.Add(day);
                }
                #endregion

                paycheckdetails.Summary = summary;
                summary = null;

                this.Paychecks.Add(paycheckdetails);
                paycheckdetails = null;
            }

            p = null;
        }

        internal void SetSeed(ref Payday seed, int PreviousPeriodsToShow)
        {
            DateTime today = DateTime.Now;
            bool FoundCurrent = false;
            while (!FoundCurrent)
            {   //this will find the current pay period
                seed.Paydate = seed.Paydate.AddDays(14);
                FoundCurrent = (today > seed.Paydate && today < seed.Paydate.AddDays(14));
            }
            seed.Paydate = seed.Paydate.AddDays((PreviousPeriodsToShow * 14) * -1);
        }
    }

    public class SettingsViewModel
    {
        public Guid UserID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Previous Periods To Show")]
        [Range(1, 4, ErrorMessage = "The value must be between {1} and {2}")]
        public int PreviousPeriodsToShow { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Checks To Show")]
        [Range(1, 8, ErrorMessage = "The value must be between {1} and {2}")]
        public int ChecksToShow { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Week Start Day")]
        [Range(0, 6, ErrorMessage = "The value must be between {1} and {2}")]
        public int WeekStartDay { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Days In Period")]
        [Range(1, 31, ErrorMessage = "The value must be between {1} and {2}")]
        public int DaysInPeriod { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Add Tithe")]
        public bool AddTithe { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Tithe Multiplier")]
        [Range(1, 2, ErrorMessage = "The value must be between {1} and {2}")]
        public decimal TitheMultiplier { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Tithe Background Color")]
        public string TitheBGColor { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Tithe Fore Color")]
        public string TitheForeColor { get; set; }

        public decimal PaycheckAmount { get; set; }
        
        public SettingsViewModel(Guid userid)
        {
        }

        public static SettingsViewModel GetSettings(Guid userid)
        {
            PBProxy p = new PBProxy();
            Setting temp = p.GetSettings(userid);
            Paycheck pc = p.GetPaychecks(userid).FirstOrDefault();
            p = null;

            SettingsViewModel Return = Mapper.Map<SettingsViewModel>(temp);

            Return.PaycheckAmount = pc == null ? 0 : pc.Amount;
            
            return Return;
        }
    }
}