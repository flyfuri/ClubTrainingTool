using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    class DayPilotCost
    {
        public int DayPilotCostID { get; set; }
        public int TrainingID { get; set; }
        public int ParticipantID { get; set; }
        public decimal CostFlights { get; set; }
        public decimal CostOtherServices { get; set; }
        public decimal BalanceFlighAndServices
        {
            get
            {
                decimal tempTotal = CostFlights - CostOtherServices;
                if (tempTotal < 0)
                {
                    return ((tempTotal / 9) * 7);
                }
                return tempTotal;
            }
        }
        public decimal CostBuy { get; set; }
        public int payableWithAbo
        {
            get
            {
                return (int)((CostFlights - CostOtherServices) / 9);
                /*if (BalanceFlighAndServices > 0)
                {
                    return (int)((CostFlights - CostOtherServices) / 9);
                }
                else
                {
                    return 0;
                }*/
            }
        }
        public int payedWithAbo { get; set; }
        public decimal TotalToPay
        {
            get
            {
                if (payableWithAbo >= 0)
                {
                    return (BalanceFlighAndServices + payedWithAbo * (-9) + CostBuy);
                }
                else
                {
                    return (BalanceFlighAndServices + payedWithAbo * (-7) + CostBuy);
                }
            }
        }
        public decimal PayedAmount { get; set; } //cash
        public bool PayedFlag { get; set; }
        public decimal PayedTwint { get; set; }
        public string PayedTwintReference { get; set; }

        public List<string> valueList //order for view
        {
            get
            {
                List<string> tempList = new List<string>();
                tempList.Clear();
                tempList.Add(CostFlights.ToString());
                tempList.Add(CostOtherServices.ToString());
                tempList.Add(BalanceFlighAndServices.ToString());
                tempList.Add(CostBuy.ToString());
                tempList.Add(payableWithAbo.ToString());
                tempList.Add(payedWithAbo.ToString());
                tempList.Add(TotalToPay.ToString());
                tempList.Add(PayedAmount.ToString());
                tempList.Add(PayedTwint.ToString());
                tempList.Add(PayedTwintReference);
                if (PayedFlag)
                {
                    tempList.Add("OK");
                }
                else
                {
                    tempList.Add("");
                }
                return tempList;
            }
        }

        public List<string> valueListinDBOrder
        {
            get
            {
                List<string> tempList = new List<string>();
                tempList.Clear();
                tempList.Add(CostFlights.ToString());
                tempList.Add(CostOtherServices.ToString());
                tempList.Add(BalanceFlighAndServices.ToString());
                tempList.Add(CostBuy.ToString());
                tempList.Add(payableWithAbo.ToString());
                tempList.Add(payedWithAbo.ToString());
                tempList.Add(TotalToPay.ToString());
                tempList.Add(PayedAmount.ToString());
                if (PayedFlag)
                {
                    tempList.Add("OK");
                }
                else
                {
                    tempList.Add("");
                }
                tempList.Add(PayedTwint.ToString());
                tempList.Add(PayedTwintReference);
                return tempList;
            }
        }

        public bool testIfPayed()
        {
            if ((TotalToPay - PayedAmount) > 3 & (TotalToPay - PayedTwint) > 0)
            {
                return false;
            }
            else if(!PayedFlag)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
