using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WebApp.Helpers
{
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
