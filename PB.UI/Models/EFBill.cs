using System;
using System.Collections.Generic;

namespace PB.UI.Models
{
    public class EFBill
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int DueDay { get; set; }
        public Nullable<int> StyleId { get; set; }

        public virtual ICollection<EFCustomBill> CustomBills { get; set; }
    }
}