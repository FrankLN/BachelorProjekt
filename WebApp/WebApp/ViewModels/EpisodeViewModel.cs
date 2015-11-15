using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class EpisodeViewModel
    {
        [DisplayName("Episode type")]
        [Helpers.Tooltip("The type of episode")]
        public string EpisodeType { get; set; }

        private int transmission;
        [DisplayName("Transmissions")]
        [Helpers.Tooltip("The number of Transmissions where the episode type is present\nThe number in parentes is the total number of transmission currently shown")]
        public int Transmissions
        {
            get
            {
                return transmission;
            }
            set
            {
                if(value >= 0)
                    transmission = value;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int procentTransmission;
        [DisplayName("Transmission %")]
        [Helpers.Tooltip("The procent of all transmissions where the episode is present")]
        public int ProcentTransmission { 
            get
            {
                return procentTransmission;
            }
            set
            {
                if (value >= 0 && value <= 100)
                    procentTransmission = value;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        public List<string> Dates { get; set; }
        public List<string> Patients { get; set; }
        public int TotalTransmissions { get; set; }
    }
}