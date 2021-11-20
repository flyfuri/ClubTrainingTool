using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainingsraport_Tool
{
    class ActualAppSettings
    {
        public void initSettings()
        {
            actYEAR = DateTime.Today.Year;
        }

        public int actYEAR { get; set; }

    }
}
