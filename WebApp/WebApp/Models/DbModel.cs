using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class DbModel
    {
        public string EpisodeType { get; set; }
        private int transmissionId;
        public int TransmissionId { 
            get
            {
                return transmissionId;
            }
            set
            {
                if(value >= 0)
                    transmissionId = value;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        private string date;
        public string Date { 
            get { 
                return date; 
            } set { 
                if (value.Length == 14) 
                    date = value; 
                else throw new ArgumentOutOfRangeException(); 
            } 
        }
    }
}
