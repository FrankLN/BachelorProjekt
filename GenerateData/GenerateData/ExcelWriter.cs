using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace GenerateData
{
    class ExcelWriter
    {
        public static void Write(List<Patient> patients) {
            var excelApp = new Excel.Application();
            // Add a new Excel workbook.
            excelApp.Workbooks.Add();
            excelApp.Visible = true;

            var p = 1;
            foreach(var patient in patients)
            {
                excelApp.Range["A1"].Select();
                excelApp.ActiveCell.Offset[0, (p-1)*2].Select();

                excelApp.ActiveCell.Value = "patient " + p;
                excelApp.ActiveCell.Offset[1, 0].Select();
                excelApp.ActiveCell.Value = "Date";
                excelApp.ActiveCell.Offset[0, 1].Select();
                excelApp.ActiveCell.Value = "Voltage";
                excelApp.ActiveCell.Offset[1, -1].Select();

                for(int i=0; i < patient.DateTimes.Count; i++)
                {
                    excelApp.ActiveCell.Value = patient.DateTimes[i].ToString("yyyyMMddHHmmss");
                    excelApp.ActiveCell.Offset[0, 1].Select();
                    excelApp.ActiveCell.Value = patient.Voltages[i];
                    excelApp.ActiveCell.Offset[1, -1].Select();
                }
                p++;
            }
        }
    }
}
