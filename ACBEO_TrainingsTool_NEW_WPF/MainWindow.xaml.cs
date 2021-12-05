using System;
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
        public Training actualTraining { get; set; } //actual training 
        public MainWindow()
        {
            InitializeComponent();

            actYEAR = DateTime.Today.Year;
            updateWindowTitle();
            trainingOpen = false;

            MainFrame.Content = new PageTrainingg(actualTraining);
        }


        public void updateWindowTitle() 
        {
            this.Title = actYEAR.ToString();
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
            MainFrame.Content = new PageBuy();
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
            MainFrame.Content = new PageDayPilotCosts();
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
            trainings.Sort((x, y) => x.TrainingDate.CompareTo(y.TrainingDate));

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
            }
        }

        private void menuItemCLOSE_Click(object sender, RoutedEventArgs e)
        {
            textBoxActualTrainingDate.Text = "kein Training geöffnet";
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
            }
        }

        private void menuItemExportAbos_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgResult = new MessageBoxResult();
            msgResult = MessageBox.Show("Export a List of all unused AboFlights? (C:\\ACBEO_TrainingsDaten)", "", MessageBoxButton.OKCancel);
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
                string basicPath = "C:\\ACBEO_TrainingsDaten"; //"D:\\ACBEO\\TrainingBelegeTool";
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
            WindowSetLeiter formSetLeiter = new WindowSetLeiter(participants, actTraining.Leiter1_ID);
            formSetLeiter.ShowDialog();
            if (!formSetLeiter.wasCanceled)
            {
                DataAccess db = new DataAccess();
                List<Training> TrainingsToUpdate = new List<Training>();
                TrainingsToUpdate.Clear();
                actTraining.Leiter1_ID = formSetLeiter.return_participant.ParticipantID;
                TrainingsToUpdate.Add(actTraining);
                db.updateTraining(TrainingsToUpdate);
                TurnsUpdateDisplay();
                int i = 0;
                foreach (Participant participant in participants)
                {
                    SetCosts(participant.ParticipantID, i);
                    i++;
                }
            }
        }
    }
}

