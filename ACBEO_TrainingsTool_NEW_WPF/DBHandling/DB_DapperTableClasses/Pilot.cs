using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainingsraport_Tool
{
    class Pilot
    {
        private bool disactive;

        public int Pilot_ID { get; set; }
        public string LicensNr { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ComplNameAndLicence
        {
            get
            {
                return $"{FirstName} {LastName} ({LicensNr})";
            }
        }
        public bool Disactivated { get; set; }
    }
}
