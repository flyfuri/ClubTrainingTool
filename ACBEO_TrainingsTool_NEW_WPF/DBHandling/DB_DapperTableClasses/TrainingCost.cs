
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    class TrainingCost
    {
        public int TrainingCostID { get; set; }
        public DateTime TrainingDate { get; set; }
        public decimal Betrag { get; set; }
        public string Kommentar { get; set; }
        public string BelegNr { get; set; }
        public string BelegPhotoName { get; set; }

        public List<string> valueList
        {
            get
            {
                List<string> tempList = new List<string>();
                tempList.Clear();
                tempList.Add(Betrag.ToString());
                tempList.Add(Kommentar);
                tempList.Add(BelegNr);
                tempList.Add(BelegPhotoName);
                return tempList;
            }
        }
    }
}
