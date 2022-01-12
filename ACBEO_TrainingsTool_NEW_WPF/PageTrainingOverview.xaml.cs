using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for PageTrainingOverview.xaml
    /// </summary>
    public partial class PageTrainingOverview : Page
    {
        List<Training> trainigsList = new List<Training>();
        List<TrainingCost> trainingCosts = new List<TrainingCost>();
        List<DayPilotCost> dayPilotCosts = new List<DayPilotCost>();
        decimal totalCostTraining;
        DataAccess db = new DataAccess();
        DataTable display = new DataTable();
        private bool skipevent;

        public PageTrainingOverview()
        {
            InitializeComponent();
        }

        private void TrngOverviewUpdateDisplay()
        {
            if (display.Columns.Count == 0)
            {
                display.Columns.Add();
                display.Columns[0].ColumnName = "Datum";  //Attention column header is not allowed to contain () /\ etc. !!
                display.Columns.Add();
                display.Columns[1].ColumnName = "Training Ausgaben";
                display.Columns.Add();
                display.Columns[2].ColumnName = "Training Einnahmen";
                display.Columns.Add();
                display.Columns[3].ColumnName = "Gewinn Verlust";
                display.Columns.Add();
                display.Columns[4].ColumnName = "Bezahlt an ACBEO";
                display.Columns.Add();
                display.Columns[5].ColumnName = "Kasse vor Training";
                display.Columns.Add();
                display.Columns[6].ColumnName = "Kasse nach Training";
                display.Columns.Add();
                display.Columns[7].ColumnName = "Differenz Fehler";
                display.Columns.Add();
                display.Columns[8].ColumnName = "Fehler summiert";
            }

            trainigsList = db.getTrainingsOrderedByDate(0);

            if (trainigsList.Count > 0)
            {
                int intFromValue = 2017;
                int intToValue = 2017;
                bool boolFromParsed, boolToParsed;
                decimal moneyAfterTrainingPreviousLine;
                decimal errorSumm;
                List<string> listTempRow = new List<string>();

                display.Clear();

                try
                {
                    boolFromParsed = Int32.TryParse(comboBoxFrom.Items[comboBoxFrom.SelectedIndex].ToString(), out intFromValue);
                    boolToParsed = Int32.TryParse(comboBoxTo.Items[comboBoxTo.SelectedIndex].ToString(), out intToValue);
                }
                catch { }

                moneyAfterTrainingPreviousLine = 0;
                errorSumm = 0;
                foreach (Training training in trainigsList)
                {
                    if (training.TrainingDate.Year >= intFromValue && training.TrainingDate.Year <= intToValue)
                    {
                        //update training costs
                        trainingCosts = db.getTrainingCostByDate(training.TrainingDate);

                        listTempRow.Clear();

                        totalCostTraining = 0;
                        foreach (TrainingCost trainingCost in trainingCosts)
                        {
                            totalCostTraining = totalCostTraining + trainingCost.Betrag;
                        }

                        //update total payed by pilots
                        dayPilotCosts = db.getDayPilotCostsByTrainID(training.TrainingID);
                        decimal totalPayedByPilots = 0;
                        bool flagAllPilotsHavePayed = true;
                        List<string> tempRowSummary = new List<string>();
                        foreach (DayPilotCost dayPilotCost in dayPilotCosts)
                        {
                            totalPayedByPilots = totalPayedByPilots + dayPilotCost.PayedAmount;
                            if (!dayPilotCost.PayedFlag)
                            {
                                flagAllPilotsHavePayed = false;
                            }
                        }

                        //totals
                        decimal totThisTraining = totalPayedByPilots - totalCostTraining;

                        display.Rows.Add();
                        display.Rows[display.Rows.Count - 1][0] = training.TrainingDate.ToShortDateString();
                        display.Rows[display.Rows.Count - 1][1] = totalCostTraining.ToString();
                        display.Rows[display.Rows.Count - 1][2] = totalPayedByPilots.ToString();
                        display.Rows[display.Rows.Count - 1][3] = totThisTraining.ToString();
                        display.Rows[display.Rows.Count - 1][4] = training.CashToACBEO;
                        display.Rows[display.Rows.Count - 1][5] = training.CashAtBegin;
                        if (display.Rows.Count > 1)
                        {
                            display.Rows[display.Rows.Count - 1][7] = (training.CashAtBegin - moneyAfterTrainingPreviousLine).ToString();
                            errorSumm = errorSumm + (training.CashAtBegin - moneyAfterTrainingPreviousLine);
                            display.Rows[display.Rows.Count - 1][8] = errorSumm.ToString();
                        }
                        else
                        { 
                            display.Rows[display.Rows.Count - 1][7] = 0;
                            display.Rows[display.Rows.Count - 1][8] = 0;
                        }
                        moneyAfterTrainingPreviousLine = (totThisTraining + training.CashAtBegin - training.CashToACBEO);
                        display.Rows[display.Rows.Count - 1][6] = (totThisTraining + training.CashAtBegin - training.CashToACBEO).ToString();
                    }
                }
            }
            dataGridViewDisplay.ItemsSource = display.DefaultView;

            labelCntTraining.Content = $"{display.Rows.Count.ToString()} Trainings";

            //set how table looks like
            /**dataGridViewDisplay.ReadOnly = true;
            dataGridViewDisplay.Columns[0].Width = 250;
            dataGridViewDisplay.Columns[0].Frozen = true;

            DataGridViewCellStyle columnCellStyle = new DataGridViewCellStyle();
            columnCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewDisplay.Columns[dataGridViewDisplay.Columns.Count - 1].Frozen = true;
            dataGridViewDisplay.Columns[dataGridViewDisplay.Columns.Count - 1].DefaultCellStyle = columnCellStyle;**/
        }

        private void Page_TrainingOverview_Loaded(object sender, RoutedEventArgs e)
        {
            skipevent = true;
            trainigsList = db.getTrainingsOrderedByDate(0);

            if (trainigsList.Count > 0)
            {
                comboBoxFrom.Items.Clear();
                comboBoxTo.Items.Clear();
                string tempDate;
                foreach (Training training in trainigsList)
                {
                    tempDate = training.TrainingDate.Year.ToString();
                    if (comboBoxFrom.Items.Contains(tempDate) != true)
                    {
                        comboBoxFrom.Items.Add(tempDate);
                        comboBoxTo.Items.Add(tempDate);
                    }
                }
                comboBoxFrom.SelectedIndex = comboBoxFrom.Items.Count - 1;
                comboBoxTo.SelectedIndex = comboBoxTo.Items.Count - 1;
            }
            skipevent = false;
            TrngOverviewUpdateDisplay();
        }

        private void comboBoxFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!skipevent)
            {
                int intFromValue, intToValue;
                bool boolFromParsed, boolToParsed;
                if (comboBoxFrom.Items.Count > 0)
                {
                    //if (Int32.Parse(comboBoxTo.SelectedItem.ToString()) < Int32.Parse(comboBoxFrom.SelectedItem.ToString()) )
                    //if (Convert.ToInt32(comboBoxTo.SelectedItem.ToString()) < Convert.ToInt32(comboBoxFrom.SelectedItem.ToString()))        
                    try
                    {
                        boolFromParsed = Int32.TryParse(comboBoxFrom.Items[comboBoxFrom.SelectedIndex].ToString(), out intFromValue);
                        boolToParsed = Int32.TryParse(comboBoxTo.Items[comboBoxTo.SelectedIndex].ToString(), out intToValue);
                        if (intFromValue > intToValue && boolToParsed == true && boolFromParsed == true)
                        {
                            comboBoxTo.SelectedIndex = comboBoxFrom.SelectedIndex;
                        }
                    }
                    catch { }
                }
                TrngOverviewUpdateDisplay();
            }
        }

        private void comboBoxTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!skipevent)
            {
                int intFromValue, intToValue;
                bool boolFromParsed, boolToParsed;
                if (comboBoxFrom.Items.Count > 0)
                {
                    //if (Int32.Parse(comboBoxTo.SelectedItem.ToString()) < Int32.Parse(comboBoxFrom.SelectedItem.ToString()))
                    //if (Convert.ToInt32(comboBoxTo.SelectedItem.ToString()) < Convert.ToInt32(comboBoxFrom.SelectedItem.ToString()))
                    try
                    {
                        boolFromParsed = Int32.TryParse(comboBoxFrom.Items[comboBoxFrom.SelectedIndex].ToString(), out intFromValue);
                        boolToParsed = Int32.TryParse(comboBoxTo.Items[comboBoxTo.SelectedIndex].ToString(), out intToValue);
                        if (intFromValue > intToValue && boolToParsed == true && boolFromParsed == true)
                        {
                            comboBoxFrom.SelectedIndex = comboBoxTo.SelectedIndex;
                        }
                    }
                    catch { }
                }
                TrngOverviewUpdateDisplay();
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
