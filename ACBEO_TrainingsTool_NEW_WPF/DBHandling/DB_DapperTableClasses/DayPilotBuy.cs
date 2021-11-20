using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    class DayPilotBuy
    {
        public int DayPilotBuyID { get; set; }
        public int TrainingID { get; set; }
        public int ParticipantID { get; set; }
        public decimal LifewestBuy { get; set; }
        public decimal LifewestRent { get; set; }
        public decimal YearFee { get; set; }
        public decimal DayFee { get; set; }
        public decimal AxalpWeekFee { get; set; }
        public decimal Others { get; set; }
        public string Remarks { get; set; }

        public List<string> valueList
        {
            get
            {
                List<string> tempList = new List<string>();
                tempList.Clear();
                tempList.Add(LifewestBuy.ToString());
                tempList.Add(LifewestRent.ToString());
                tempList.Add(YearFee.ToString());
                tempList.Add(DayFee.ToString());
                tempList.Add(AxalpWeekFee.ToString());
                tempList.Add(Others.ToString());
                tempList.Add(Remarks);
                return tempList;
            }
        }
        public decimal totalCostBuyWithoutAbos
        {
            get
            {
                decimal totalBuyToPay = 0;
                totalBuyToPay = totalBuyToPay + LifewestBuy;
                totalBuyToPay = totalBuyToPay + LifewestRent;
                totalBuyToPay = totalBuyToPay + YearFee;
                totalBuyToPay = totalBuyToPay + DayFee;
                totalBuyToPay = totalBuyToPay + AxalpWeekFee;
                totalBuyToPay = totalBuyToPay + Others;
                return totalBuyToPay;
            }
        }
    }
}
