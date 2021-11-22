using System;
using System.Data;
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

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for PageTrainingg.xaml
    /// </summary>
    public partial class PageTrainingg : Page
    {
        private int actTrningID;
        private Training actTraining;
        //private List<TrainingCost> displayTrainingCosts = new List<TrainingCost>();
        //private List<Training> displaySummary = new List<Training>();
        DataTable displayTrainingCosts = new DataTable();
        DataTable displaySummary = new DataTable();
        private List<TrainingCost> trainingCosts = new List<TrainingCost>();
        private List<DayPilotCost> dayPilotCosts = new List<DayPilotCost>();
        private decimal totalCostTraining;

        public PageTrainingg(Training actualTraining)
        {
            InitializeComponent();
            actTraining = actualTraining;
            if (actTraining != null)
            {
                actTraining = actualTraining;
                actTrningID = actualTraining.TrainingID;
                updateDisplay();
            }
            else
            {
                actTraining = null;
                actTrningID = -1;
                emptyDisplays();
            }
        }

        private void updateDisplay()
        {
            /**if (textBoxActualTrainingDate.Text.Equals(DateTime.Today.ToShortDateString()))
            {
                this.BackColor = SystemColors.Control;
                textBoxActualTrainingDate.BackColor = SystemColors.Window;
                textBoxActualTrainingDate.ForeColor = SystemColors.WindowText;
            }
            else
            {
                this.BackColor = Color.Orange;
                textBoxActualTrainingDate.BackColor = SystemColors.Window;
                textBoxActualTrainingDate.ForeColor = Color.Red;
            }**/

            DataAccess dbUpdate = new DataAccess();

            //update training costs
            trainingCosts = dbUpdate.getTrainingCostByDate(actTraining.TrainingDate);
            displayTrainingCosts.Clear();
            while (displayTrainingCosts.Columns.Count < 4)
            {
                displayTrainingCosts.Columns.Add();
            }

            displayTrainingCosts.Columns[0].ColumnName = "Betrag";
            displayTrainingCosts.Columns[1].ColumnName = "Kommentar";
            displayTrainingCosts.Columns[2].ColumnName = "Beleg-Nr";
            displayTrainingCosts.Columns[3].ColumnName = "Beleg-Bild";

            List<string> listTempRow = new List<string>();
            listTempRow.Clear();

            totalCostTraining = 0;
            foreach (TrainingCost trainingCost in trainingCosts)
            {
                displayTrainingCosts.Rows.Add();
                displayTrainingCosts.Rows[displayTrainingCosts.Rows.Count - 1].ItemArray = trainingCost.valueList.ToArray();
                totalCostTraining = totalCostTraining + trainingCost.Betrag;
            }
            dataGridViewDispTrnCosts.ItemsSource = displayTrainingCosts.DefaultView;  //DefaultView new due to WPF

            //set how table looks like
            ///dataGridViewDispTrnCosts.ReadOnly = true;
            //dataGridViewDisplay.Columns[0].Width = 150;
            ///dataGridViewDispTrnCosts.Columns[0].Frozen = true;

            ///DataGridViewCellStyle columnCellStyle = new DataGridViewCellStyle();
            ///columnCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewDisplay.Columns[dataGridViewDisplay.Columns.Count - 1].Frozen = true;
            ///dataGridViewDispTrnCosts.Columns[dataGridViewDispTrnCosts.Columns.Count - 1].DefaultCellStyle = columnCellStyle;

            /**for (int ii = 1; ii < dataGridViewDispTrnCosts.Columns.Count; ii++)
            {
                dataGridViewDispTrnCosts.Columns[ii].Width = 55;
                dataGridViewDispTrnCosts.Columns[ii].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            for (int ii = 0; ii < dataGridViewDispTrnCosts.Rows.Count; ii++)
            {
                dataGridViewDispTrnCosts.Rows[ii].Height = 45;
            }**/

            //update total payed by pilots
            dayPilotCosts = dbUpdate.getDayPilotCostsByTrainID(actTrningID);
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
            displaySummary.Clear();
            while (displaySummary.Columns.Count < 4)
            {
                displaySummary.Columns.Add();
            }
            displaySummary.Columns[0].ColumnName = "What";
            displaySummary.Columns[1].ColumnName = "Income";
            displaySummary.Columns[2].ColumnName = "spent";
            displaySummary.Columns[3].ColumnName = "totals";

            tempRowSummary.Clear();
            tempRowSummary.Add("Total Cost Training");
            tempRowSummary.Add("");
            tempRowSummary.Add(totalCostTraining.ToString());
            tempRowSummary.Add("");
            displaySummary.Rows.Add(tempRowSummary.ToArray());
            displaySummary.Rows.Add();

            tempRowSummary.Clear();
            tempRowSummary.Add("Total Payed by Pilots");
            tempRowSummary.Add(totalPayedByPilots.ToString());
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            displaySummary.Rows.Add(tempRowSummary.ToArray());

            tempRowSummary.Clear();
            tempRowSummary.Add("Win/loss of this Training");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            decimal totThisTraining = totalPayedByPilots - totalCostTraining;
            tempRowSummary.Add(totThisTraining.ToString());
            displaySummary.Rows.Add(tempRowSummary.ToArray());
            displaySummary.Rows.Add();

            tempRowSummary.Clear();
            tempRowSummary.Add("Money Before Training");
            tempRowSummary.Add(actTraining.CashAtBegin.ToString());
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            displaySummary.Rows.Add(tempRowSummary.ToArray());
            displaySummary.Rows.Add();

            tempRowSummary.Clear();
            tempRowSummary.Add("Money Payed to ACBEO");
            tempRowSummary.Add("");
            tempRowSummary.Add(actTraining.CashToACBEO.ToString());
            tempRowSummary.Add("");
            displaySummary.Rows.Add(tempRowSummary.ToArray());
            displaySummary.Rows.Add();

            tempRowSummary.Clear();
            tempRowSummary.Add("Money left");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            tempRowSummary.Add((totThisTraining + actTraining.CashAtBegin - actTraining.CashToACBEO).ToString());
            displaySummary.Rows.Add(tempRowSummary.ToArray());
            dataGridViewSummary.ItemsSource = displaySummary.DefaultView;  //DefaultView new due to WPF

            //set how table looks like
            ///dataGridViewSummary.ReadOnly = true;
            //dataGridViewSummary.Columns[0].Width = 200;
            ///dataGridViewSummary.Columns[0].Frozen = true;


            /**DataGridViewCellStyle columnCellStyleSummary = new DataGridViewCellStyle();
            columnCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewSummary.Columns[dataGridViewSummary.Columns.Count - 1].DefaultCellStyle = columnCellStyle;

            for (int ii = 1; ii < dataGridViewSummary.Columns.Count; ii++)
            {
                dataGridViewSummary.Columns[ii].Width = 55;
                dataGridViewSummary.Columns[ii].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }**/

            /**for (int ii = 0; ii < dataGridViewSummary.Columns[0].Rows.Count; ii++)
            {
                dataGridViewSummary.Rows[ii].Height = 35;
            }
            dataGridViewSummary.Rows[1].Height = 5;
            dataGridViewSummary.Rows[4].Height = 5;
            dataGridViewSummary.Rows[6].Height = 5;
            dataGridViewSummary.Rows[8].Height = 5;
            dataGridViewSummary.Rows[5].Cells[1].Style.BackColor = Color.White;
            dataGridViewSummary.Rows[7].Cells[2].Style.BackColor = Color.White;**/
        }

        private void emptyDisplays()
        {
            displayTrainingCosts.Clear();
            dataGridViewDispTrnCosts.ItemsSource = displayTrainingCosts.DefaultView;  //DefaultView new due to WPF
            displaySummary.Clear();
            dataGridViewSummary.ItemsSource = displaySummary.DefaultView;  //DefaultView new due to WPF
        }

        private void dataGridViewDispTrnCosts_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void dataGridViewSummary_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
