using System;
using System.Collections.Generic;

namespace PB.UI.Models
{
    public class EFCustomBill
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
    }
}