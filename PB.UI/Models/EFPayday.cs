using System;
using System.Collections.Generic;

namespace PB.UI.Models
{
    public class EFPayday
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public System.DateTime Paydate { get; set; }
    }
}