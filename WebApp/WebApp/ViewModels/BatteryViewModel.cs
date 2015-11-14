using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class BatteryViewModel
    {
        public string PatientName { get; set; }
        public string DateOfImplantation { get; set; }
        public int PacemakerSerialNumber { get; set; }
        public int EstimatedDaysOfServiceLeft { get; set; }
    }
}
