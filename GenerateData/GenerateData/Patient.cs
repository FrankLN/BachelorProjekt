using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    class Patient
    {
        public List<DateTime> DateTimes;
        public List<double> Voltages;

        public Patient(List<DateTime> dateTimes, List<double> voltages)
        {
            DateTimes = new List<DateTime>();
            foreach (var dt in dateTimes)
                DateTimes.Add(dt);

            Voltages = new List<double>();
            foreach (var voltage in voltages)
                Voltages.Add(voltage);
        }
    }
}
