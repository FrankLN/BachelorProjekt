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
        [Tooltip("The type of episode")]
        public String EpisodeType { get; set; }

        [DisplayName("Transmission")]
        [Tooltip("The number of Transmissions where the episode type is present")]
        public int Transmissions { get; set; }

        [DisplayName("Transmission %")]
        [Tooltip("The procent of all transmissions where the episode is present")]
        public int ProcentTransmission { get; set; }
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