using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class EpisodeController : Controller
    {
        // Mockup data created
        protected List<Models.DbModel> getFromDB()
        {
            List<Models.DbModel> db = new List<Models.DbModel>();

            db.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 1, Date = "20151220113452" });
            db.Add(new Models.DbModel() { EpisodeType = "Shock", TransmissionId = 1, Date = "20150926103452" });
            db.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 2, Date = "20150927213452" });
            db.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 2, Date = "20151009063452" });
            db.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 3, Date = "20151016123452" });
            db.Add(new Models.DbModel() { EpisodeType = "Shock", TransmissionId = 4	, Date = "20151020053452" });
            db.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 4	, Date = "20151108013452" });
            db.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 5	, Date = "20151112153452" });
            db.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 5	, Date = "20151122053452" });
            db.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 5	, Date = "20151126043452" });
            db.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 6	, Date = "20151204073452" });
            db.Add(new Models.DbModel() { EpisodeType = "Power failure", TransmissionId = 7, Date = "20150918153452" });


            return db;
        }

        // GET: Episode
        [HttpGet]
        public ActionResult Index()
        {
            var data = getFromDB();
            List<ViewModels.EpisodeViewModel> eMList = new List<ViewModels.EpisodeViewModel>();

            foreach(var model in data.Select(m => m.EpisodeType).Distinct() )
            {
                eMList.Add(new ViewModels.EpisodeViewModel() {  EpisodeType = model, 
                                                                Transmissions = data.Where(modelItem => modelItem.EpisodeType == model).Select(modelItem => modelItem.TransmissionId).Distinct().Count(), 
                                                                ProcentTransmission = Helpers.ModelHelpers.calcProcent(data.Where(modelItem => modelItem.EpisodeType == model).Select(modelItem => modelItem.TransmissionId).Distinct().Count(), 
                                                                                                                        data.Select(m => m.TransmissionId).Distinct().Count())
                                                                });
            }

            string d = data.Min(m => m.Date);
            d = d.Substring(6, 2) + "/" + d.Substring(4, 2) + "/" + d.Substring(0, 4);

            string de = data.Max(m => m.Date);
            de = de.Substring(6, 2) + "/" + de.Substring(4, 2) + "/" + de.Substring(0, 4);

            ViewBag.DateBegin = d; //20150918153452 ~ 18/09/2015
            ViewBag.DateEnd = de; //20151220113452 ~ 20/12/2015
            ViewBag.Header = "Episode Administration";
            return View("Index", eMList);
        }

        // POST: Episode/index?db=20150918153452&de=20150918153452
        [HttpPost]
        public ActionResult Index(string db, string de)
        {
            var data2 = getFromDB();
            var data = data2.Where(m => long.Parse(m.Date) >= long.Parse(db) && long.Parse(m.Date) <= long.Parse(de));
            List<ViewModels.EpisodeViewModel> eMList = new List<ViewModels.EpisodeViewModel>();

            foreach (var model in data.Select(m => m.EpisodeType).Distinct())
            {
                eMList.Add(new ViewModels.EpisodeViewModel()
                {
                    EpisodeType = model,
                    Transmissions = data.Count(modelItem => modelItem.EpisodeType == model),
                    ProcentTransmission = Helpers.ModelHelpers.calcProcent(data.Count(modelItem => modelItem.EpisodeType == model),
                                                                            data.Select(m => m.TransmissionId).Distinct().Count()),
                });
            }

            db = db.Substring(6, 2) + "/" + db.Substring(4, 2) + "/" + db.Substring(0, 4);
            de = de.Substring(6, 2) + "/" + de.Substring(4, 2) + "/" + de.Substring(0, 4);

            ViewBag.DateBegin = db;
            ViewBag.DateEnd = de; 
            ViewBag.Header = "Episode Administration";
            return View("Index", eMList);
        }
    }
}