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
            var model = new List<ViewModels.BatteryViewModel>();

            if(dbModel.batteristatistikview != null)
            {
                foreach(var row in dbModel.batteristatistikview.OrderBy(m => m.estimatedDaysOfServiceLeft))
                {
                    var tempObj = new ViewModels.BatteryViewModel();

                    tempObj.PatientName = row.firstName + " " + row.lastName;
                    tempObj.PacemakerSerialNumber = row.pacemakerSerialNumber;
                    tempObj.DateOfImplantation = row.dateOfImplantation;
                    tempObj.EstimatedDaysOfServiceLeft = row.estimatedDaysOfServiceLeft;

                    model.Add(tempObj);
                }
            }

            return View(model);
        }

    }
}
