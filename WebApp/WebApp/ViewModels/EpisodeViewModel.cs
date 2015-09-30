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
        public String EpisodeType { get; set; }

        private int transmission;
        [DisplayName("Transmissions")]
        [Helpers.Tooltip("The number of Transmissions where the episode type is present")]
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
    }
}