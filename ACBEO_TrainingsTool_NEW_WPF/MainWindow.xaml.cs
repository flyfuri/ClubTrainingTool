﻿using System;
using System.IO;
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
using CsvHelper;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool trainingOpen;
        public static int actYEAR;
        public Training actualTraining { get; private set; } //actual training 
        public MainWindow()
        {
            if (Environment.MachineName.ToString().Contains("Name of Target Tablet"))
            {
                WindowState = WindowState.Maximized;
            }
            DataAccess db = new DataAccess();
            List<Training> trainings = new List<Training>();

            InitializeComponent();

            actYEAR = DateTime.Today.Year;

            do
            {
                trainings = db.getTrainingByYEAR(actYEAR);
                if (trainings.Count <= 0)
                {
                    actYEAR = actYEAR - 1;
                }

            }
            while (trainings.Count <= 0 & actYEAR > 2016);  //first year of use of this software was 2017...
            if(actYEAR <= 2016)
            {
                actYEAR = DateTime.Today.Year;
                trainingOpen = false;
            }
            else
            {
                trainings.Sort((x, y) => y.TrainingDate.CompareTo(x.TrainingDate));
                if(trainings.Count > 0)
                {
                    actualTraining = trainings[0];
                    textBoxActualTrainingDate.Text = actualTraining.TrainingDate.Date.ToShortDateString();
                    trainingOpen = true;
                    MainFrame.Content = new PageTrainingg(actualTraining);
                    updateDisplayActTrainingAndLeiter1and2();
                }
            }

            updateWindowTitle();

            MainFrame.Content = new PageTrainingg(actualTraining);
        }


        public void updateWindowTitle() 
        {
            this.Title = actYEAR.ToString();
        }

        public void updateDisplayActTrainingAndLeiter1and2()
        {
            List<Participant> participants = new List<Participant>();
            DataAccess dbParticpants = new DataAccess();
            participants = dbParticpants.getParticipants(actualTraining.TrainingID);

            foreach (Participant participant in participants)
            {
                if (participant.ParticipantID == actualTraining.Leiter1_ID)
                {
                    textBoxLeiter1.Text = participant.ComplNameAndLicence;
                }
                else if (participant.ParticipantID == actualTraining.Leiter2_ID)
                {
                    textBoxLeiter2.Text = participant.ComplNameAndLicence;
                }
            }
        }

        private void backupDatabase(bool messagewhenOK)
        {
            string backuppath = "C:\\TESTDATA_TrainingsTool\\Test_TrainingsDB\\Bckups\\";
            DataAccess db = new DataAccess();
            bool backupOK = db.backupdb("TrainingsRapport", backuppath);
            if (!backupOK)
            {
                MessageBox.Show($"Backup not successfull! Folder \"{backuppath}\" existing?");
            }
            else if(messagewhenOK)
            {
                MessageBox.Show($"new Backup to be found in folder \"{backuppath}\".");
            }
        }


        /**********Main Menue Events**********************************************************/
        private void MenueTraining_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)  //routed event (tunneling mode) see https://docs.microsoft.com/en-us/archive/msdn-magazine/2008/september/advanced-wpf-understanding-routed-events-and-commands-in-wpf
        {   
            if (((MainFrame.Content as Page).Name.Equals("Page_Training") == false) & (e.Source as ItemsControl).Name.Equals(MenueTraining.Name))
            {
                foreach (ItemsControl m in MenueMain.Items)
                    m.IsEnabled = true;
                MainFrame.Content = new PageTrainingg(actualTraining);
            }
        }
        
        private void MenueTraining_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items) 
            m.IsEnabled = true;
            MainFrame.Content = new PageTrainingg(actualTraining);
        }
        private void MenueScan_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items)
            {
                if (m.Equals(e.Source as ItemsControl) == false)
                   m.IsEnabled = true;
                else
                   m.IsEnabled = false;
            }
            MainFrame.Content = new PageParticipant (actualTraining);
        }

        private void MenueTurns_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items)
            {
                if (m.Equals(e.Source as ItemsControl) == false)
                    m.IsEnabled = true;
                else
                    m.IsEnabled = false;
            }
            MainFrame.Content = new PageTurns(actualTraining, actYEAR);
        }

        private void MenueBuy_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items)
            {
                if (m.Equals(e.Source as ItemsControl) == false)
                    m.IsEnabled = true;
                else
                    m.IsEnabled = false;
            }
            MainFrame.Content = new PageBuy(actualTraining);
        }

        private void MenuePay_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items)
            {
                if (m.Equals(e.Source as ItemsControl) == false)
                    m.IsEnabled = true;
                else
                    m.IsEnabled = false;
            }
            MainFrame.Content = new PageDayPilotCosts(actualTraining);
        }

        private void MenuePilots_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items)
            {
                if (m.Equals(e.Source as ItemsControl) == false)
                    m.IsEnabled = true;
                else
                    m.IsEnabled = false;
            }
            MainFrame.Content = new PagePilots();
        }

        /**********Menue Events Training Menues**********************************************************/
        private void MenuItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowDialogKeyNumDecimal formBuyKeyNumInt = new WindowDialogKeyNumDecimal(true, actYEAR);
            formBuyKeyNumInt.Title = "Enter year to work in";
            formBuyKeyNumInt.Owner = App.Current.MainWindow;
            formBuyKeyNumInt.ShowDialog();
            if (formBuyKeyNumInt.wasCanceled == false)
            {
                decimal formAnswer = Decimal.ToInt32(formBuyKeyNumInt.return_decimal);
                if (formAnswer < 2017)
                {
                    actYEAR = 2017;
                }
                else if (formAnswer > DateTime.Today.Year)
                {
                    actYEAR = DateTime.Today.Year;
                }
                else
                {
                    actYEAR = Decimal.ToInt32(formAnswer);
                }
                updateWindowTitle();
            }
        }

        private void menuItemQuickOpen_MouseEnter(object sender, MouseEventArgs e)
        {
            DataAccess db = new DataAccess();
            List<Training> trainings = new List<Training>();

            trainings = db.getTrainingByYEAR(actYEAR);
            //trainings.Sort((x, y) => x.TrainingDate.CompareTo(y.TrainingDate));
            trainings.Sort((x, y) => y.TrainingDate.CompareTo(x.TrainingDate));

            menuItemQuickOpen.Items.Clear();

            foreach (Training training in trainings)
            {
                MenuItem subitem = new MenuItem();
                subitem.Header = training.TrainingDate.ToShortDateString();

                // Hook up the event handler (in this case the method File_Language_Click handles all these menu items)
                //subitem.Click += new RoutedEventHandler(menuItemQuickOpen_Click);  //from an example (found not needed)

                // Add menu item as child to pre-defined menu item
                menuItemQuickOpen.Items.Add(subitem); // Add menu item as child to pre-defined menu item
            }

            /**toolStripMenuItemQuickOpen.DropDown = ListToOpen;
            Font drpdwnFont = new Font("Segoe UI", 10, FontStyle.Bold);
            toolStripMenuItemQuickOpen.DropDown.Font = drpdwnFont;**/

        }

        private void menuItemQuickOpen_Click(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess();
            List<Training> trainings = new List<Training>();
            trainings = db.getTrainingByYEAR(actYEAR);
            trainings.Sort((x, y) => x.TrainingDate.CompareTo(y.TrainingDate));

            string datestr;
            datestr = (e.OriginalSource as MenuItem).Header.ToString();

            foreach (Training training in trainings)
            {
                if (datestr == training.TrainingDate.ToShortDateString())
                {
                    actualTraining = training;
                    textBoxActualTrainingDate.Text = actualTraining.TrainingDate.Date.ToShortDateString();
                    trainingOpen = true;
                    MainFrame.Content = new PageTrainingg(actualTraining);
                    updateDisplayActTrainingAndLeiter1and2();
                }
                else
                {
                }
            }
        }

        private void menuItemOPEN_Click(object sender, RoutedEventArgs e)
        {
            WindowOpenTraining formOpenTraining = new WindowOpenTraining(actYEAR);
            formOpenTraining.Owner = App.Current.MainWindow;
            formOpenTraining.ShowDialog();

            if (!formOpenTraining.wasCanceled)
            {
                DataAccess db = new DataAccess();
                actualTraining = db.getTrainingByID(formOpenTraining.return_TrainingsID)[0];
                textBoxActualTrainingDate.Text = actualTraining.TrainingDate.Date.ToShortDateString();
                trainingOpen = true;
                MainFrame.Content = new PageTrainingg(actualTraining);
                updateDisplayActTrainingAndLeiter1and2();
            }
        }

        private void menuItemOPENnext_Click(object sender, RoutedEventArgs e)
        {
            int temp_index;

            DataAccess db = new DataAccess();
            List<Training> trainings = new List<Training>();

            trainings = db.getTrainingByYEAR(actYEAR);
            trainings.Sort((x, y) => x.TrainingDate.CompareTo(y.TrainingDate));

            if (trainings.Count > 0)
            {
                temp_index = 0;
                foreach (Training training in trainings)
                {
                    if (training.TrainingID == actualTraining.TrainingID)
                    {
                        break;
                    }
                    else
                    {
                        temp_index++;
                    }
                }

                temp_index++;

                if (temp_index > trainings.Count - 1)
                {
                    temp_index = temp_index - trainings.Count;
                }

                actualTraining = trainings[temp_index];
                textBoxActualTrainingDate.Text = actualTraining.TrainingDate.Date.ToShortDateString();
                trainingOpen = true;
                MainFrame.Content = new PageTrainingg(actualTraining);
                updateDisplayActTrainingAndLeiter1and2();
            }
        }

        private void menuItemOPENprevious_Click(object sender, RoutedEventArgs e)
        {
            int temp_index;

            DataAccess db = new DataAccess();
            List<Training> trainings = new List<Training>();

            trainings = db.getTrainingByYEAR(actYEAR);
            trainings.Sort((x, y) => x.TrainingDate.CompareTo(y.TrainingDate));

            if (trainings.Count > 0)
            {
                temp_index = 0;
                foreach (Training training in trainings)
                {
                    if (training.TrainingID == actualTraining.TrainingID)
                    {
                        break;
                    }
                    else
                    {
                        temp_index++;
                    }
                }

                temp_index--;

                if (temp_index < 0)
                {
                    temp_index = trainings.Count - 1; // + temp_index;
                }

                actualTraining = trainings[temp_index];
                textBoxActualTrainingDate.Text = actualTraining.TrainingDate.Date.ToShortDateString();
                trainingOpen = true;
                MainFrame.Content = new PageTrainingg(actualTraining);
                updateDisplayActTrainingAndLeiter1and2();
            }
        }

        private void menuItemCLOSE_Click(object sender, RoutedEventArgs e)
        {
            textBoxActualTrainingDate.Text = "kein Training geöffnet";
            textBoxLeiter1.Text = "";
            textBoxLeiter2.Text = "";
            trainingOpen = false;
            actualTraining = null;
            MainFrame.Content = new PageTrainingg(actualTraining);
        }

        private void menuItemNEW_Click(object sender, RoutedEventArgs e)
        {
            WindowNewTraining windowNewTraining = new WindowNewTraining();
            windowNewTraining.Owner = App.Current.MainWindow;
            windowNewTraining.ShowDialog();

            if (!windowNewTraining.wasCanceled)
            {
                actualTraining = windowNewTraining.lastNewTrainigWithID;
                DataAccess db = new DataAccess();
                actualTraining = db.getTrainingByID(windowNewTraining.lastNewTrainigWithID.TrainingID)[0];
                textBoxActualTrainingDate.Text = actualTraining.TrainingDate.ToShortDateString();
                trainingOpen = true;
                MainFrame.Content = new PageTrainingg(actualTraining);
                updateDisplayActTrainingAndLeiter1and2();
            }
        }

        private void menuItemExportAbos_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgResult = new MessageBoxResult();
            msgResult = MessageBox.Show("Export a List of all unused AboFlights? (C:\\TESTDATA_TrainingsTool\\AboBackups)", "", MessageBoxButton.OKCancel);
            if (msgResult == MessageBoxResult.OK)
            {
                string month = actualTraining.TrainingDate.Month.ToString();
                if (month.Length <= 1)
                {
                    month = $"0{month}";
                }
                string day = actualTraining.TrainingDate.Day.ToString();
                if (day.Length <= 1)
                {
                    day = $"0{day}";
                }
                string basicPath = "C:\\TESTDATA_TrainingsTool\\AboBackups";
                string completePath = $"{basicPath}\\Abos_Stand_{actualTraining.TrainingDate.Year}{month}{day}";
                string completePathWithName = "";

                DirectoryInfo directoryInfo_Training = new DirectoryInfo(completePath);
                if (!directoryInfo_Training.Exists)
                {
                    directoryInfo_Training.Create();
                }

                completePath = $"{completePath}";  // \\backup";
                DirectoryInfo directoryInfo_Bkup = new DirectoryInfo(completePath);
                if (!directoryInfo_Bkup.Exists)
                {
                    directoryInfo_Bkup.Create();
                }

                DataAccess db1 = new DataAccess();

                List<UnusedAbosToExport> allValidAboFlights = new List<UnusedAbosToExport>();

                allValidAboFlights = db1.getAllUnusedAboFlightsCurrent2Years(actualTraining.TrainingDate.Year);

                completePathWithName = $"{completePath}\\AboBackup{actualTraining.TrainingDate.Year}{month}{day}.csv";
                using (var sw = new StreamWriter(completePathWithName))
                {
                    //var reader = new CsvReader(sr);
                    
                    var writer = new CsvWriter(sw, System.Globalization.CultureInfo.InvariantCulture, false);

                    //read the whole db into an enumerable
                    System.Collections.IEnumerable records = allValidAboFlights.ToList();

                    // foreach (TrainingCost trainingCost in trainingCosts)
                    /*records.
                    foreach (IEnumerable rec in records)
                    {
                        if rec.
                    }*/

                    try //Write the entire contents of the CSV file into another
                    {
                        writer.WriteRecords(records);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error writing Pilots.csv, {ex}");
                    }
                }

            }
        }

        private void textBoxLeiter1_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MainFrame.Content.GetType().Name == "PageTurns")
            {
                string leiter1Text = (MainFrame.Content as PageTurns).setLeiter1or2(1);
                if (leiter1Text != null)
                {
                    textBoxLeiter1.Text = leiter1Text;
                }
            }
        }

        private void textBoxLeiter2_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MainFrame.Content.GetType().Name == "PageTurns")
            {
                string leiter2Text = (MainFrame.Content as PageTurns).setLeiter1or2(2);
                if (leiter2Text != null)
                {
                    textBoxLeiter2.Text = leiter2Text;
                }
            }
        }

        private void menuItemOverView_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items)
                m.IsEnabled = true;
            MainFrame.Content = new PageTrainingOverview();
        }

        private void BackupDB_Click(object sender, RoutedEventArgs e)
        {
            backupDatabase(true);
        }
    }
}

