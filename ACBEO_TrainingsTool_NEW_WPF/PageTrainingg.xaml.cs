﻿using System;
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
using MyWPFExtentions;

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
        private string tempMemory_Remark;

        public Training readActTraining
        {
            get{return actTraining;}
            private set { actTraining = value;}    
        }

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

        private void Page_Training_Loaded(object sender, RoutedEventArgs e)
        {
            setDataGridSummaryLooking();
        }

        private void setDataGridSummaryLooking()
        {//set how table looks like
            //dataGridViewSummary.SelectionUnit = DataGridSelectionUnit.Cell;
            SolidColorBrush bgbrush = new SolidColorBrush(Colors.White);
            dataGridViewSummary.setBgColorByRowColIndexes(4, 1, bgbrush);
            dataGridViewSummary.setBgColorByRowColIndexes(5, 2, bgbrush);
            dataGridViewSummary.setBgColorByRowColIndexes(5, 5, bgbrush);
            dataGridViewSummary.setBgColorByRowColIndexes(5, 6, bgbrush);
            dataGridViewSummary.setBgColorByRowColIndexes(6, 5, bgbrush);
            dataGridViewSummary.UnselectAll();
            TrainingFinalizeHelper calcColor = new TrainingFinalizeHelper(actTraining);
            mainGrid.Background = calcColor.calcBGBrush();
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
            decimal totalPayedByPltsTwint = 0;
            bool flagAllPilotsHavePayed = true;
            List<string> tempRowSummary = new List<string>();
            foreach (DayPilotCost dayPilotCost in dayPilotCosts)
            {
                totalPayedByPilots = totalPayedByPilots + dayPilotCost.PayedAmount;
                totalPayedByPltsTwint = totalPayedByPltsTwint + dayPilotCost.PayedTwint;
                if (!dayPilotCost.PayedFlag)
                {
                    flagAllPilotsHavePayed = false;
                }
            }
            displaySummary.Clear();
            while (displaySummary.Columns.Count < 7)  //create table with headers
            {
                displaySummary.Columns.Add();
            }
            displaySummary.Columns[0].ColumnName = "What";
            displaySummary.Columns[1].ColumnName = "Income";
            displaySummary.Columns[2].ColumnName = "spent";
            displaySummary.Columns[3].ColumnName = "totals";
            displaySummary.Columns[4].ColumnName = "Comment1";
            displaySummary.Columns[5].ColumnName = "Comment2";
            displaySummary.Columns[6].ColumnName = "Date and Time from TWINT App";

            //Row 0
            tempRowSummary.Clear();
            tempRowSummary.Add("Total Cost Training");
            tempRowSummary.Add("");
            tempRowSummary.Add(totalCostTraining.ToString());
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            displaySummary.Rows.Add(tempRowSummary.ToArray());

            //Row 1
            tempRowSummary.Clear();
            tempRowSummary.Add("Total Payed by Pilots CASH"); 
            tempRowSummary.Add(totalPayedByPilots.ToString());
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            displaySummary.Rows.Add(tempRowSummary.ToArray());

            //Row 2
            tempRowSummary.Clear();
            tempRowSummary.Add("Total Payed by Pilots TWINT");
            tempRowSummary.Add(totalPayedByPltsTwint.ToString());
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            displaySummary.Rows.Add(tempRowSummary.ToArray());

            //Row 3
            tempRowSummary.Clear();
            tempRowSummary.Add("Win/loss of this Training");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            decimal totThisTraining = totalPayedByPilots + totalPayedByPltsTwint - totalCostTraining;
            tempRowSummary.Add(totThisTraining.ToString());
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            displaySummary.Rows.Add(tempRowSummary.ToArray());

            //Row 4
            tempRowSummary.Clear();
            tempRowSummary.Add("Money Before Training");
            tempRowSummary.Add(actTraining.CashAtBegin.ToString());
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            displaySummary.Rows.Add(tempRowSummary.ToArray());

            //Row 5
            tempRowSummary.Clear();
            tempRowSummary.Add("Money Payed to ACBEO");
            tempRowSummary.Add("");
            tempRowSummary.Add(actTraining.CashToACBEO.ToString());
            tempRowSummary.Add("");
            tempRowSummary.Add("payed by:");
            tempRowSummary.Add(actTraining.CashToACBEO_PayedBy);
            tempRowSummary.Add(actTraining.PayedTwintReference);
            displaySummary.Rows.Add(tempRowSummary.ToArray());

            //Row 6
            tempRowSummary.Clear();
            tempRowSummary.Add("Money left");
            tempRowSummary.Add("");
            tempRowSummary.Add("");
            tempRowSummary.Add((actTraining.CashAtBegin + totalPayedByPilots - totalCostTraining - actTraining.CashToACBEO - totalPayedByPltsTwint).ToString());
            tempRowSummary.Add("finalized");
            if (actTraining.Finalized) { tempRowSummary.Add("YES"); } else { tempRowSummary.Add("NOT YET"); }
            displaySummary.Rows.Add(tempRowSummary.ToArray());
            dataGridViewSummary.ItemsSource = displaySummary.DefaultView;  //DefaultView new due to WPF

            //set how table looks like
            setDataGridSummaryLooking();
            //dataGridViewSummary.SelectionUnit = DataGridSelectionUnit.Cell;
            dataGridViewSummary.UnselectAll();

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
            textBoxRemark.Text = actTraining.Remarks;
            tempMemory_Remark = actTraining.Remarks;
        }

        private void emptyDisplays()
        {
            displayTrainingCosts.Clear();
            dataGridViewDispTrnCosts.ItemsSource = displayTrainingCosts.DefaultView;  //DefaultView new due to WPF
            displaySummary.Clear();
            dataGridViewSummary.ItemsSource = displaySummary.DefaultView;  //DefaultView new due to WPF
        }

        private void updateTrainingInDB(Training trainingToUpdate)
        {
            //update Training in database
            if (actTrningID > 0) //old: (trainingOpen)
            {
                DataAccess dbupdTrain = new DataAccess();
                List<Training> trainingsToUpdate = new List<Training>();
                trainingsToUpdate.Add(trainingToUpdate);
                dbupdTrain.updateTraining(trainingsToUpdate);
            } 
        }

        private void dataGridViewDispTrnCosts_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string pathBelegPhoto = "C:/ACBEO_TrainingsBelege";
            string nameBelegPhoto;
            string belegPathComplete;

            DataGridCellClickRowColumnFormStyle e2 = new DataGridCellClickRowColumnFormStyle();  //provide RowIndex and ColumnIndex from e in forms style
            e2.wpf_e = e;

            TrainingFinalizeHelper trnFinalizHelper = new TrainingFinalizeHelper(actTraining);

            if (trnFinalizHelper.checkNotFinalized()
                & (e2.ColumnIndex >= 0
                    & e2.ColumnIndex < dataGridViewDispTrnCosts.Columns.Count
                    & e2.RowIndex >= 0
                    & e2.RowIndex < dataGridViewDispTrnCosts.Items.Count & e2.isCell))
            {
                DataAccess db = new DataAccess();
                List<TrainingCost> rowTrainingCostToEdit = new List<TrainingCost>();
                rowTrainingCostToEdit = db.getTrainingCostByDate(actTraining.TrainingDate);

                int trainingCostID = 0;
                if (rowTrainingCostToEdit.Count >= e2.RowIndex + 1)
                {
                    trainingCostID = rowTrainingCostToEdit[e2.RowIndex].TrainingCostID;
                }

                bool boolFormWasCancled = false;
                bool picFormWasCancled = false;
                decimal decimalFormKeyDecResult = 0;
                string stringFormABCResult = "";
                string oldBelegNrBeforeChanging = "";

                if (e2.ColumnIndex == 1 || e2.ColumnIndex == 2) //alpanumeric ************************
                {
                    string defaultValue = "";
                    bool useDefaultVal = false;
                    string windowTitle = "";
                    rowTrainingCostToEdit = db.getTrainingCostByID(trainingCostID);
                    if (rowTrainingCostToEdit.Count > 0)
                    {
                        switch (e2.ColumnIndex)
                        {
                            case 1:
                                defaultValue = rowTrainingCostToEdit[0].Kommentar;
                                useDefaultVal = true;
                                windowTitle = "Enter comment (what was the expense)";
                                break;

                            case 2:
                                defaultValue = rowTrainingCostToEdit[0].BelegNr;
                                useDefaultVal = true;
                                windowTitle = "Enter receipt nbr (BelegNr)";
                                oldBelegNrBeforeChanging = rowTrainingCostToEdit[0].BelegNr;
                                break;
                        }
                    }
                    WindowKeyABC123 formAlphanum = new WindowKeyABC123(true, defaultValue);
                    formAlphanum.Owner = App.Current.MainWindow;
                    formAlphanum.Title = windowTitle;
                    formAlphanum.ShowDialog();
                    boolFormWasCancled = formAlphanum.wasCanceled;
                    stringFormABCResult = formAlphanum.return_string;
                }
                else if (e2.ColumnIndex == 3) //beleg photo************************
                {
                    if (rowTrainingCostToEdit.Count > 0)
                    {
                        if (rowTrainingCostToEdit[e2.RowIndex].BelegNr != ""
                            & rowTrainingCostToEdit[e2.RowIndex].BelegNr != " ")
                        {

                            //pathBelegPhoto = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
                            //nameBelegPhoto.Replace('.', '_');
                            //nameBelegPhoto = $"{nameBelegPhoto}_{rowTrainingCostToEdit[0].BelegNr}";

                            nameBelegPhoto = $"Beleg{actTraining.TrainingDate.Year.ToString()}_{actTraining.TrainingDate.Month.ToString()}_{actTraining.TrainingDate.Date.Day.ToString()}_{rowTrainingCostToEdit[e2.RowIndex].BelegNr}.jpg";

                            WindowBelegPhoto formBelegPhoto = new WindowBelegPhoto(pathBelegPhoto, nameBelegPhoto);
                            formBelegPhoto.Owner = App.Current.MainWindow;
                            formBelegPhoto.ShowDialog();
                            picFormWasCancled = formBelegPhoto.wasCanceled;
                            stringFormABCResult = formBelegPhoto.return_string;
                        }
                        else
                        {
                            MessageBox.Show("First Enter BelegNr!/Zuerst BelegNr eingeben!");
                            picFormWasCancled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("First Enter BelegNr!/Zuerst BelegNr eingeben!");
                        picFormWasCancled = true;
                    }
                }
                else  //numeric column***************************************************************************
                {
                    int defaultValue = 0;
                    bool useDefVal = true;
                    string windowTitle = "";

                    rowTrainingCostToEdit = db.getTrainingCostByID(trainingCostID);

                    if (e2.ColumnIndex == 0)
                    {
                        defaultValue = 0;
                        windowTitle = "Enter amount (Betrag)";
                    }
                    else
                    {
                        useDefVal = false;
                    }
                    WindowDialogKeyNumDecimal formBuyKeyNumInt = new WindowDialogKeyNumDecimal(true, defaultValue);
                    formBuyKeyNumInt.Owner = App.Current.MainWindow;
                    formBuyKeyNumInt.Title = windowTitle;
                    formBuyKeyNumInt.ShowDialog();
                    boolFormWasCancled = formBuyKeyNumInt.wasCanceled;
                    decimalFormKeyDecResult = formBuyKeyNumInt.return_decimal;
                }
                //update TrainingCostTable
                if (!boolFormWasCancled & !picFormWasCancled)
                {
                    if (rowTrainingCostToEdit.Count <= 0)
                    {   //add
                        TrainingCost trainingCostToAdd = new TrainingCost();
                        rowTrainingCostToEdit.Add(trainingCostToAdd);
                        rowTrainingCostToEdit[0].TrainingDate = actTraining.TrainingDate;
                        rowTrainingCostToEdit[0].Betrag = 0;
                        rowTrainingCostToEdit[0].Kommentar = "noch kein Kommentar!!!";
                        switch (e2.ColumnIndex)
                        {
                            case 0:
                                rowTrainingCostToEdit[0].Betrag = decimalFormKeyDecResult;
                                break;

                            case 1:
                                rowTrainingCostToEdit[0].Kommentar = stringFormABCResult;
                                break;

                            case 2:
                                rowTrainingCostToEdit[0].BelegNr = stringFormABCResult;
                                break;

                            case 3:
                                rowTrainingCostToEdit[0].BelegPhotoName = stringFormABCResult;
                                break;
                        }
                        db.addTrainingCosts(rowTrainingCostToEdit);
                    }
                    else
                    {
                        switch (e2.ColumnIndex)
                        {
                            case 0:
                                rowTrainingCostToEdit[0].Betrag = decimalFormKeyDecResult;
                                break;

                            case 1:
                                rowTrainingCostToEdit[0].Kommentar = stringFormABCResult;
                                break;

                            case 2:
                                rowTrainingCostToEdit[0].BelegNr = stringFormABCResult;
                                break;

                            case 3:
                                rowTrainingCostToEdit[0].BelegPhotoName = stringFormABCResult;
                                break;
                        }
                        db.updateTrainingCosts(rowTrainingCostToEdit);
                    }
                    //update filename if "BelegNr" changed
                    if (e2.ColumnIndex == 2 || e2.ColumnIndex == 3)
                    {
                        string oldNameBelegPhoto = $"Beleg{actTraining.TrainingDate.Year.ToString()}_{actTraining.TrainingDate.Month.ToString()}_{actTraining.TrainingDate.Date.Day.ToString()}_{oldBelegNrBeforeChanging}.jpg";
                        string oldBelegPathComplete = $"{pathBelegPhoto}/{oldNameBelegPhoto}";

                        nameBelegPhoto = $"Beleg{actTraining.TrainingDate.Year.ToString()}_{actTraining.TrainingDate.Month.ToString()}_{actTraining.TrainingDate.Date.Day.ToString()}_{rowTrainingCostToEdit[0].BelegNr}.jpg";
                        belegPathComplete = $"{pathBelegPhoto}/{nameBelegPhoto}";

                        if (System.IO.File.Exists(oldBelegPathComplete))
                        {
                            try
                            {
                                System.IO.File.Move(oldBelegPathComplete, belegPathComplete);
                            }
                            catch
                            {
                                MessageBox.Show($"Error while renaming");
                            }
                        }

                        List<TrainingCost> costRecordsWithSameBelegNr = new List<TrainingCost>();
                        costRecordsWithSameBelegNr = db.getTrainingCostByDateAndBelegNr(actTraining.TrainingDate, rowTrainingCostToEdit[0].BelegNr);

                        if (costRecordsWithSameBelegNr.Count > 0)
                        {
                            bool belegPicExists = false;
                            foreach (TrainingCost costRecord in costRecordsWithSameBelegNr)
                            {
                                if (costRecord.BelegPhotoName != "")
                                {
                                    belegPicExists = true;
                                }
                                costRecord.BelegPhotoName = belegPathComplete;
                            }
                            if (belegPicExists || e2.ColumnIndex == 3) //if BelegNr was updated, update pic paht only if at least one exists
                            {
                                db.updateTrainingCosts(costRecordsWithSameBelegNr);
                            }
                        }
                        //delete BelegNr for all records with old BelegNr
                        if (e2.ColumnIndex == 2 & oldBelegNrBeforeChanging != "" & oldBelegNrBeforeChanging != rowTrainingCostToEdit[0].BelegNr)
                        {
                            List<TrainingCost> costRecordsWithOldBelegNr = new List<TrainingCost>();
                            costRecordsWithOldBelegNr = db.getTrainingCostByDateAndBelegNr(actTraining.TrainingDate, oldBelegNrBeforeChanging);

                            bool belegPicExists = false;
                            if (costRecordsWithOldBelegNr.Count > 0)
                            {
                                foreach (TrainingCost costRecord in costRecordsWithOldBelegNr)
                                {
                                    costRecord.BelegPhotoName = "";
                                }
                                db.updateTrainingCosts(costRecordsWithOldBelegNr);
                            }
                        }
                    }
                    updateDisplay();
                }
            }
            dataGridViewDispTrnCosts.UnselectAllCells();
        }

        private void dataGridViewSummary_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGridCellClickRowColumnFormStyle e2 = new DataGridCellClickRowColumnFormStyle();  //provide RowIndex and ColumnIndex from e in forms style
            e2.wpf_e = e;

            bool tempIsNotFinalized;
            if (e2.ColumnIndex == 5 & e2.RowIndex == 6)
            {
            tempIsNotFinalized = true;
            }
            else
            {
                TrainingFinalizeHelper trnFinalizHelper = new TrainingFinalizeHelper(actTraining);
                tempIsNotFinalized = trnFinalizHelper.checkNotFinalized();
            }

            if (tempIsNotFinalized //(trnFinalizHelper.checkNotFinalized()
                & (e2.ColumnIndex == 1
                    & e2.RowIndex == 4
                    || e2.ColumnIndex == 2
                    & e2.RowIndex == 5
                    & actTrningID > 0 //old: & trainingOpen)
                    || e2.ColumnIndex == 5
                    & e2.RowIndex == 5
                    || e2.ColumnIndex == 6
                    & e2.RowIndex == 5
                    || e2.ColumnIndex == 5
                    & e2.RowIndex == 6))
            {
                decimal defaultValue = 0;
                string defaultValueStr = "";
                bool useDefVal = true;
                string windowTitle = "";
                bool boolFormWasCancled = false;
                decimal decimalFormKeyDecResult = 0;
                string stringFormABCResult = "";

                if (e2.ColumnIndex == 1 /*|| e2.ColumnIndex == 2* handeled in its own case*/) //numeric column***************************************************************************
                {
                    if (e2.ColumnIndex == 1) //Cash before
                    {
                        defaultValue = actTraining.CashAtBegin;
                        windowTitle = "Enter cash in briefcase before training (please count!)";
                    }
                    else
                    {
                        useDefVal = false;
                    }
                    WindowDialogKeyNumDecimal formBuyKeyNumDecimal = new WindowDialogKeyNumDecimal(true, defaultValue);
                    formBuyKeyNumDecimal.Owner = App.Current.MainWindow;
                    formBuyKeyNumDecimal.Title = windowTitle;
                    formBuyKeyNumDecimal.ShowDialog();
                    boolFormWasCancled = formBuyKeyNumDecimal.wasCanceled;
                    decimalFormKeyDecResult = formBuyKeyNumDecimal.return_decimal;

                    if (!boolFormWasCancled)
                    {
                        if (e2.ColumnIndex == 1) //Cash before
                        {
                            actTraining.CashAtBegin = formBuyKeyNumDecimal.return_decimal;
                        }
                        //update Training in database
                        updateTrainingInDB(actTraining);

                        updateDisplay();
                    }

                }
                else if (e2.RowIndex == 5 & (e2.ColumnIndex == 2 || e2.ColumnIndex == 5 || e2.ColumnIndex == 6)) //special payed to acbeo with amount and payed by ************************
                {
                    defaultValue = actTraining.CashToACBEO;
                    WindowDialogKeyNumDecimal formBuyKeyNumDecimal = new WindowDialogKeyNumDecimal(true, defaultValue);
                    formBuyKeyNumDecimal.Title = "Please put here amount transfaired to ACBEO";
                    defaultValueStr = actTraining.CashToACBEO_PayedBy;
                    WindowKeyABC123 formAlphanum = new WindowKeyABC123(true, defaultValueStr);
                    formAlphanum.Title = "Please put here name of person who pays to ACBEO";
                    bool dbupdate_needed = false;

                    if (e2.ColumnIndex == 2 || actTraining.CashToACBEO <= 0) //edit "amount" when clicked or when still 0
                    {
                        formBuyKeyNumDecimal.Owner = App.Current.MainWindow;
                        formBuyKeyNumDecimal.ShowDialog();
                        boolFormWasCancled = formBuyKeyNumDecimal.wasCanceled;
                        dbupdate_needed = !formBuyKeyNumDecimal.wasCanceled;
                        decimalFormKeyDecResult = formBuyKeyNumDecimal.return_decimal;

                        if (!boolFormWasCancled)
                        {
                            actTraining.CashToACBEO = formBuyKeyNumDecimal.return_decimal;
                        }
                    }

                    if (!boolFormWasCancled & (e2.ColumnIndex == 5 || actTraining.CashToACBEO_PayedBy == "")) //edit "payed by" when clicked or still empty
                    {
                        formAlphanum.Owner = App.Current.MainWindow;
                        formAlphanum.ShowDialog();
                        boolFormWasCancled = formAlphanum.wasCanceled;
                        dbupdate_needed = dbupdate_needed || !formAlphanum.wasCanceled;
                        stringFormABCResult = formAlphanum.return_string;

                        if (!boolFormWasCancled)
                        {
                            actTraining.CashToACBEO_PayedBy = formAlphanum.return_string;
                        }
                    }

                    if (actTraining.CashToACBEO > 0 & actTraining.CashToACBEO_PayedBy != "")  //show QR when both were filled
                    {
                        decimal paymentamount = formBuyKeyNumDecimal.return_decimal;
                        string paymentmessage = $"{actTraining.TrainingDate.ToShortDateString()};Abschöpfung Training;{string.Format("{0:0.00}", paymentamount)}";
                        WindowCreateShowSwissQR formShowSwissQRBill = new WindowCreateShowSwissQR(paymentamount, paymentmessage, false);
                        formShowSwissQRBill.Title = "Here you can photograph or scan QR code to pay the money to ACBEO via TWINT or e-banking";
                        formShowSwissQRBill.ShowDialog();
                        if (e2.ColumnIndex == 6 || actTraining.PayedTwintReference == "" & formShowSwissQRBill.twintIcludedInQR) //edit "TwintRef" when clicked or still empty
                        {
                            defaultValueStr = actTraining.PayedTwintReference;
                            WindowKeyABC123 formAlphanum2 = new WindowKeyABC123(true, defaultValueStr);
                            formAlphanum2.Title = "Please put here Date and Time from Twint-App as Reference";
                            formAlphanum2.Owner = App.Current.MainWindow;
                            formAlphanum2.ShowDialog();
                            boolFormWasCancled = formAlphanum2.wasCanceled;
                            dbupdate_needed = dbupdate_needed || !formAlphanum2.wasCanceled;
                            stringFormABCResult = formAlphanum2.return_string;

                            if (!boolFormWasCancled)
                            {
                                actTraining.PayedTwintReference = formAlphanum2.return_string;
                            }
                        }
                    }

                    if (dbupdate_needed)
                    {
                        //update Training in database
                        updateTrainingInDB(actTraining);

                        updateDisplay();
                    }
                }
                else if (e2.ColumnIndex == 5 & e2.RowIndex == 6) //special finalize ************************
                {
                    WindowFinalizeTraining formFinalizeTraining = new WindowFinalizeTraining(actTraining.Finalized);
                    formFinalizeTraining.Title = $"finalize the Training from {actTraining.TrainingDate.ToShortDateString()}";
                    formFinalizeTraining.Owner = App.Current.MainWindow;
                    formFinalizeTraining.ShowDialog();
                    boolFormWasCancled = false;
                    switch (formFinalizeTraining.return_string)
                    {
                        case "FINALIZE":
                            DataAccess dbUpdate = new DataAccess();
                            trainingCosts = dbUpdate.getTrainingCostByDate(actTraining.TrainingDate);
                            actTraining.Finalized = true;
                            foreach (DayPilotCost dayPilotCost in dayPilotCosts)
                            {
                                if (!dayPilotCost.testIfPayed()) 
                                {
                                    //actTraining.Finalized = false;
                                    break;
                                }
                            }
                            break;

                        case "UNFINALIZE":
                            actTraining.Finalized = false;
                            break;

                        case "CANCEL":
                            boolFormWasCancled = true;
                            break;
                    }
                    if (!boolFormWasCancled)
                    {
                        //update Training in database
                        updateTrainingInDB(actTraining);

                        updateDisplay();
                    }
                }
                    //else if (e2.ColumnIndex == ) //alpanumeric ************************
                    //{
                    //    if (e2.ColumnIndex == )
                    //    {

                    //    }
                    //    defaultValueStr = 
                    //    WindowKeyABC123 formAlphanum = new WindowKeyABC123(true, defaultValueStr);
                    //    formAlphanum.Owner = App.Current.MainWindow;
                    //    formAlphanum.ShowDialog();
                    //    boolFormWasCancled = formAlphanum.wasCanceled;
                    //    stringFormABCResult = formAlphanum.return_string;

                    //    if (!boolFormWasCancled)
                    //    {
                    //        if (e2.ColumnIndex == ) //
                    //        {
                    //            actTraining. = formAlphanum.return_string;
                    //        }

                    //        //update Training in database
                    //        updateTrainingInDB(actTraining);

                    //        updateDisplay();
                    //    }
                    //}
                }
            dataGridViewSummary.UnselectAllCells();
        }

        //update remark when changed after focus lost...
        private void textBoxRemark_GotFocus(object sender, RoutedEventArgs e)
        {
            tempMemory_Remark = textBoxRemark.Text.ToString();
            TrainingFinalizeHelper trnFinalizHelper = new TrainingFinalizeHelper(actTraining);
            if(!trnFinalizHelper.checkNotFinalized())
            {
                textBoxRemark.IsEnabled = false;
            }
        }
        private void textBoxRemark_LostFocus(object sender, RoutedEventArgs e)
        {
            textBoxRemark.IsEnabled = true;
            if (tempMemory_Remark != textBoxRemark.Text.ToString())
            {
                actTraining.Remarks = textBoxRemark.Text.ToString();
                //update Training in database
                updateTrainingInDB(actTraining);

                updateDisplay();
            }
        }
    }
}