using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class EpisodeViewModel
    {
        public EpisodeViewModel(string episodeType, int transmission, int allTransmissions)
        {
            EpisodeType = episodeType;
            Transmissions = transmission;
            calcProcent(allTransmissions);
        }

        [DisplayName("Episode type")]
        [Tooltip("The type of episode")]
        public String EpisodeType { get; set; }

        [DisplayName("Transmission")]
        [Tooltip("The number of Transmissions where the episode type is present")]
        public int Transmissions { get; set; }

        [DisplayName("Transmission %")]
        [Tooltip("The procent of all transmissions where the episode is present")]
        public int ProcentTransmission { get; set; }

        public void calcProcent(int totalTransmission) { ProcentTransmission = (int)Math.Round( (double)(Transmissions) / (double)(totalTransmission) * 100, 0 ); }
    }

    public class TooltipAttribute : DescriptionAttribute
    {
        public TooltipAttribute()
            : base("")
        {

        }

        public TooltipAttribute(string description)
            : base(description)
        {

        }
    }
}