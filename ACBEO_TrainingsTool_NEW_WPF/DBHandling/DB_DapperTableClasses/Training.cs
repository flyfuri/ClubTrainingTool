﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainingsraport_Tool
{
    public class Training
    {
        public int TrainingID { get; set; }
        public DateTime TrainingDate { get; set; }
        public decimal CashAtBegin { get; set; }
        public decimal CashToACBEO{ get; set; }
        public decimal CashAtEnd { get; set; }
        public string Remarks { get; set; }
        public int Leiter1_ID { get; set; }
        public int Leiter2_ID { get; set; }
    }
}
