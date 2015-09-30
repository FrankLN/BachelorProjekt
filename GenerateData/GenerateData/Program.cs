using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            List<Patient> patients = new List<Patient>();

            int p = 0;
            while(p < 5)
            {
                var newPatient = GeneratePatient.getGeneratedPatient(rand);

                patients.Add(newPatient);
                p++;
            }
            ExcelWriter.Write(patients);
        }
    }
}
