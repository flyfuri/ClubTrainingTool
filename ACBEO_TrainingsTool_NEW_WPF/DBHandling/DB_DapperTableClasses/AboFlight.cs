using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    class AboFlight
    {
        public int AboFlight_ID { get; set; }
        public int PilotID { get; set; }
        public int Abo_Year { get; set; }
        public int Abo_NrInYear { get; set; }
        public int FlightNrOnAbo { get; set; }
        public DateTime DateBought { get; set; }
        public DateTime DateFlightPayedWith { get; set; }
        public string Comment { get; set; }
        public int SellerID { get; set; }
        public string AboYearNr 
        { 
            get
            {
                return $"{Abo_Year.ToString()}-{Abo_NrInYear.ToString()}";
            }
        }
    }
}
