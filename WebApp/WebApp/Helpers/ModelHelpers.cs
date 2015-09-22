using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    public class ModelHelpers
    {
        public static int calcProcent(int transmissions, int totalTransmission) 
        { 
            return (int)Math.Round((double)(transmissions) / (double)(totalTransmission) * 100, 0); 
        }
    }
}
