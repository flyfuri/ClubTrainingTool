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

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for PageTurns.xaml
    /// </summary>
    public partial class PageTurns : Page
    {
        private int actTrningID;
        private Training actTraining;
        private int actYear;
        int totalFlightToPay;
        public PageTurns(Training actualTraining, int actualYear)
        {
            InitializeComponent();
            actTraining = actualTraining;
            actTrningID = actualTraining.TrainingID;
            actYear = actualYear;
        }

        List<Participant> participants = new List<Participant>();
        List<Turn> tempRowOfTurns = new List<Turn>();
        List<AboFlight> validAboFlights = new List<AboFlight>();
        List<Color> bgColorNames = new List<Color>();
        DataTable display = new DataTable();
        int iNbrOfValidAboFlights;
        //DataTable displaycolors = new DataTable();

        private void Page_Turns_Loaded(object sender, RoutedEventArgs e)
        {
            TurnsUpdateDisplay();
        }

        private void TurnsUpdateDisplay()
        {
            display.Clear();
            bgColorNames.Clear();

            DataAccess dbParticpants = new DataAccess();
            participants = dbParticpants.getParticipants(actTrningID);

            //add Pilot coumn
            if (display.Columns.Count == 0)
            {
                display.Columns.Add();
                display.Columns[0].ColumnName = "Pilot";
            }

            int i = 0;
            int highestTurnNbr = 0;
            List<string> listTempRow = new List<string>();
            //List<string> listTempRowColor = new List<string>();

            foreach (Participant participant in participants)
            {
                tempRowOfTurns = dbParticpants.getTurnsByTrainIDParticipID(actTrningID, participant.ParticipantID);
                foreach (Turn turn in tempRowOfTurns)
                {
                    if (turn.TurnNr > highestTurnNbr)
                    {
                        highestTurnNbr = turn.TurnNr;
                    }
                }

            }

            foreach (Participant participant in participants)
            {
                totalFlightToPay = 0;

                if (participant.ParticipantID == actTraining.Leiter1_ID)
                {
                    //old :textBoxLeiter1.Text = participant.ComplNameAndLicence;
                    totalFlightToPay--;
                }

                if (participant.ParticipantID == actTraining.Leiter2_ID)
                {
                    //old: textBoxLeiter2.Text = participant.ComplNameAndLicence;
                    totalFlightToPay--;
                }

                tempRowOfTurns = dbParticpants.getTurnsByTrainIDParticipID(actTrningID, participant.ParticipantID);

                //add columns if needed
                if (highestTurnNbr + 3 > display.Columns.Count)
                {
                    for (int ii = display.Columns.Count; ii < (highestTurnNbr + 3); ii++)
                    {
                        display.Columns.Add();
                    }
                }

                //add rows
                listTempRow.Clear();

                for (int iii = 0; iii < highestTurnNbr; iii++)
                {
                    bool found = false;
                    foreach (Turn turn in tempRowOfTurns)
                    {
                        if (turn.TurnNr == (iii + 1))
                        {
                            listTempRow.Add(turn.Flight);
                            found = true;

                            if (turn.Flight.StartsWith("FLIGHT"))
                            {
                                totalFlightToPay++;
                            }
                            else if (turn.Flight.StartsWith("BUS") || turn.Flight.StartsWith("BOAT")
                                     || turn.Flight.StartsWith("BandB") || turn.Flight.StartsWith("FnBnB"))
                            {
                                totalFlightToPay--;
                            }
                            break;
                        }
                    }
                    if (!found)
                    {
                        listTempRow.Add("");
                    }

                }

                //name turn columns
                for (int ii = 1; ii < display.Columns.Count; ii++)
                {
                    string colname = "Turn" + ii.ToString();
                    display.Columns[ii].ColumnName = colname;
                }

                //add total cost column
                display.Columns[display.Columns.Count - 1].ColumnName = "Cost";

                listTempRow.Insert(0, participant.ComplNameAndLicence);

                iNbrOfValidAboFlights = dbParticpants.getUsableAboFlightsByPilotID(participant.PilotID, actYear).Count
                                        + (dbParticpants.getDayPilotCostsByTrainIDParticipID(actTrningID, participant.ParticipantID)[0].payedWithAbo);
                if (totalFlightToPay <= iNbrOfValidAboFlights)
                {
                    bgColorNames.Add(SystemColors.ControlColor);//OLD: ControlLight);
                }
                else
                {
                    bgColorNames.Add(Color.FromRgb(10, 10, 10));    //OLD: .Orange) ; 
                }


                display.Rows.Add();
                totalFlightToPay = totalFlightToPay * 9;

                if (listTempRow.Count < (display.Columns.Count - 1))
                {
                    for (int ii = listTempRow.Count; ii < (display.Columns.Count - 1); ii++)
                    {
                        listTempRow.Add("");
                    }
                }
                listTempRow.Add(totalFlightToPay.ToString());
                display.Rows[i].ItemArray = listTempRow.ToArray();

                i++;
            }

            dataGridViewDisplay.ItemsSource = display.DefaultView;

            //set how table looks like
            /*dataGridViewDisplay.ReadOnly = true;
            dataGridViewDisplay.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDisplay.Columns[0].Frozen = true;

            DataGridViewCellStyle columnCellStyle = new DataGridViewCellStyle();
            columnCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 

            //dataGridViewDisplay.Columns[dataGridViewDisplay.Columns.Count - 1].Frozen = true;
            dataGridViewDisplay.Columns[dataGridViewDisplay.Columns.Count - 1].DefaultCellStyle = columnCellStyle;**/

            /*for (int ii = 0; ii < dataGridViewDisplay.Items.Count && ii < bgColorNames.Count; ii++)
            {
                //dataGridViewDisplay.Items[ii].Cells[0].Style.BackColor = bgColorNames[ii];
            }*/
            for (int ii = 0; ii < dataGridViewDisplay.Items.Count && ii < bgColorNames.Count; ii++)
            {
                //dataGridViewDisplay.Items[ii].Cells[0].Style.BackColor = bgColorNames[ii];
            }


        }

        private void SetCosts(int participantID, int displayRowIndex)
        {
            //..set costs
            //..try to get record in order to know if add or update
            List<DayPilotCost> tempDayPilotCost = new List<DayPilotCost>();
            DataAccess dbCost = new DataAccess();
            tempDayPilotCost = dbCost.getDayPilotCostsByTrainIDParticipID(actTrningID, participantID);
            if (tempDayPilotCost.Count == 0)
            {
                //add
                DayPilotCost dayPilotCostToUpdate = new DayPilotCost();
                dayPilotCostToUpdate.TrainingID = actTrningID;
                dayPilotCostToUpdate.ParticipantID = participantID;
                try
                {
                    dayPilotCostToUpdate.CostFlights = decimal.Parse(dataGridViewDisplay.Items[displayRowIndex].Cells[dataGridViewDisplay.Columns.Count - 1].Value.ToString());
                }
                catch (Exception exept)
                {
                    MessageBox.Show("Error parsing CostBuy : '{exept}'");
                    dayPilotCostToUpdate.CostFlights = 99999;
                }
                tempDayPilotCost.Clear();
                tempDayPilotCost.Add(dayPilotCostToUpdate);
                dbCost.addDayPilotCost(tempDayPilotCost);
            }
            else if (tempDayPilotCost.Count == 1)
            {
                try
                {
                    tempDayPilotCost[0].CostFlights = decimal.Parse(dataGridViewDisplay.Items[displayRowIndex].Cells[dataGridViewDisplay.Columns.Count - 1].Value.ToString());
                }
                catch (Exception exept)
                {
                    MessageBox.Show("Error parsing CostBuy : '{exept}'");
                    tempDayPilotCost[0].CostFlights = 99999;
                }
                dbCost.updateDayPilotCosts(tempDayPilotCost);
            }
        }

        private void dataGridViewDisplay_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGridCellClickRowColumnFormStyle e2 = new DataGridCellClickRowColumnFormStyle();  //provide RowIndex and ColumnIndex from e in forms style
            e2.wpf_e = e;
            //show pilotinfos
            if (e2.ColumnIndex == 0
                & e2.RowIndex >= 0
                & e2.RowIndex < dataGridViewDisplay.Items.Count & e2.isCell) //dataGridViewDisplay.Items.Count - 1)
            {
                DataAccess db = new DataAccess();
                List<AboFlight> validAboFlights = new List<AboFlight>();
                string codeToShow = $"{participants[e2.RowIndex].ComplNameAndLicence}__{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}";
                validAboFlights = db.getUsableAboFlightsByPilotID(participants[e2.RowIndex].PilotID, actYear);
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
                    WindowAboPilotOverview formAboOverview = new WindowAboPilotOverview(participants[e2.RowIndex].PilotID, participants[e2.RowIndex].ComplNameAndLicence, actYear);
                    formAboOverview.ShowDialog();
                }

            }
            else if (e2.ColumnIndex > 0
                & e2.ColumnIndex < dataGridViewDisplay.Columns.Count - 1
                & e2.RowIndex >= 0
                & e2.RowIndex < dataGridViewDisplay.Items.Count - 1)
            {
                WindowFillTurn formFillTurn = new WindowFillTurn();
                formFillTurn.ShowDialog();
                if (formFillTurn.return_string != "CANCEL")
                {
                    dataGridViewDisplay.Items[e2.RowIndex].Cells[e2.ColumnIndex].Value = formFillTurn.return_string;
                }

                //Get record by cell position via partitipant and turnNr
                int participantID = participants[e2.RowIndex].ParticipantID;
                int turnNr = e2.ColumnIndex;
                int turnID;

                DataAccess db = new DataAccess();
                tempRowOfTurns = db.getTurnsByTrainIDParticipID(actTrningID, participantID);
                if (tempRowOfTurns.Count == 0)
                {
                    Turn tempTurn = new Turn();
                    tempTurn.TrainingID = actTrningID;
                    tempTurn.ParticipantID = participantID;
                    tempTurn.TurnNr = turnNr;
                    tempTurn.Flight = dataGridViewDisplay.Items[e2.RowIndex].Cells[e2.ColumnIndex].Value.ToString();

                    tempRowOfTurns.Clear();
                    tempRowOfTurns.Add(tempTurn);

                    db.addTurn(tempRowOfTurns);
                    TurnsUpdateDisplay();
                }
                else
                {
                    //get turnID out of that record and set Flight in list
                    turnID = -1;
                    int i = -1;
                    foreach (Turn turn in tempRowOfTurns)
                    {
                        i++;
                        if (turn.TurnNr == turnNr)
                        {
                            turnID = turn.TurnID;
                            tempRowOfTurns[i].Flight = dataGridViewDisplay.Items[e2.RowIndex].Cells[e2.ColumnIndex].Value.ToString();
                            break;
                        }
                    }
                    //if turnID found.. 
                    if (turnID >= 0)
                    {
                        //..try to get record in order to know if add or update
                        List<Turn> tempCheckTurns = new List<Turn>();
                        tempCheckTurns = db.getTurnByTurnID(turnID);
                        if (tempCheckTurns.Count == 0)
                        {
                            //add
                            Turn tempTurn = new Turn();
                            tempTurn.TrainingID = actTrningID;
                            tempTurn.ParticipantID = participantID;
                            tempTurn.TurnNr = turnNr;
                            tempTurn.Flight = dataGridViewDisplay.Items[e2.RowIndex].Cells[e2.ColumnIndex].Value.ToString();

                            tempRowOfTurns.Clear();
                            tempRowOfTurns.Add(tempTurn);

                            db.addTurn(tempRowOfTurns);
                        }
                        else if (tempCheckTurns.Count == 1)
                        {
                            //update
                            tempRowOfTurns[0].Flight = dataGridViewDisplay.Items[e2.RowIndex].Cells[e2.ColumnIndex].Value.ToString();
                            db.updateTurns(tempRowOfTurns);
                        }
                    }
                    else
                    {
                        //add
                        Turn tempTurn = new Turn();
                        tempTurn.TrainingID = actTrningID;
                        tempTurn.ParticipantID = participantID;
                        tempTurn.TurnNr = turnNr;
                        tempTurn.Flight = dataGridViewDisplay.Items[e2.RowIndex].Cells[e2.ColumnIndex].Value.ToString();

                        tempRowOfTurns.Clear();
                        tempRowOfTurns.Add(tempTurn);

                        db.addTurn(tempRowOfTurns);
                    }
                }
                TurnsUpdateDisplay();
                SetCosts(participantID, e2.RowIndex);
                /*//..set costs
                //..try to get record in order to know if add or update
                List<DayPilotCost> tempDayPilotCost = new List<DayPilotCost>();
                tempDayPilotCost = db.getDayPilotCostsByTrainIDParticipID(actTrningID, participantID);
                if(tempDayPilotCost.Count == 0)
                {
                    //add
                    DayPilotCost dayPilotCostToUpdate = new DayPilotCost();
                    dayPilotCostToUpdate.TrainingID = actTrningID;
                    dayPilotCostToUpdate.ParticipantID = participantID;
                    try
                    {
                        dayPilotCostToUpdate.CostFlights = decimal.Parse(dataGridViewDisplay.Items[e.RowIndex].Cells[dataGridViewDisplay.Columns.Count - 1].Value.ToString());
                    }
                    catch (Exception exept)
                    {
                        MessageBox.Show("Error parsing CostBuy : '{exept}'");
                            dayPilotCostToUpdate.CostFlights = 99999;
                    }
                    tempDayPilotCost.Clear();
                    tempDayPilotCost.Add(dayPilotCostToUpdate);
                    db.addDayPilotCost(tempDayPilotCost);
                }
                else if(tempDayPilotCost.Count == 1)
                {
                    try
                    {
                        tempDayPilotCost[0].CostFlights = decimal.Parse(dataGridViewDisplay.Items[e.RowIndex].Cells[dataGridViewDisplay.Columns.Count - 1].Value.ToString());
                    }
                    catch (Exception exept)
                    {
                        MessageBox.Show("Error parsing CostBuy : '{exept}'");
                        tempDayPilotCost[0].CostFlights = 99999;
                    }
                    db.updateDayPilotCosts(tempDayPilotCost);
                }*/
            }
        }
    }
}
