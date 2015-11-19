using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class BatteryController : Controller
    {
        private Models.pacemakerdataEntities dbModel = new Models.pacemakerdataEntities();

        public BatteryController()
        {
            try
            {
                var test = dbModel.batteristatistikview.Count();
            }
            catch (Exception e)
            {
                ISet<Models.batteristatistikview> test = new HashSet<Models.batteristatistikview>();
                dbModel.batteristatistikview = test as System.Data.Entity.DbSet<Models.batteristatistikview>;
            }
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            var model = new ViewModels.BatteryGraphViewModel();
            model.ByYear = new List<int>();
            model.ByMonth = new List<int>();
            model.MonthName = new List<string>();

            var date = DateTime.Now;

            for (var i = 0; i < 12; i++)
            {
                model.ByYear.Add(0);
                model.ByMonth.Add(0);
                model.MonthName.Add(date.AddMonths(i).ToString("MMMM yyyy"));
            }

            var thisYear = date.Year;
            var thisMonth = date.Month;

            model.CurrentYear = thisYear.ToString();

            if(dbModel.batteristatistikview == null)
            {
                return View("Error");
            }

            foreach(var row in dbModel.batteristatistikview.OrderBy(m => m.estimatedDaysOfServiceLeft))
            {
                var tempDate = date.AddDays(row.estimatedDaysOfServiceLeft);

                model.ByYear[tempDate.Year - thisYear]++;
                if (tempDate.Year == thisYear || tempDate.Year == thisYear + 1)
                    model.ByMonth[tempDate.Month - thisMonth < 0 ? tempDate.Month - thisMonth + 12 : tempDate.Month - thisMonth]++;
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult getBatteryData()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            var dateNow = DateTime.Now;

            if(dbModel.batteristatistikview != null)
            {
                foreach(var modelItem in dbModel.batteristatistikview.OrderBy(m => m.estimatedDaysOfServiceLeft))
                {
                    var key = dateNow.AddDays(modelItem.estimatedDaysOfServiceLeft).ToString("dd-MM-yyyy");

                    if (result.ContainsKey(key))
                    {
                        result[key]++;
                    }
                    else
                    {
                        result.Add(key, 1);
                    }

                }
            }

            var resultJS = new List<ViewModels.BatteryJSViewModel>();

            foreach(var obj in result)
            {
                resultJS.Add(new ViewModels.BatteryJSViewModel() { date = obj.Key, value = obj.Value });
            }

            return Json(resultJS, JsonRequestBehavior.AllowGet);
        }

    }
}
