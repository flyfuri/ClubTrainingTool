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
using MyWPFExtentions;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for PageDayPilotCosts.xaml
    /// </summary>
    public partial class PageDayPilotCosts : Page
    {
        private Training actTraining;

        private List<Participant> participants = new List<Participant>();
        private List<DayPilotCost> tempRowOfTotCosts = new List<DayPilotCost>();
        private DataTable display = new DataTable();
        private List<Training> actTrainings = new List<Training>();
        public PageDayPilotCosts(Training actualTraining)
        {
            InitializeComponent();
            actTraining = actualTraining;
        }

        private void TotalCostUpdateDisplay()
        {
            display.Clear();
            DataAccess dbUpdate = new DataAccess();
            participants = dbUpdate.getParticipants(actTraining.TrainingID);
            //actTrainings = dbUpdate.getTrainingByID(actTraining.TrainingID);
            //actTraining = actTrainings[0];

            //add Pilot coumn
            if (display.Columns.Count == 0)
            {
                display.Columns.Add();
                display.Columns[0].ColumnName = "Pilot";
                display.Columns.Add();
                display.Columns[1].ColumnName = "Kosten Flüge";
                display.Columns.Add();
                display.Columns[2].ColumnName = "Weitere Dienste";
                display.Columns.Add();
                display.Columns[3].ColumnName = "ZwischenTotal Flüge Dienste";
                display.Columns.Add();
                display.Columns[4].ColumnName = "Kosten Einkäufe";
                display.Columns.Add();
                display.Columns[5].ColumnName = "Mit Abo bezahlbare Flüge (Anzahl)";
                display.Columns.Add();
                display.Columns[6].ColumnName = "Flüge mit Abo bezahlt (Anzahl)";
                display.Columns.Add();
                display.Columns[7].ColumnName = "Total noch zu bezahlen";
                display.Columns.Add();
                display.Columns[8].ColumnName = "Bezahlt (Cash - genauer Betrag)"; 
                display.Columns.Add();
                display.Columns[9].ColumnName = "Bezahlt (TWINT - genauer Betrag)";
                display.Columns.Add();
                display.Columns[10].ColumnName = "Date and Time from TWINT App";
                display.Columns.Add();
                display.Columns[11].ColumnName = "OK alles Bezahlt";

            }

            List<string> listTempRow = new List<string>();

            foreach (Participant participant in participants)
            {
                listTempRow.Clear();
                tempRowOfTotCosts = dbUpdate.getDayPilotCostsByTrainIDParticipID(actTraining.TrainingID, participant.ParticipantID);
                if (tempRowOfTotCosts.Count > 0)
                {
                    //numbers of flghts payed with Abo
                    int nbFlightsPayedWithAbo = dbUpdate.getAboFlightPayedWithDayPilot(participant.PilotID, actTraining.TrainingID).Count;
                    if (nbFlightsPayedWithAbo == 0)
                    {
                        nbFlightsPayedWithAbo = dbUpdate.getAboFlightPayedBackWithDayPilot(participant.PilotID, actTraining.TrainingID).Count;
                        nbFlightsPayedWithAbo = nbFlightsPayedWithAbo * (-1);
                    }
                    tempRowOfTotCosts[0].payedWithAbo = nbFlightsPayedWithAbo;

                    //all from database
                    listTempRow = tempRowOfTotCosts[0].valueList;


                    //listTempRow[6] = tempRowOfTotCosts[0].TotalToPay.ToString();
                    listTempRow.Insert(0, participant.ComplNameAndLicence);
                }
                else
                {
                    listTempRow.Clear();
                    listTempRow.Add(participant.ComplNameAndLicence);
                    for (int i = 1; i < 11; i++)
                    {
                        listTempRow.Add("");
                    }
                }
                display.Rows.Add();
                display.Rows[display.Rows.Count - 1].ItemArray = listTempRow.ToArray();
            }
            dataGridViewDisplay.ItemsSource = display.DefaultView;

            //set how table looks like
            /**dataGridViewDisplay.ReadOnly = true;
            dataGridViewDisplay.Columns[0].Width = 250;
            dataGridViewDisplay.Columns[0].Frozen = true;

            DataGridViewCellStyle columnCellStyle = new DataGridViewCellStyle();
            columnCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewDisplay.Columns[dataGridViewDisplay.Columns.Count - 1].Frozen = true;
            dataGridViewDisplay.Columns[dataGridViewDisplay.Columns.Count - 1].DefaultCellStyle = columnCellStyle;

            for (int ii = 1; ii < dataGridViewDisplay.Columns.Count; ii++)
            {
                dataGridViewDisplay.Columns[ii].Width = 55;
                dataGridViewDisplay.Columns[ii].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            for (int ii = 0; ii < dataGridViewDisplay.Rows.Count; ii++)
            {
                dataGridViewDisplay.Rows[ii].Height = 45;
            }
            for (int ii = 0; ii < dataGridViewDisplay.Rows.Count; ii++)
            {
                for (int iii = 1; iii < dataGridViewDisplay.Rows[ii].Cells.Count - 1; iii++)
                {
                    if (iii == 6 || iii == 8 || iii == 9)
                    {
                        dataGridViewDisplay.Rows[ii].Cells[iii].Style.BackColor = Color.White;
                    }
                }
            }**/
        }

        private void Page_DayPilotCosts_Loaded(object sender, RoutedEventArgs e)
        {
            TotalCostUpdateDisplay();
        }

        private void dataGridViewDisplay_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGridCellClickRowColumnFormStyle e2 = new DataGridCellClickRowColumnFormStyle();  //provide RowIndex and ColumnIndex from e in forms style
            e2.wpf_e = e;

            if (e2.RowIndex >= 0
               & e2.RowIndex <= dataGridViewDisplay.Items.Count - 1
               & (e2.ColumnIndex == 0
               || e2.ColumnIndex == 6
               || e2.ColumnIndex == 8
               || e2.ColumnIndex == 9
               || e2.ColumnIndex == 10))
            {
                int actCellPilotID = participants[e2.RowIndex].PilotID;
                Participant actCellParticipant = participants[e2.RowIndex];
                DataAccess db = new DataAccess();
                bool boolFormWasCancled = false;
                decimal decimalFormKeyDecResult = 0;
                string stringFormABCResult = "";

                //show pilotinfos
                if (e2.ColumnIndex == 0)
                {
                    List<AboFlight> validAboFlights = new List<AboFlight>();
                    string codeToShow = $"{actCellParticipant.ComplNameAndLicence}__{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}";
                    validAboFlights = db.getUsableAboFlightsByPilotID(actCellParticipant.PilotID, MainWindow.actYEAR);
                    foreach (AboFlight validAboFlight in validAboFlights)
                    {
                        if (!codeToShow.Contains(validAboFlight.AboYearNr))
                        {
                            codeToShow = $"{codeToShow}__{validAboFlight.AboYearNr}";
                        }
                    }
                    codeToShow = $"{codeToShow}__{validAboFlights.Count.ToString()}";
                    codeToShow = $"{codeToShow}{Environment.NewLine}{Environment.NewLine}Mehr Details anzeigen?";
                    MessageBoxResult msgBoxResult = MessageBox.Show(codeToShow,
                                                                "",
                                                                MessageBoxButton.YesNo,
                                                                MessageBoxImage.Question);
                    if (msgBoxResult == MessageBoxResult.Yes)
                    {
                        WindowAboPilotOverview formAboOverview = new WindowAboPilotOverview(participants[e2.RowIndex].PilotID, participants[e2.RowIndex].ComplNameAndLicence, MainWindow.actYEAR);
                        formAboOverview.ShowDialog();
                    }
                }

                //number of Abos to Pay
                if (e2.ColumnIndex == 6)
                {
                    tempRowOfTotCosts = db.getDayPilotCostsByTrainIDParticipID(actTraining.TrainingID, actCellParticipant.ParticipantID);
                    int flightsPayedYet = db.getAboFlightPayedWithDayPilot(actCellParticipant.PilotID, actTraining.TrainingID).Count;
                    int flightsNotPayedYet = tempRowOfTotCosts[0].payableWithAbo - flightsPayedYet;
                    WindowPayWithAbo formPayWithAbo = new WindowPayWithAbo(actTraining, actCellParticipant, flightsNotPayedYet);
                    formPayWithAbo.ShowDialog();
                    TotalCostUpdateDisplay();
                }
                else if (e2.ColumnIndex == 8 || e2.ColumnIndex == 9) //numeric ************************
                {
                    decimal defaultValueDecimal = 0;
                    bool flagUseDefaultValue = false;
                    string windowTitle = "";

                    tempRowOfTotCosts = db.getDayPilotCostsByTrainIDParticipID(actTraining.TrainingID, actCellParticipant.ParticipantID);
                    if (tempRowOfTotCosts.Count > 0)
                    {
                        if(e2.ColumnIndex == 8)
                        {
                            defaultValueDecimal = tempRowOfTotCosts[0].PayedAmount;
                            windowTitle = "Enter amount payed cash";
                        }
                        else if (e2.ColumnIndex == 9)
                        {
                            defaultValueDecimal = tempRowOfTotCosts[0].PayedTwint;
                            windowTitle = "Enter amount payed with TWINT";
                        }
                        flagUseDefaultValue = true;
                    }
                    else
                    {
                        defaultValueDecimal = 0;
                        flagUseDefaultValue = false;
                    }
                    WindowDialogKeyNumDecimal formKeyNumDecimal = new WindowDialogKeyNumDecimal(flagUseDefaultValue, defaultValueDecimal);
                    formKeyNumDecimal.Title = windowTitle;
                    formKeyNumDecimal.ShowDialog();
                    boolFormWasCancled = formKeyNumDecimal.wasCanceled;
                    decimalFormKeyDecResult = formKeyNumDecimal.return_decimal;
                }
                //TWINT payment with SwissQR code
                else if (e2.ColumnIndex == 10)
                {
                    
                    bool twintModifyCanceled = false;
                    string twintRef = dataGridViewDisplay.getValueTextStringByRowColIndexes(e2.RowIndex, 10);

                    //in case theres already a value, ask if want to modifie...
                    if (twintRef != "")
                    {
                        MessageBoxResult msgBoxResult = MessageBox.Show("Do you really want to modify date and time of Twint Payment reference?",
                                                                "",
                                                                MessageBoxButton.YesNo,
                                                                MessageBoxImage.Question);
                        twintModifyCanceled = !(msgBoxResult.Equals(MessageBoxResult.Yes));
                    }

                    if (!twintModifyCanceled)
                    {
                        decimal paymentamount = 0;

                        try
                        {
                            paymentamount = decimal.Parse(dataGridViewDisplay.getValueTextStringByRowColIndexes(e2.RowIndex, 9));
                        }
                        catch (Exception exept)
                        {
                            MessageBox.Show("Error parsing TotalCost : '{exept}'");
                            return;
                        }

                        //open window to set payment amount if 0..
                        if (paymentamount <= 0)  //open window for amountif 0
                        {
                            decimal defaultValueDecimal;
                            bool flagUseDefaultValue;

                            tempRowOfTotCosts = db.getDayPilotCostsByTrainIDParticipID(actTraining.TrainingID, actCellParticipant.ParticipantID);
                            if (tempRowOfTotCosts.Count > 0)
                            {
                                defaultValueDecimal = tempRowOfTotCosts[0].PayedTwint;
                                flagUseDefaultValue = true;
                            }
                            else
                            {
                                defaultValueDecimal = 0;
                                flagUseDefaultValue = false;
                            }
                            WindowDialogKeyNumDecimal formKeyNumDecimal = new WindowDialogKeyNumDecimal(flagUseDefaultValue, (int)defaultValueDecimal);
                            formKeyNumDecimal.Title = "Enter amount payed with TWINT";
                            formKeyNumDecimal.ShowDialog();
                            boolFormWasCancled = formKeyNumDecimal.wasCanceled;
                            decimalFormKeyDecResult = formKeyNumDecimal.return_decimal;
                            paymentamount = decimalFormKeyDecResult;
                        }
                        else
                        {
                            decimalFormKeyDecResult = paymentamount;
                        }

                        // open window with TWINT QR..
                        if (paymentamount > 0 & !boolFormWasCancled)
                        {
                            string paymentmessage = $"{actTraining.TrainingDate.ToShortDateString()}{";"}{ actCellParticipant.ComplNameAndLicence}{";Bezahlung Training Pilot;"}{paymentamount.ToString()} ";
                            WindowCreateShowSwissQR formShowSwissQRBill = new WindowCreateShowSwissQR(paymentamount, paymentmessage, true);
                            formShowSwissQRBill.Title = "Scan QR code to pay with TWINT and press CLOSE button";
                            formShowSwissQRBill.ShowDialog();

                            //open alphanumeric to set timestamp from TWINT...
                            string defaultValueString;
                            //bool flagUseDefaultValue = false;

                            // open window to set TWINT payment reference (currently date, time)...
                            if (twintRef == "")
                            {
                                int actMinute = DateTime.Now.Minute;
                                if (actMinute < 10)
                                {
                                    defaultValueString = $"{DateTime.Now.Year}{"."}{DateTime.Now.Month}{"."}{DateTime.Now.Day}{" "}{DateTime.Now.Hour}{":0"}{actMinute}";
                                }
                                else
                                {
                                    defaultValueString = $"{DateTime.Now.Year}{"."}{DateTime.Now.Month}{"."}{DateTime.Now.Day}{" "}{DateTime.Now.Hour}{":"}{actMinute}";
                                }
                            }
                            else
                            {
                                defaultValueString = twintRef;
                            }

                            WindowKeyABC123 formAlphanum = new WindowKeyABC123(true, defaultValueString);
                            formAlphanum.Title = "Please enter date and time of payment shown in TWINT app as reference";
                            formAlphanum.ShowDialog();
                            boolFormWasCancled = formAlphanum.wasCanceled;
                            stringFormABCResult = formAlphanum.return_string;
                        }
                        else if (!boolFormWasCancled)
                        {
                            MessageBox.Show("Error: Amount is less than or 0!");
                        }
                    }
                }
                else if (e2.ColumnIndex == 10) //alpanumeric ************************
                {
                    string defaultValueString;
                    //bool flagUseDefaultValue = false;
                    string windowTitle = "";

                    tempRowOfTotCosts = db.getDayPilotCostsByTrainIDParticipID(actTraining.TrainingID, actCellParticipant.ParticipantID);
                    defaultValueString = "OK";

                    WindowKeyABC123 formAlphanum = new WindowKeyABC123(true, defaultValueString);
                    formAlphanum.ShowDialog();
                    boolFormWasCancled = formAlphanum.wasCanceled;
                    stringFormABCResult = formAlphanum.return_string;
                }

                //add or update PilotCostTable
                if (!boolFormWasCancled & (e2.ColumnIndex == 6 || e2.ColumnIndex == 8 || e2.ColumnIndex == 9 || e2.ColumnIndex == 10 || e2.ColumnIndex == 11))
                {
                    //Get record by cell position via partitipant and trainingNr
                    tempRowOfTotCosts = db.getDayPilotCostsByTrainIDParticipID(actTraining.TrainingID, actCellParticipant.ParticipantID);
                    DayPilotCost tempDayPilotCost = new DayPilotCost();
                    if (tempRowOfTotCosts.Count == 0)  //add
                    {
                        tempDayPilotCost.ParticipantID = actCellParticipant.ParticipantID;
                        tempDayPilotCost.TrainingID = actTraining.TrainingID;

                        switch (e2.ColumnIndex)
                        {
                            case 8:
                                tempDayPilotCost.PayedAmount = decimalFormKeyDecResult;
                                break;

                            case 9:
                                tempDayPilotCost.PayedTwint = decimalFormKeyDecResult;
                                break;

                            case 10:
                                tempDayPilotCost.PayedTwint = decimalFormKeyDecResult;
                                tempDayPilotCost.PayedTwintReference = stringFormABCResult;
                                break;

                            case 11:
                                if (stringFormABCResult == "OK")
                                {
                                    tempDayPilotCost.PayedFlag = true;
                                }
                                else
                                {
                                    tempDayPilotCost.PayedFlag = false;
                                }
                                break;
                        }
                        tempRowOfTotCosts.Clear();
                        tempRowOfTotCosts.Add(tempDayPilotCost);

                        db.addDayPilotCost(tempRowOfTotCosts);
                        TotalCostUpdateDisplay();
                    }
                    if (tempRowOfTotCosts.Count == 1) //update
                    {
                        switch (e2.ColumnIndex)
                        {
                            case 8:
                                tempRowOfTotCosts[0].PayedAmount = decimalFormKeyDecResult;
                                break;

                            case 9:
                                tempRowOfTotCosts[0].PayedTwint = decimalFormKeyDecResult;
                                break;

                            case 10:
                                tempRowOfTotCosts[0].PayedTwint = decimalFormKeyDecResult;
                                tempRowOfTotCosts[0].PayedTwintReference = stringFormABCResult;
                                break;

                            case 11:
                                if (stringFormABCResult == "OK")
                                {
                                    tempRowOfTotCosts[0].PayedFlag = true;
                                }
                                else
                                {
                                    tempRowOfTotCosts[0].PayedFlag = false;
                                }
                                break;
                        }
                        db.updateDayPilotCosts(tempRowOfTotCosts);
                        TotalCostUpdateDisplay();
                    }
                }
            }
        }

        //wraping the data grid header
        private void dataGridViewDisplay_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (dataGridViewDisplay.Columns.Count == 1)
            {
                dataGridViewDisplay.Columns[0].MinWidth = 200;
            }
            TextBlock tbHeader = new TextBlock();
            tbHeader.TextWrapping = TextWrapping.Wrap;  //for wrapping the Header  
            tbHeader.Text = e.Column.Header.ToString();
            tbHeader.SizeChanged += TbHeader_SizeChanged;

            e.Column.Header = tbHeader;
        }

        //change dategrid header height when wrapping
        private void TbHeader_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height > dataGridViewDisplay.ColumnHeaderHeight)
            {
                dataGridViewDisplay.ColumnHeaderHeight = e.NewSize.Height + 5;
            }
        }
    }
}
