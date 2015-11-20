using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WebApp.Helpers
{
    // Class code found at http://stackoverflow.com/a/3711878
    public class TooltipAttribute : DescriptionAttribute
    {
        public TooltipAttribute()
            : base("")
        {

        }

        public TooltipAttribute(string description)
            : base(description)
        {

        }
    }
}
