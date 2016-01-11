using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects.SqlClient;

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

        // GET: Episode
        [HttpGet]
        public ActionResult Index()
        {
            List<ViewModels.EpisodeViewModel> eMList = new List<ViewModels.EpisodeViewModel>();

            if (dbModel.pacemakerdataview == null)
            {
                return View("Error");
            }

            foreach (var episode in dbModel.pacemakerdataview.Where(m => !(m.episodeName == "" || m.episodeName == "None") ).Select(m => m.episodeName).Distinct())
            {
                var tempObj = new ViewModels.EpisodeViewModel();

                tempObj.EpisodeType = episode;

                eMList.Add(tempObj);
            }

            foreach (var item in eMList)
            {
                item.TotalTransmissions = dbModel.pacemakerdataview.Select(m => m.ID).Distinct().Count();
                item.Transmissions = dbModel.pacemakerdataview.Where(modelItem => modelItem.episodeName == item.EpisodeType)
                                            .Select(modelItem => modelItem.ID).Distinct().Count();

                item.ProcentTransmission = Helpers.ModelHelpers.calcProcent(
                    dbModel.pacemakerdataview.Where(modelItem => modelItem.episodeName == item.EpisodeType).Select(modelItem => modelItem.ID).Distinct().Count(),
                    item.TotalTransmissions);

                var d = dbModel.pacemakerdataview.Min(m => m.episodeDate);
                item.EpisodeDateMin = d.Substring(6, 2) + "/" + d.Substring(4, 2) + "/" + d.Substring(0, 4);

                var de = dbModel.pacemakerdataview.Max(m => m.episodeDate);
                item.EpisodeDateMax = de.Substring(6, 2) + "/" + de.Substring(4, 2) + "/" + de.Substring(0, 4);
            }

            return View("Index", eMList);
        }

        [HttpGet]
        public JsonResult getPatientTags()
        {
            List<string> result = new List<string>();

            if (dbModel.pacemakerdataview != null)
            {
                var model = dbModel.pacemakerdataview;
                foreach (var item in model)
                {
                    if (!result.Contains(item.firstName))
                    {
                        result.Add(item.firstName);
                    }
                    if (!result.Contains(item.lastName))
                    {
                        result.Add(item.lastName);
                    }
                    if (!result.Contains(item.name))
                    {
                        result.Add(item.name);
                    }
                    if (!result.Contains(item.pacemakerSerialNumber.ToString()))
                    {
                        result.Add(item.pacemakerSerialNumber.ToString());
                    }
                    if (!result.Contains(item.type))
                    {
                        result.Add(item.type);
                    }
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getNewModel(string episodes, string datesSelected, string patientsChecked)
        {
            string[] episodeList = episodes.Split('|');
            string[] dateList = datesSelected.Split('|');
            string db = dateList[0];
            string de = dateList[1];
            string[] patientList = patientsChecked.Split('|');
            int transmissions;

            List<ViewModels.EpisodeViewModel> eMList = new List<ViewModels.EpisodeViewModel>();

            if (dbModel.pacemakerdataview != null)
            {
                transmissions = dbModel.pacemakerdataview.Where(m => (m.transmissionDate.CompareTo(db) >= 0 && m.transmissionDate.CompareTo(de) <= 0) &&
                    patientList.Contains(m.firstName + " " + m.lastName)).Select(m => m.ID).Distinct().Count();
                var model = dbModel.pacemakerdataview.Where(m => episodeList.Contains(m.episodeName) &&
                    (m.transmissionDate.CompareTo(db) >= 0 && m.transmissionDate.CompareTo(de) <= 0) &&
                    patientList.Contains(m.firstName + " " + m.lastName));

                foreach (var episode in model.Where(m => m.episodeName != "").Select(m => m.episodeName).Distinct())
                {
                    var tempObj = new ViewModels.EpisodeViewModel();

                    tempObj.EpisodeType = episode;

                    eMList.Add(tempObj);
                }

                foreach (var item in eMList)
                {
                    item.TotalTransmissions = transmissions;
                    item.Transmissions = model.Where(modelItem => modelItem.episodeName == item.EpisodeType)
                                                .Select(modelItem => modelItem.ID).Distinct().Count();

                    item.ProcentTransmission = Helpers.ModelHelpers.calcProcent(
                        model.Where(modelItem => modelItem.episodeName == item.EpisodeType).Select(modelItem => modelItem.ID).Distinct().Count(),
                        transmissions);
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
                foreach (var episode in dbModel.pacemakerdataview.Where(m => !(m.episodeName == "" || m.episodeName == "None") ).Select(m => m.episodeName).Distinct())
                {
                    result.Add(episode);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getPatients(string filter)
        {
            List<ViewModels.PatientViewModel> result = new List<ViewModels.PatientViewModel>();

            if (dbModel.pacemakerdataview != null)
            {
                var model = dbModel.pacemakerdataview.Where(m => true);
                if (filter != null && filter.Length >= 2)
                {
                    model = model.Where(m => m.firstName.ToLower().Contains(filter.ToLower()) || 
                                        m.lastName.ToLower().Contains(filter.ToLower()) ||
                                        m.pacemakerSerialNumber.ToLower().Contains(filter.ToLower()) ||
                                        m.name.ToLower().Contains(filter.ToLower()) || 
                                        m.type.ToLower().Contains(filter.ToLower()));
                }
                foreach (var patient in model.Select(m => new { m.firstName, m.lastName, m.name, m.pacemakerSerialNumber, m.type, m.dateOfBirth }).Distinct())
                {
                    result.Add(new ViewModels.PatientViewModel() { PatientName = patient.firstName + " " + patient.lastName, 
                                                                    PacemakerName = patient.name, 
                                                                    PacemakerSerialnumber = patient.pacemakerSerialNumber, 
                                                                    PacemakerType = patient.type,
                                                                    SecurityNumber = patient.dateOfBirth
                                                                });
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult getGraphData(string episodeType, string db, string de, string patientsChecked)
        {
            ViewModels.GraphViewModel result = new ViewModels.GraphViewModel();

            string[] patientList = patientsChecked.Split('|');

            if (dbModel.pacemakerdataview != null)
            {
                var data = dbModel.pacemakerdataview.Where(m => m.transmissionDate.CompareTo(db) >= 0 && 
                                                            m.transmissionDate.CompareTo(de) <= 0 && 
                                                            m.episodeName == episodeType && 
                                                            patientList.Contains(m.firstName + " " + m.lastName)
                                                            );

                if (data.Count() > 0)
                {
                    result.NewestYear = data.Max(m => m.transmissionDate);
                    result.NewestYear = result.NewestYear.Substring(0, 4);
                }
                else
                {
                    result.NewestYear = DateTime.Now.ToString("yyyy");
                }

                List<int> transmissionId = new List<int>();

                foreach (var model in data)
                {
                    if (model.transmissionDate.Substring(0, 4) == result.NewestYear && !transmissionId.Contains(model.ID))
                    {
                        transmissionId.Add(model.ID);
                        switch (model.transmissionDate.Substring(4, 2))
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