using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.Common.Helpers
{
    public class BootstrapLI
    {
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string Class { get { return this.Selected ? "active" : ""; } set { } }
    }
}
