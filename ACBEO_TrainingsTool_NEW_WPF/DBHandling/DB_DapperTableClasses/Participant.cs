using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    public class Participant
    {
        public int ParticipantID { get; set; }
        public int TrainingID { get; set; }
        public int PilotID { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string LicensNr { get; }

        public string ComplNameAndLicence
        {
            get
            {
                return $"{FirstName} {LastName} ({LicensNr})";
            }
        }
    }
}
