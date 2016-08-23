using System;
using System.Collections.Generic;

namespace PB.UI.Models
{
    public class EFPaycheck
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}