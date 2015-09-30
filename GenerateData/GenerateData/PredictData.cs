using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    class PredictData
    {
        public static List<DataPoint> getPredict() {
            List<DataPoint> predict = new List<DataPoint>();

            predict.Add(new DataPoint { x = 0, y = 5 });
            predict.Add(new DataPoint { x = 1, y = 4.765625 });
            predict.Add(new DataPoint { x = 5, y = 4.625 });
            predict.Add(new DataPoint { x = 10, y = 4.609375 });
            predict.Add(new DataPoint { x = 15, y = 4.6016 });
            predict.Add(new DataPoint { x = 20, y = 4.59375 });
            predict.Add(new DataPoint { x = 25, y = 4.5859 });
            predict.Add(new DataPoint { x = 30, y = 4.578125 });
            predict.Add(new DataPoint { x = 35, y = 4.5625 });
            predict.Add(new DataPoint { x = 40, y = 4.546875 });
            predict.Add(new DataPoint { x = 45, y = 4.5391 });
            predict.Add(new DataPoint { x = 50, y = 4.53125 });
            predict.Add(new DataPoint { x = 55, y = 4.5156 });
            predict.Add(new DataPoint { x = 60, y = 4.5 });
            predict.Add(new DataPoint { x = 65, y = 4.4688 });
            predict.Add(new DataPoint { x = 70, y = 4.4375 });
            predict.Add(new DataPoint { x = 75, y = 4.3906 });
            predict.Add(new DataPoint { x = 80, y = 4.34375 });
            predict.Add(new DataPoint { x = 85, y = 4.1875 });
            predict.Add(new DataPoint { x = 90, y = 4.03125 });
            predict.Add(new DataPoint { x = 95, y = 3.875 });
            predict.Add(new DataPoint { x = 100, y = 3.4375 });

            return predict;
        }
    }
}
