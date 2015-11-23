using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class BatteryGraphViewModel
    {
        public List<int> ByYear { get; set; }
        public List<int> ByMonth { get; set; }
        public List<string> MonthName { get; set; }
        public string CurrentYear { get; set; }
    }
}
