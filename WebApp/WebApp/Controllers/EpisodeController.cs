using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class EpisodeController : Controller
    {
        private Models.pacemakerdataEntities dbModel = new WebApp.Models.pacemakerdataEntities();
        
        
        public EpisodeController()
        {
            try
            {
                var test = dbModel.pacemakerdataview.Count();
            }
            catch(Exception e)
            {
                ISet<Models.pacemakerdataview> test = new HashSet<Models.pacemakerdataview>();
                dbModel.pacemakerdataview = test as System.Data.Entity.DbSet<Models.pacemakerdataview>;
            }
        }

        // Mockup data created
        //public System.Data.Entity.DbSet<Models.pacemakerdataview> getFromDB()
        //{
        //    System.Data.Entity.DbSet<Models.pacemakerdataview> result;

            //result.Add(new Models.pacemakerdataview() { episodeName = "Aterial fibrillation", ID = 1, episodeDate = "20150918153452", firstName = "Egon", lastName = "Olsen" });
            //result.Add(new Models.DbModel() { EpisodeType = "Shock", TransmissionId = 1, Date = "20150926103452", Patient = "Egon Olsen" });
            //result.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 2, Date = "20150927213452", Patient = "Benny" });
            //result.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 2, Date = "20151009063452", Patient = "Benny" });
            //result.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 3, Date = "20151016123452", Patient = "Kjeld" });
            //result.Add(new Models.DbModel() { EpisodeType = "Shock", TransmissionId = 4, Date = "20151020053452", Patient = "Egon Olsen" });
            //result.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 4, Date = "20151108013452", Patient = "Egon Olsen" });
            //result.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 5, Date = "20151112153452", Patient = "Benny" });
            //result.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 5, Date = "20151122053452", Patient = "Benny" });
            //result.Add(new Models.DbModel() { EpisodeType = "Other", TransmissionId = 5, Date = "20151126043452", Patient = "Benny" });
            //result.Add(new Models.DbModel() { EpisodeType = "Aterial fibrillation", TransmissionId = 6, Date = "20151204073452", Patient = "Kjeld" });
            //result.Add(new Models.DbModel() { EpisodeType = "Power failure", TransmissionId = 7, Date = "20151220113452", Patient = "Egon Olsen" });


        //    return result;
        //}

        // GET: Episode
        [HttpGet]
        public ActionResult Index()
        {
            List<ViewModels.EpisodeViewModel> eMList = new List<ViewModels.EpisodeViewModel>();

            string de = "00000000000000", d = "00000000000000";

            if (dbModel.pacemakerdataview != null)
            {
                foreach (var episode in dbModel.pacemakerdataview.Select(m => m.episodeName).Distinct())
                {
                    var tempObj = new ViewModels.EpisodeViewModel();

                    tempObj.EpisodeType = episode;

                    eMList.Add(tempObj);
                }

                foreach (var item in eMList)
                {
                    item.Transmissions = dbModel.pacemakerdataview.Where(modelItem => modelItem.episodeName == item.EpisodeType)
                                                .Select(modelItem => modelItem.ID).Distinct().Count();

                    item.ProcentTransmission = Helpers.ModelHelpers.calcProcent(
                        dbModel.pacemakerdataview.Where(modelItem => modelItem.episodeName == item.EpisodeType).Select(modelItem => modelItem.ID).Distinct().Count(),
                        dbModel.pacemakerdataview.Select(m => m.ID).Distinct().Count());

                    List<string> dates = new List<string>();
                    List<string> patients = new List<string>();
                    foreach(var episode in dbModel.pacemakerdataview.Where(m => m.episodeName == item.EpisodeType))
                    {
                        dates.Add(episode.episodeDate);
                        patients.Add(episode.firstName + " " + episode.lastName + "|");
                    }

                    item.Dates = dates;
                    item.Patients = patients;
                }

                d = dbModel.pacemakerdataview.Min(m => m.episodeDate);
                d = d.Substring(6, 2) + "/" + d.Substring(4, 2) + "/" + d.Substring(0, 4);

                de = dbModel.pacemakerdataview.Max(m => m.episodeDate);
                de = de.Substring(6, 2) + "/" + de.Substring(4, 2) + "/" + de.Substring(0, 4);
            }

            ViewBag.DateBegin = d; //20150918153452 ~ 18/09/2015
            ViewBag.DateEnd = de; //20151220113452 ~ 20/12/2015
            ViewBag.Header = "Episode Administration";
            return View("Index", eMList);
        }

        [HttpGet]
        public JsonResult getNewModel(string episodes, string datesSelected, string patientsChecked)
        {
            string[] episodeList = episodes.Split('|');
            string[] dateList = datesSelected.Split('|');
            string db = dateList[0];
            string de = dateList[1];
            string[] patientList = patientsChecked.Split('|');

            List<ViewModels.EpisodeViewModel> eMList = new List<ViewModels.EpisodeViewModel>();

            if (dbModel.pacemakerdataview != null)
            {
                var model = dbModel.pacemakerdataview.Where(m => episodeList.Contains(m.episodeName) &&
                    (m.episodeDate.CompareTo(db) >= 0 &&  m.episodeDate.CompareTo(de) <= 0) &&
                    patientList.Contains(m.firstName + " " + m.lastName));


                foreach (var episode in model.Select(m => m.episodeName).Distinct())
                {
                    var tempObj = new ViewModels.EpisodeViewModel();

                    tempObj.EpisodeType = episode;

                    eMList.Add(tempObj);
                }

                foreach (var item in eMList)
                {
                    item.Transmissions = model.Where(modelItem => modelItem.episodeName == item.EpisodeType)
                                                .Select(modelItem => modelItem.ID).Distinct().Count();

                    item.ProcentTransmission = Helpers.ModelHelpers.calcProcent(
                        model.Where(modelItem => modelItem.episodeName == item.EpisodeType).Select(modelItem => modelItem.ID).Distinct().Count(),
                        model.Select(m => m.ID).Distinct().Count());

                    List<string> dates = new List<string>();
                    List<string> patients = new List<string>();
                    foreach (var episode in model.Where(m => m.episodeName == item.EpisodeType))
                    {
                        dates.Add(episode.episodeDate);
                        patients.Add(episode.firstName + " " + episode.lastName + "|");
                    }

                    item.Dates = dates;
                    item.Patients = patients;
                }
            }

            return Json(eMList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getEpisodes()
        {
            List<string> result = new List<string>();

            if (dbModel.pacemakerdataview != null)
            {
                foreach (var episode in dbModel.pacemakerdataview.Select(m => m.episodeName).Distinct())
                {
                    result.Add(episode);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getPatients()
        {
            List<string> result = new List<string>();

            if (dbModel.pacemakerdataview != null)
            {
                foreach (var patient in dbModel.pacemakerdataview.Select(m => new { m.firstName, m.lastName }).Distinct())
                {
                    result.Add(patient.firstName + " " + patient.lastName);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult getGraphData(string episodeType, string db, string de)
        {
            ViewModels.GraphViewModel result = new ViewModels.GraphViewModel();

            if (dbModel.pacemakerdataview != null)
            {
                var data = dbModel.pacemakerdataview.Where(m => m.episodeDate.CompareTo(db) >= 0 && m.episodeDate.CompareTo(de) <= 0 && m.episodeName == episodeType);

                result.newestYear = data.Max(m => m.episodeDate);
                result.newestYear = result.newestYear.Substring(0, 4);

                List<int> transmissionId = new List<int>();

                foreach (var model in data)
                {
                    if (model.episodeDate.Substring(0, 4) == result.newestYear && !transmissionId.Contains(model.ID))
                    {
                        transmissionId.Add(model.ID);
                        switch (model.episodeDate.Substring(4, 2))
                        {
                            case "01":
                                result.Jan++;
                                break;
                            case "02":
                                result.Feb++;
                                break;
                            case "03":
                                result.Mar++;
                                break;
                            case "04":
                                result.Apr++;
                                break;
                            case "05":
                                result.May++;
                                break;
                            case "06":
                                result.Jun++;
                                break;
                            case "07":
                                result.Jul++;
                                break;
                            case "08":
                                result.Aug++;
                                break;
                            case "09":
                                result.Sep++;
                                break;
                            case "10":
                                result.Oct++;
                                break;
                            case "11":
                                result.Nov++;
                                break;
                            case "12":
                                result.Dec++;
                                break;

                        }

                    }
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}