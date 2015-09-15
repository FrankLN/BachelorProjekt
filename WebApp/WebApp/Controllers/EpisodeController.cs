using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class EpisodeController : Controller
    {
        public List<Models.DbModel> getFromDB()
        {
            List<Models.DbModel> db = new List<Models.DbModel>();

            db.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 1 });
            db.Add(new Models.DbModel() { EpisodeType = "Shock", TransmissionId = 1 });
            db.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 2 });
            db.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 2 });
            db.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 3 });
            db.Add(new Models.DbModel() { EpisodeType = "Shock", TransmissionId = 4 });
            db.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 4 });
            db.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 5 });
            db.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 5 });
            db.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 5 });
            db.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 6 });
            db.Add(new Models.DbModel() { EpisodeType = "Power failure", TransmissionId = 7 });

            return db;
        }

        // GET: Episode
        public ActionResult Index()
        {
            var data = getFromDB();
            List<ViewModels.EpisodeViewModel> eMList = new List<ViewModels.EpisodeViewModel>();

            foreach(var model in data.Select(m => m.EpisodeType).Distinct() )
            {
                eMList.Add(new ViewModels.EpisodeViewModel(model, data.Count(modelItem => modelItem.EpisodeType == model), data.Select(m => m.TransmissionId).Distinct().Count()));
            }


            ViewBag.Header = "Episode Administration";
            return View(eMList);
        }
    }
}