﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    class DBHelper
    {
        public static string CnnVal(string name)
        {
            try
            {
                if (Environment.MachineName.ToString().Contains("Name of Target Tablet"))
                {
                    return "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=somePathOnTargetMachine\\TrainingsRapport.mdf;Integrated Security" + "=True";
                }
                else
                {
                    //return ConfigurationManager.ConnectionStrings[name].ConnectionString;
                    return "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=somePathOnDevelopMachine\\TrainingsRapport.mdf;Integrated Security" + "=True";
                    //TO DO...
                }
            }
            catch
            {
                return "error";
            }
          
        }
    }
}
