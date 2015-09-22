using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace GenerateData
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            DateTime d = DateTime.Now;
            double v = 5;
            
            List<DateTime> list = new List<DateTime>();
            List<double> voltage = new List<double>();

            list.Add(d);
            voltage.Add(v);

            int i = 200;
            while(i > 0)
            {
                int j = 99;
                while(rand.Next(101) < j)
                {
                    d = d.AddHours(rand.Next(1, 47));
                    j--;
                }
                list.Add(d);
                v = rand.Next(6) == 5 ? v - 0.1 : v;
                voltage.Add(v);
                i--;
            }

            var excelApp = new Excel.Application();
            // Add a new Excel workbook.
            excelApp.Workbooks.Add();
            excelApp.Visible = true;
            excelApp.Range["A1"].Value = "Date";
            excelApp.Range["B1"].Value = "Voltage";
            excelApp.Range["A2"].Select();

            for(i=0; i < list.Count; i++)
            {
                excelApp.ActiveCell.Value = list[i].ToString("yyyyMMddHHmmss");
                excelApp.ActiveCell.Offset[0, 1].Select();
                excelApp.ActiveCell.Value = voltage[i];
                excelApp.ActiveCell.Offset[1, -1].Select();
            }
        }
    }
}
