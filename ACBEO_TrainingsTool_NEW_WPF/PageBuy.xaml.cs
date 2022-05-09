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
    /// Interaction logic for PageBuy.xaml
    /// </summary>
    public partial class PageBuy : Page
    {
        private Training actTraining;

        List<Participant> participants = new List<Participant>();
        List<DayPilotBuy> tempRowOfBuys = new List<DayPilotBuy>();
        DataTable display = new DataTable();

        public PageBuy(Training actualTraining)
        {
            InitializeComponent();
            actTraining = actualTraining;
            TrainingFinalizeHelper calcColor = new TrainingFinalizeHelper(actTraining);
            mainGrid.Background = calcColor.calcBGBrush();
        }

        private void Page_Buy_Loaded(object sender, RoutedEventArgs e)
        {
            BuyUpdateDisplay();
        }

        private void BuyUpdateDisplay()
        {
            display.Clear();
            DataAccess dbUpdate = new DataAccess();
            participants = dbUpdate.getParticipants(actTraining.TrainingID);

            //add Pilot coumn
            if (display.Columns.Count == 0)
            {
                display.Columns.Add();
                display.Columns[0].ColumnName = "Pilot";
                display.Columns.Add();
                display.Columns[1].ColumnName = "Abo(s) heute kaufen (Nr)";
                display.Columns.Add();
                display.Columns[2].ColumnName = "Schwimmweste Kauf";
                display.Columns.Add();
                display.Columns[3].ColumnName = "Schwimmweste Miete";
                display.Columns.Add();
                display.Columns[4].ColumnName = "Jahresbeitrag";
                display.Columns.Add();
                display.Columns[5].ColumnName = "Tagesmitlied";
                display.Columns.Add();
                display.Columns[6].ColumnName = "Axalp Week Mitglied";
                display.Columns.Add();
                display.Columns[7].ColumnName = "Anderes";
                display.Columns.Add();
                display.Columns[8].ColumnName = "Bemerkungen";
                display.Columns.Add();
                display.Columns[9].ColumnName = "Kosten Eingekauft";

            }

            List<string> listTempRow = new List<string>();

            foreach (Participant participant in participants)
            {
                listTempRow.Clear();
                tempRowOfBuys = dbUpdate.getDayPilotBuyByTrainIDParticipID(actTraining.TrainingID, participant.ParticipantID);
                if (tempRowOfBuys.Count > 0)
                {
                    listTempRow = tempRowOfBuys[0].valueList;

                    //insert Abos to buy
                    List<AboFlight> abosToBuyInThisTraining = new List<AboFlight>();
                    abosToBuyInThisTraining = dbUpdate.getAboFlightNrsDayPilot(participant.PilotID, actTraining.TrainingID);
                    string stringAbosToBuy = "";
                    if (abosToBuyInThisTraining.Count > 0)
                    {
                        foreach (AboFlight abo in abosToBuyInThisTraining)
                        {
                            if (stringAbosToBuy == "")
                            {
                                stringAbosToBuy = $"{abo.Abo_Year}-{abo.Abo_NrInYear}";
                            }
                            else
                            {
                                stringAbosToBuy = $"{stringAbosToBuy}; {abo.Abo_Year}-{abo.Abo_NrInYear}";
                            }
                        }
                    }
                    listTempRow.Insert(0, stringAbosToBuy);

                    //insert Pilot
                    listTempRow.Insert(0, participant.ComplNameAndLicence);
                    //add Total at end
                    listTempRow.Add(((abosToBuyInThisTraining.Count * 72) + tempRowOfBuys[0].totalCostBuyWithoutAbos).ToString());

                }
                else
                {
                    listTempRow.Clear();
                    listTempRow.Add(participant.ComplNameAndLicence);
                    for (int i = 1; i < 9; i++)
                    {
                        listTempRow.Add("");
                    }
                    listTempRow.Add("0");
                }

                display.Rows.Add();
                display.Rows[display.Rows.Count - 1].ItemArray = listTempRow.ToArray();
            }
            dataGridViewDisplay.ItemsSource = display.DefaultView;

            //set how table looks like
            ///dataGridViewDisplay.ReadOnly = true;
            //dataGridViewDisplay.Columns[0].Width = 150;
            ///dataGridViewDisplay.Columns[0].Frozen = true;

            ///DataGridViewCellStyle columnCellStyle = new DataGridViewCellStyle();
            ///columnCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewDisplay.Columns[dataGridViewDisplay.Columns.Count - 1].Frozen = true;
            /**dataGridViewDisplay.Columns[dataGridViewDisplay.Columns.Count - 1].DefaultCellStyle = columnCellStyle;

            for (int ii = 0; ii < dataGridViewDisplay.Columns.Count; ii++)
            {
                if (ii == 8)
                {
                    dataGridViewDisplay.Columns[ii].Width = 350;
                    dataGridViewDisplay.Columns[ii].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewDisplay.Columns[ii].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }
                else
                {
                    dataGridViewDisplay.Columns[ii].Width = 55;
                    dataGridViewDisplay.Columns[ii].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }

            for (int ii = 0; ii < dataGridViewDisplay.Rows.Count; ii++)
            {
                dataGridViewDisplay.Rows[ii].Height = 45;
            }

            for (int ii = 0; ii < dataGridViewDisplay.Rows.Count; ii++)
            {
                for (int iii = 1; iii < dataGridViewDisplay.Rows[ii].Cells.Count; iii++)
                    dataGridViewDisplay.Rows[ii].Cells[iii].Style.BackColor = Color.White;
            }**/
        }

        private void dataGridViewDisplay_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGridCellClickRowColumnFormStyle e2 = new DataGridCellClickRowColumnFormStyle();  //provide RowIndex and ColumnIndex from e in forms style
            e2.wpf_e = e;

            TrainingFinalizeHelper trnFinalizHelper = new TrainingFinalizeHelper(actTraining);

            if (trnFinalizHelper.checkNotFinalized()
                & (e2.ColumnIndex > 0
                    & e2.ColumnIndex < dataGridViewDisplay.Columns.Count - 1
                    & e2.RowIndex >= 0
                    & e2.RowIndex < dataGridViewDisplay.Items.Count - 1))
            {
                DataAccess db = new DataAccess();
                int participID = participants[e2.RowIndex].ParticipantID;
                bool boolFormWasCancled = false;
                bool boolNewAboCancled = false;
                decimal decimalFormKeyDecResult = 0;
                string stringFormABCResult = "";

                if (e2.ColumnIndex == 1)//Abo Column******************************************************************
                {
                    Participant actRowParticipant = participants[e2.RowIndex];

                    List<AboFlight> listAbos = new List<AboFlight>();
                    listAbos = db.getUsableAboFlightsByPilotID(actRowParticipant.PilotID, MainWindow.actYEAR);

                    WindowPlusMinusDialog formPlusMinusDialog = new WindowPlusMinusDialog();
                    formPlusMinusDialog.ShowDialog();

                    switch (formPlusMinusDialog.return_string.ToUpper())
                    {
                        case "CANCEL":
                            boolNewAboCancled = true;
                            break;

                        case "PLUS":
                            if (listAbos.Count > 10)
                            {
                                MessageBox.Show("Noch 2 Abos angefangen! Kein Abo-Kauf möglich");
                                boolNewAboCancled = true;
                            }
                            else
                            {
                                List<AboFlight> newAboFlights = new List<AboFlight>();
                                AboFlight newAboFlight = new AboFlight();
                                newAboFlight.PilotID = actRowParticipant.PilotID;
                                newAboFlight.Abo_Year = DateTime.Today.Year;
                                newAboFlights = db.getAllAboFlightsCurrentYear(MainWindow.actYEAR);

                                //find lowest not allready used Nr in YEAR
                                int lowestUnused = 1;
                                for (int i = 1; i < (newAboFlights.Count + 1); i++)
                                {
                                    bool allreadyUsed = false;
                                    foreach (AboFlight AboFl in newAboFlights)
                                    {
                                        if (AboFl.Abo_NrInYear == i)
                                        {
                                            allreadyUsed = true;
                                            break;
                                        }
                                    }
                                    if (!allreadyUsed)
                                    {
                                        lowestUnused = i;
                                        break;
                                    }
                                }
                                newAboFlight.Abo_NrInYear = lowestUnused;
                                newAboFlight.DateBought = actTraining.TrainingDate;
                                newAboFlight.DateFlightPayedWith = DateTime.Parse("1753-01-01");
                                newAboFlight.Comment = "Abo-Flug";
                                newAboFlight.SellerID = 0;
                                foreach (Participant participant in participants)
                                {
                                    if (participant.ParticipantID == actTraining.Leiter1_ID)
                                    {
                                        newAboFlight.SellerID = participant.PilotID;
                                    }
                                }
                                //newAboFlights.Clear();
                                for (int i = 1; i <= 10; i++)
                                {
                                    newAboFlights.Clear();
                                    newAboFlight.FlightNrOnAbo = i;
                                    newAboFlights.Add(newAboFlight);
                                    db.addAboFlights(newAboFlights);
                                }
                                //db.addAboFlights(newAboFlights);
                            }
                            break;

                        case "MINUS":

                            if (listAbos.Count > 0)
                            {
                                List<AboFlight> abosBuyThisTraining = new List<AboFlight>();
                                abosBuyThisTraining = db.getAboFlightNrsDayPilot(actRowParticipant.PilotID, actTraining.TrainingID);
                                if (abosBuyThisTraining.Count >= 1)
                                {
                                    foreach (AboFlight aboFlght in abosBuyThisTraining)
                                    {
                                        if (aboFlght.DateFlightPayedWith.Year < 1800)
                                        {
                                            List<AboFlight> abosToDelete = new List<AboFlight>();
                                            abosToDelete = db.getAboFlightUsablePilotBoughtThisTraining(actRowParticipant.PilotID, actTraining.TrainingID, aboFlght.Abo_NrInYear);
                                            if (abosToDelete.Count == 10)
                                            {
                                                db.deleteAboFlights(abosToDelete);
                                            }
                                            break;
                                        }
                                    }
                                }

                            }
                            break;

                        default:
                            break;

                    }
                }
                else if (e2.ColumnIndex == 8) //alpanumeric ************************
                {
                    string defaultValue = "";
                    string windowTitle = "";
                    tempRowOfBuys = db.getDayPilotBuyByTrainIDParticipID(actTraining.TrainingID, participID);
                    if (tempRowOfBuys.Count > 0 & e2.ColumnIndex == 8)
                    {
                        defaultValue = tempRowOfBuys[0].Remarks;
                        windowTitle = "Enter any remarks";
                    }
                    WindowKeyABC123 formAlphanum = new WindowKeyABC123(true, defaultValue);
                    formAlphanum.Title = windowTitle;
                    formAlphanum.ShowDialog();
                    boolFormWasCancled = formAlphanum.wasCanceled;
                    stringFormABCResult = formAlphanum.return_string;
                }
                else  //numeric column***************************************************************************
                {
                    int defaultValue = 0;
                    bool useDefVal = true;
                    string windowTitle = "";

                    if (e2.ColumnIndex == 2) //LifewestBuy
                    {
                        defaultValue = 85;
                        windowTitle = "Enter prize lifewest (to buy)";
                    }
                    else if (e2.ColumnIndex == 3) //LifewestRent
                    {
                        defaultValue = 5;
                        windowTitle = "Enter prize lifewest (to rent)";
                    }
                    else if (e2.ColumnIndex == 4) //YearFee
                    {
                        defaultValue = 100;
                        windowTitle = "Enter membership fee (YEAR)";
                    }
                    else if (e2.ColumnIndex == 5) //DayFee
                    {
                        defaultValue = 30;
                        windowTitle = "Enter membership fee (Day)";
                    }
                    else if (e2.ColumnIndex == 6) //AxalpWeekFee
                    {
                        defaultValue = 50;
                        windowTitle = "Enter membership fee (Axalpweek)";
                    }
                    else if (e2.ColumnIndex == 7) //Others
                    {
                        useDefVal = false;
                        windowTitle = "Enter prize (others)";
                    }
                    WindowDialogKeyNumDecimal formBuyKeyNumInt = new WindowDialogKeyNumDecimal(true, defaultValue);
                    formBuyKeyNumInt.Title = windowTitle;
                    formBuyKeyNumInt.ShowDialog();
                    boolFormWasCancled = formBuyKeyNumInt.wasCanceled;
                    decimalFormKeyDecResult = formBuyKeyNumInt.return_decimal;
                }
                //update BuyTable
                if (!boolFormWasCancled || e2.ColumnIndex == 1 & !boolNewAboCancled)
                {
                    //Get record by cell position via partitipant and trainingNr
                    tempRowOfBuys = db.getDayPilotBuyByTrainIDParticipID(actTraining.TrainingID, participID);
                    if (tempRowOfBuys.Count == 0)  //add
                    {
                        DayPilotBuy tempDayPilotBuy = new DayPilotBuy();
                        tempDayPilotBuy.ParticipantID = participID;
                        tempDayPilotBuy.TrainingID = actTraining.TrainingID;

                        switch (e2.ColumnIndex)
                        {
                            case 2:
                                tempDayPilotBuy.LifewestBuy = decimalFormKeyDecResult;
                                break;

                            case 3:
                                tempDayPilotBuy.LifewestRent = decimalFormKeyDecResult;
                                break;

                            case 4:
                                tempDayPilotBuy.YearFee = decimalFormKeyDecResult;
                                break;

                            case 5:
                                tempDayPilotBuy.DayFee = decimalFormKeyDecResult;
                                break;

                            case 6:
                                tempDayPilotBuy.AxalpWeekFee = decimalFormKeyDecResult;
                                break;

                            case 7:
                                tempDayPilotBuy.Others = decimalFormKeyDecResult;
                                break;

                            case 8:
                                tempDayPilotBuy.Remarks = stringFormABCResult;
                                break;
                        }
                        tempRowOfBuys.Clear();
                        tempRowOfBuys.Add(tempDayPilotBuy);

                        db.addDayPilotBuy(tempRowOfBuys);
                        BuyUpdateDisplay();
                    }
                    else if (tempRowOfBuys.Count == 1) //update
                    {
                        switch (e2.ColumnIndex)
                        {
                            case 2:
                                tempRowOfBuys[0].LifewestBuy = decimalFormKeyDecResult;
                                break;

                            case 3:
                                tempRowOfBuys[0].LifewestRent = decimalFormKeyDecResult;
                                break;

                            case 4:
                                tempRowOfBuys[0].YearFee = decimalFormKeyDecResult;
                                break;

                            case 5:
                                tempRowOfBuys[0].DayFee = decimalFormKeyDecResult;
                                break;

                            case 6:
                                tempRowOfBuys[0].AxalpWeekFee = decimalFormKeyDecResult;
                                break;

                            case 7:
                                tempRowOfBuys[0].Others = decimalFormKeyDecResult;
                                break;

                            case 8:
                                tempRowOfBuys[0].Remarks = stringFormABCResult;
                                break;
                        }
                        db.updateDayPilotBuy(tempRowOfBuys);
                        BuyUpdateDisplay();
                    }
                    //..set costs
                    //..try to get record in order to know if add or update
                    List<DayPilotCost> tempDayPilotCost = new List<DayPilotCost>();
                    tempDayPilotCost = db.getDayPilotCostsByTrainIDParticipID(actTraining.TrainingID, participID);
                    if (tempDayPilotCost.Count == 0)
                    {
                        //add
                        DayPilotCost dayPilotCostToUpdate = new DayPilotCost();
                        dayPilotCostToUpdate.TrainingID = actTraining.TrainingID;
                        dayPilotCostToUpdate.ParticipantID = participID;
                        try
                        {
                            //old Forms: dayPilotCostToUpdate.CostBuy = decimal.Parse(dataGridViewDisplay.Items[e2.RowIndex].Cells[dataGridViewDisplay.Columns.Count - 1].Value.ToString());
                            dayPilotCostToUpdate.CostBuy = decimal.Parse(dataGridViewDisplay.getValueTextStringByRowColIndexes(e2.RowIndex, dataGridViewDisplay.Columns.Count - 1));
                        }
                        catch (Exception exept)
                        {
                            MessageBox.Show("Error parsing CostBuy : '{exept}'");
                            dayPilotCostToUpdate.CostBuy = 99999;
                        }
                        tempDayPilotCost.Clear();
                        tempDayPilotCost.Add(dayPilotCostToUpdate);
                        db.addDayPilotCost(tempDayPilotCost);
                    }
                    else if (tempDayPilotCost.Count == 1)
                    {
                        try
                        {
                            //old Forms: tempDayPilotCost[0].CostBuy = decimal.Parse(dataGridViewDisplay.Items[e2.RowIndex].Cells[dataGridViewDisplay.Columns.Count - 1].Value.ToString());
                            tempDayPilotCost[0].CostBuy = decimal.Parse(dataGridViewDisplay.getValueTextStringByRowColIndexes(e2.RowIndex, dataGridViewDisplay.Columns.Count - 1));
                        }
                        catch (Exception exept)
                        {
                            MessageBox.Show("Error parsing CostBuy : '{exept}'");
                            tempDayPilotCost[0].CostBuy = 99999;
                        }
                        db.updateDayPilotCosts(tempDayPilotCost);
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
