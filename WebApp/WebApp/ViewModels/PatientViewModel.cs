using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class PatientViewModel
    {
        public string PatientName { get; set; }
        public string SecurityNumber { get; set; }
        public string PacemakerSerialnumber { get; set; }
        public string PacemakerType { get; set; }
        public string PacemakerName { get; set; }
    }
}
