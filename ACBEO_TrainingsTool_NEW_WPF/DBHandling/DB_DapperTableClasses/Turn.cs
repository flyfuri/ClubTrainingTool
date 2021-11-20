using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    class Turn
    {
        public int TurnID { get; set; }
        public int TrainingID { get; set; }
        public int ParticipantID { get; set; }
        public int TurnNr { get; set; }
        public string Flight { get; set; }

        public string getFlightShort
        {
            get
            {
                return (Flight.Substring(0, Flight.IndexOf(":")));
            }
        }
    }
}