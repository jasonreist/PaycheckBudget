using System;
using System.Collections.Generic;

namespace PB.UI.Models
{
    public class EFCustomPaycheck
    {
        public int Id { get; set; }
        public int PaycheckId { get; set; }
        public DateTime PaycheckDate { get; set; }
        public decimal Amount { get; set; }
    }
}