using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    class GeneratePatient
    {
        public static Patient getGeneratedPatient(Random rand, int number = 1000)
        {
            List<DateTime> dates = new List<DateTime>();
            List<double> voltages = new List<double>();

            double minDistance;
            DataPoint closest;

            DateTime d = DateTime.Now;
            double v = 5;

            List<DataPoint> predict = PredictData.getPredict();
            double predictPushed = 0;

            dates.Add(d);
            voltages.Add(v);

            int i = number;
            while(i > 0 && predictPushed != 100)
            {
                int j = 99;
                while(rand.Next(101) < j)
                {
                    d = d.AddHours(rand.Next(1, 47));
                    j--;
                }

                var dd = d.Subtract(dates.First());
                double days = dd.Days;

                double procentYear = days / 2922 * 100;

                // step 1
                minDistance = predict.Min(n => Math.Abs(procentYear - n.x));
                closest = predict.First(n => Math.Abs(procentYear - n.x) == minDistance);
                v = closest.y > v ? v : closest.y;

                // step 2
                double y = rand.NextDouble();
                while (y >= 1 || y <= 0)
                    y = rand.NextDouble();
                double w = Math.Sqrt(-2 * Math.Log(1 - y) * Math.Pow(0.2, 2));
                v -= w;

                // step 3
                //minDistance = predict.Where(n => n.y >= v).Min(n => Math.Abs(v - n.y));
                //closest = predict.First(n => Math.Abs(v - n.y) == minDistance);

                //predictPushed = closest.x;

                dates.Add(d);                
                voltages.Add(v);
                i--;
            }
            return new Patient(dates, voltages);
        }
    }
}
