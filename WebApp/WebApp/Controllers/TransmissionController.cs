using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class TransmissionController : Controller
    {
        WebApp.Models.pacemakerdataEntities db = new Models.pacemakerdataEntities();

        // GET: Transmission
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        // POST: Transmission
        [HttpPost]
        public ActionResult Index(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                Models.tblpatient patient = new Models.tblpatient();
                List<Models.tblepisodetype> episodes = new List<Models.tblepisodetype>();
                List<Models.linktransmissionepisode> linkTransmissionEpisode = new List<Models.linktransmissionepisode>();
                Models.tbltransmission transmission = new Models.tbltransmission();
                Models.tblpacemakerunittype pacemakerUnitType = new Models.tblpacemakerunittype();
                Models.tblpacemakerunit pacemakerUnit = new Models.tblpacemakerunit();
                

                

                // Read the file and display it line by line.
                System.IO.StreamReader f =
                   new System.IO.StreamReader(file.InputStream);

                string line =  f.ReadLine();
                while (line != null && !line.Contains("OBR|2|"))
                {
                    if(line.Contains("PID"))
                    {
                        var s = line.Split('|');

                        var name = s[5].Split('^');
                        patient.firstName = name[1];
                        patient.lastName = name[0];
      
                        patient.dateOfBirth = s[7];
                    }

                    if (line.Contains("720897"))
                    {
                        var s = line.Split('|');

                        var st = s[5].Split('^')[1];

                        pacemakerUnitType.type = st.Substring(st.IndexOf("TYPE_") + 5);
                    }

                    if(line.Contains("720898"))
                    {
                        var s = line.Split('|');

                        pacemakerUnitType.name = s[5];
                    }

                    if(line.Contains("720899"))
                    {
                        var s = line.Split('|');

                        pacemakerUnit.pacemakerSerialNumber = s[5];
                    }
                    if(line.Contains("720901"))
                    {
                        var s = line.Split('|');

                        pacemakerUnit.dateOfImplantation = s[5];
                    }

                    if(line.Contains("721536"))
                    {
                        var s = line.Split('|');

                        transmission.PMBatteryPercent = (int)Math.Round(double.Parse(s[5], CultureInfo.InvariantCulture), 0);
                    }

                    if(line.Contains("OBR|1|"))
                    {
                        var s = line.Split('|');

                        transmission.transmissionDate = s[7] != "" ? s[7] : "19900101000000";
                    }

                    if (line.Contains("739568"))
                    {
                        var s = line.Split('|');

                        episodes.Add(new Models.tblepisodetype() { episodeName = s[5].Split('^')[1].Substring(s[5].Split('^')[1].IndexOf("Epis_") + 5) });
                    }

                    if(line.Contains("739552"))
                    {
                        var s = line.Split('|');

                        linkTransmissionEpisode.Add(new Models.linktransmissionepisode(){ episodeDate = s[5] });
                    }

                    line = f.ReadLine();
                }

                pacemakerUnit.dateOfImplantation = pacemakerUnit.dateOfImplantation != null ? pacemakerUnit.dateOfImplantation : "19900101000000";

                if (db.tblpatient.Where(m => m.firstName == patient.firstName &&
                                               m.lastName == patient.lastName &&
                                               m.dateOfBirth == patient.dateOfBirth).Count() == 0)
                {
                    patient.ID = db.tblpatient.Max(m => m.ID) + 1;
                    db.tblpatient.Add(patient);
                }
                else
                {
                    patient.ID = db.tblpatient.Where(m => m.firstName == patient.firstName &&
                                                            m.lastName == patient.lastName &&
                                                            m.dateOfBirth == patient.dateOfBirth).FirstOrDefault().ID;
                }

                if(db.tblpacemakerunittype.Where(m => m.name == pacemakerUnitType.name && m.type == pacemakerUnitType.type).Count() == 0)
                {
                    pacemakerUnitType.ID = db.tblpacemakerunittype.Max(m => m.ID) + 1;
                    db.tblpacemakerunittype.Add(pacemakerUnitType);
                }
                else
                {
                    pacemakerUnitType.ID = db.tblpacemakerunittype.Where(m => m.name == pacemakerUnitType.name && 
                                                                        m.type == pacemakerUnitType.type).FirstOrDefault().ID;
                }

                pacemakerUnit.patientID = patient.ID;
                pacemakerUnit.pacemakerUnitTypeID = pacemakerUnitType.ID;
                if(db.tblpacemakerunit.Where(m => m.patientID == pacemakerUnit.patientID &&
                                                    m.pacemakerUnitTypeID == pacemakerUnit.pacemakerUnitTypeID &&
                                                    m.pacemakerSerialNumber == pacemakerUnit.pacemakerSerialNumber &&
                                                    m.dateOfImplantation == pacemakerUnit.dateOfImplantation).Count() == 0)
                {
                    pacemakerUnit.ID = db.tblpacemakerunit.Max(m => m.ID) + 1;
                    db.tblpacemakerunit.Add(pacemakerUnit);
                }
                else
                {
                    pacemakerUnit.ID = db.tblpacemakerunit.Where(m => m.patientID == pacemakerUnit.patientID &&
                                                    m.pacemakerUnitTypeID == pacemakerUnit.pacemakerUnitTypeID &&
                                                    m.pacemakerSerialNumber == pacemakerUnit.pacemakerSerialNumber &&
                                                    m.dateOfImplantation == pacemakerUnit.dateOfImplantation).FirstOrDefault().ID;
                }


                transmission.patientID = patient.ID;
                if(db.tbltransmission.Where(m => m.transmissionDate == transmission.transmissionDate &&
                                                    m.patientID == transmission.patientID &&
                                                    m.PMBatteryPercent == transmission.PMBatteryPercent).Count() == 0)
                {
                    transmission.ID = db.tbltransmission.Max(m => m.ID) + 1;
                    db.tbltransmission.Add(transmission);
                }
                else
                {
                    transmission.ID = db.tbltransmission.Where(m => m.transmissionDate == transmission.transmissionDate &&
                                                    m.patientID == transmission.patientID &&
                                                    m.PMBatteryPercent == transmission.PMBatteryPercent).FirstOrDefault().ID;
                }

                var nextId = db.tblepisodetype.Max(m => m.ID) + 1;
                foreach(var episode in episodes)
                {
                    if(db.tblepisodetype.Where(m => m.episodeName == episode.episodeName).Count() == 0)
                    {
                        episode.ID = nextId;
                        nextId++;
                        db.tblepisodetype.Add(episode);
                    }
                    else
                    {
                        episode.ID = db.tblepisodetype.Where(m => m.episodeName == episode.episodeName).FirstOrDefault().ID;
                    }
                }



                var i = 1;
                foreach(var link in linkTransmissionEpisode)
                {
                    link.episodeID = episodes[i - 1].ID;
                    link.transmissionID = transmission.ID;
                    if (db.linktransmissionepisode.Where(m => m.episodeDate == link.episodeDate &&
                                                            m.episodeID == link.episodeID &&
                                                            m.transmissionID == link.transmissionID).Count() == 0)
                    {
                        link.ID = db.linktransmissionepisode.Max(m => m.ID) + i;
                        db.linktransmissionepisode.Add(link);
                    }
                    else
                    {
                        link.ID = db.linktransmissionepisode.Where(m => m.episodeDate == link.episodeDate &&
                                                            m.episodeID == link.episodeID &&
                                                            m.transmissionID == link.transmissionID).FirstOrDefault().ID;
                    }
                    i++;
                }

                db.SaveChanges();

            }

            return View("Succes");
        }
    }
}