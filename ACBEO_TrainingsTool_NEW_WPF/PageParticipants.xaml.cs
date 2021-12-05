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

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for PageParticipant.xaml
    /// </summary>
    public partial class PageParticipant : Page
    {
        private Training actTraining;
        private int pilotsColNrToOrderBy = 0; /*0=LastName ASC, 1 = LastName ASC*/

        //Anlegen Piloten und Teilnehmerliste
        List<Pilot> pilots = new List<Pilot>();
        List<Participant> participants = new List<Participant>();
        public PageParticipant(Training actualTraining)
        {
            InitializeComponent();
            actTraining = actualTraining;
        }

        private void UpdateDisplay()
        {
            DataAccess dbDisplay = new DataAccess();
            pilots = dbDisplay.getActivPilots(pilotsColNrToOrderBy);
            participants = dbDisplay.getParticipants(actTraining.TrainingID);

            dataGridViewParticipants.ItemsSource = participants;

            dataGridPilots.ItemsSource = pilots;
            dataGridPilots.Columns[0].Visibility = Visibility.Hidden;  //make first column invisible
            dataGridPilots.Columns[1].Visibility = Visibility.Hidden;  //make first column invisible
            dataGridPilots.Columns[5].Visibility = Visibility.Hidden;  //make first column invisible

            //how dataGridPilots looks like

            for (int i = 0; i < dataGridPilots.Columns.Count; i++)
            {
                if (dataGridPilots.Columns[i].Header.ToString() == "ComplNameAndLicence")
                {
                    dataGridPilots.Columns[i].Visibility = Visibility.Visible;
                    //dataGridPilots.Columns[i].Width = 260;
                }
                else
                {
                    dataGridPilots.Columns[i].Visibility = Visibility.Hidden;
                }
            }
            //dataGridPilots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //how dataGridParticipants looks like

            for (int i = 0; i < dataGridViewParticipants.Columns.Count; i++)
            {
                if (dataGridViewParticipants.Columns[i].Header.ToString() == "FirstName"
                    || dataGridViewParticipants.Columns[i].Header.ToString() == "LastName"
                    || dataGridViewParticipants.Columns[i].Header.ToString() == "LicensNr"
                    )
                {
                    dataGridViewParticipants.Columns[i].Visibility = Visibility.Visible;
                }
                else
                {
                    dataGridViewParticipants.Columns[i].Visibility = Visibility.Hidden;
                }
            }
            //dataGridViewParticipants.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        //*************************************************************************************
        /* private int searchInDataGridView(DataGridView dgView ,int columnNr, string searchString)
         {
             List<string> tempColumnList = new List<string>();

             tempColumnList.Clear();
             for(int i = 0; i < dgView.Rows.Count; i++ )
             {
                 tempColumnList.Add(dgView.Rows[i].ToString());
             }

             tempColumnList.
         }
         */



        private void CheckAndAddToParticipantList(Pilot actualScannedPilot)
        {
            //check if in Pilot list
            bool foundInPilotList = false;

            foreach (Pilot pilot in pilots)
            {
                if (pilot.Pilot_ID == actualScannedPilot.Pilot_ID)
                {
                    foundInPilotList = true;
                }
            }
            //check if allready in Partitipants list
            bool foundInParticipantList = false;
            foreach (Participant participant in participants)
            {
                if (participant.PilotID == actualScannedPilot.Pilot_ID)
                {
                    foundInParticipantList = true;
                }
            }

            //einfüllen
            if (!foundInParticipantList & foundInPilotList)
            {
                List<Participant> participantToAdd = new List<Participant>();
                participantToAdd.Add(new Participant { TrainingID = actTraining.TrainingID, PilotID = actualScannedPilot.Pilot_ID });


                DataAccess dbAddParticipant = new DataAccess();
                dbAddParticipant.addParticipant(participantToAdd);

                UpdateDisplay();
                Console.Beep(1500, 100);
            }
            else
            {
                Console.Beep(500, 50);
            }

        }


        

        private void Page_Participants_Loaded(object sender, RoutedEventArgs e)
        {
            //ToolStripMenuItemSCAN.Enabled = false;

            buttonInsert.IsEnabled = false;

            //DataAccess db = new DataAccess();
            //pilots = db.getPilots(pilotsColNrToOrderBy);

            UpdateDisplay();
            /**
            //Anlegen einer Liste mit allen Videoquellen. (Hier können neben der gewünschten Webcam
            //auch TV-Karten, etc. auftauchen)
            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //Überprüfen, ob mindestens eine Aufnahmequelle vorhanden ist
            if (videosources != null)
            {
                //Die erste Aufnahmequelle an unser Webcam Objekt binden
                //(habt ihr mehrere Quellen, muss nicht immer die erste Quelle die
                //gewünschte Webcam sein!)
                videoSource = new VideoCaptureDevice(videosources[0].MonikerString);

                try
                {
                    //Überprüfen ob die Aufnahmequelle eine Liste mit möglichen Aufnahme-
                    //Auflösungen mitliefert.

                    if (videoSource.VideoCapabilities.Length > 0)
                    {
                        string highestSolution = "0;0";
                        //Das Profil mit der höchsten Auflösung suchen
                        for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                        {
                            if (videoSource.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestSolution.Split(';')[0]))
                            {
                                highestSolution = (videoSource.VideoCapabilities[4].FrameSize.Width.ToString() + ";" + videoSource.VideoCapabilities[4].FrameSize.Height.ToString());
                            }
                        }
                        //Dem Webcam Objekt ermittelte Auflösung übergeben
                        //textBoxResVideo.Text = highestSolution;
                        videoSource.VideoResolution = videoSource.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])];
                    }
                }
                catch { }

                //NewFrame Eventhandler zuweisen anlegen.
                //(Dieser registriert jeden neuen Frame der Webcam)
                videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(videoSource_NewFrame);

                Bitmap emptyBitmap = new Bitmap(500, 500);
                pictureBox_ShowCode.Image = emptyBitmap;

                //timer_QRcheck.Start();
                //videoSource.Start(); //Das Aufnahmegerät aktivieren

            }**/
        }

        private void btnSortFirstname_Click(object sender, RoutedEventArgs e)
        {
            pilotsColNrToOrderBy = 1;
            UpdateDisplay();
        }

        private void btnSortLastname_Click(object sender, RoutedEventArgs e)
        {
            pilotsColNrToOrderBy = 0;
            UpdateDisplay();
        }

        private void dataGridPilots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridPilots.SelectedItems.Count == 1)
            {
                //old: buttonInsert.IsEnabled = dataGridPilots.SelectedRows[0].Index >= 0;
                buttonInsert.IsEnabled = dataGridPilots.SelectedIndex >= 0;
            }
            else
            {
                buttonInsert.IsEnabled = false;
            }
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPilots.SelectedItems.Count == 1)
            {
                CheckAndAddToParticipantList(pilots[dataGridPilots.SelectedIndex]);
            }
        }

        private void dataGridViewParticipants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridViewParticipants.SelectedItems.Count == 1)
            {
                buttonRemove.IsEnabled = dataGridViewParticipants.SelectedIndex > 0 || (dataGridViewParticipants.SelectedIndex == 0 & dataGridViewParticipants.Items.Count > 1);
            }
            else
            {
                buttonRemove.IsEnabled = false;
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            List<Participant> participantsToRemove = new List<Participant>();
            participantsToRemove.Add(participants[dataGridViewParticipants.SelectedIndex]);
            DataAccess dbRem = new DataAccess();

            dbRem.deleteFromAllTblsParticipant(participantsToRemove);
            UpdateDisplay();
        }
    }
}
