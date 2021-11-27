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
    /// Interaction logic for PagePilots.xaml
    /// </summary>
    public partial class PagePilots : Page
    {
        private int pilotsColNrToOrderBy = 0; /*0=LastName ASC, 1 = LastName ASC*/

        //Anlegen Pilotenliste
        List<Pilot> pilots = new List<Pilot>();
        private int actualChosenPilotID;

        public PagePilots()
        {
            InitializeComponent();
        }

        private void Page_Pilots_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridPilotsUpdateDisplay();
        }
        private void PilotsUpdateBinding()
        {
            dataGridViewPilots.ItemsSource = pilots;

            //dataGridViewPilots.ReadOnly = true;
            /*for (int ii = 1; ii < dataGridViewPilots.Columns.Count; ii++)
            {
                dataGridViewPilots.Columns[ii].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }*/
        }

        private void DataGridPilotsUpdateDisplay()
        {
            //read active Pilots for display
            DataAccess db = new DataAccess();
            pilots = db.getActivPilots(pilotsColNrToOrderBy);
            PilotsUpdateBinding();
            actualChosenPilotID = -1;


            //how data grid looks like

            /*DataGridViewColumn newColumn = new DataGridViewColumn();
            newColumn = dataGridViewPilots.Columns["LicensNr"];
            newColumn.Name = "Licens";
            try
            {
                dataGridViewPilots.Columns.Add(newColumn);
            }
            catch
            {

            }*/

            /*for (int i = 0; i < dataGridViewPilots.Columns.Count; i++)
            {
                if (dataGridViewPilots.Columns[i].Header.Equals("FirstName")
                    || dataGridViewPilots.Columns[i].Header.Equals("LastName")
                    || dataGridViewPilots.Columns[i].Header == "Licens"
                    || dataGridViewPilots.Columns[i].Header = "ComplNameAndLicence")
                {
                    dataGridViewPilots.Columns[i].Visibility = true;
                }
                else
                {
                    dataGridViewPilots.Columns[i].Visibility = false;
                }
            }*/
        }

        private void dataGridViewPilots_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGridCellClickRowColumnFormStyle e2 = new DataGridCellClickRowColumnFormStyle();  //provide RowIndex and ColumnIndex from e in forms style
            e2.wpf_e = e;
            
            if (e2.RowIndex >= 0 & e2.RowIndex < pilots.Count & e2.isCell)
            {
                actualChosenPilotID = pilots[e2.RowIndex].Pilot_ID;
                textBoxPilotLicenseNr.Text = pilots[e2.RowIndex].LicensNr.ToString();
                textBoxFirstName.Text = pilots[e2.RowIndex].FirstName;
                textBoxLastName.Text = pilots[e2.RowIndex].LastName;
            }
        }

        private void btn_SortFirstName_Click(object sender, RoutedEventArgs e)
        {
            pilotsColNrToOrderBy = 1;
            DataGridPilotsUpdateDisplay();
        }

        private void btn_SortLastName_Click(object sender, RoutedEventArgs e)
        {
            pilotsColNrToOrderBy = 0;
            DataGridPilotsUpdateDisplay();
        }

        private void btn_EMPTY_Click(object sender, RoutedEventArgs e)
        {
            textBoxPilotLicenseNr.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
        }

        private void btn_NEW_Click(object sender, RoutedEventArgs e)
        {
            List<Pilot> pilotsInclInactiv = new List<Pilot>();
            DataAccess dbRallPlts = new DataAccess();

            pilotsInclInactiv = dbRallPlts.getPilots(0);


            List<Pilot> pilotToAdd = new List<Pilot>();
            List<Pilot> pilotToUpdate = new List<Pilot>();

            List<Pilot> pilotsSameName = new List<Pilot>();
            foreach (Pilot plt in pilotsInclInactiv)
            {
                if (plt.FirstName == textBoxFirstName.Text
                    & plt.LastName == textBoxLastName.Text)
                {
                    pilotsSameName.Add(plt);
                }
            }
            if (pilotsSameName.Count > 0)
            {
                MessageBoxResult msgBoxResult = MessageBox.Show($"pilot with same name allready exist.   JA = take this one: {pilotsSameName[0].ComplNameAndLicence}   NEIN = take this one: {textBoxFirstName.Text} {textBoxLastName.Text}({textBoxPilotLicenseNr.Text})",
                                                            "",
                                                            MessageBoxButton.YesNoCancel,
                                                            MessageBoxImage.Question);
                if (msgBoxResult == MessageBoxResult.Yes)
                {
                    pilotToUpdate.Add(pilotsSameName[0]);
                }
                else if (msgBoxResult == MessageBoxResult.No)
                {
                    pilotsSameName[0].LicensNr = textBoxPilotLicenseNr.Text;
                    pilotToUpdate.Add(pilotsSameName[0]);
                }
                else if (msgBoxResult == MessageBoxResult.Cancel)
                {
                    pilotToUpdate.Clear();
                    pilotToAdd.Clear();
                }
            }
            else
            {
                pilotToAdd.Add(new Pilot { LicensNr = textBoxPilotLicenseNr.Text, FirstName = textBoxFirstName.Text, LastName = textBoxLastName.Text });
            }
            if (pilotToUpdate.Count > 0)
            {
                DataAccess dbToWrite = new DataAccess();
                pilotToUpdate[0].Disactivated = false;
                dbToWrite.updatePilot(pilotToUpdate);
            }
            else if (pilotToAdd.Count > 0)
            {
                DataAccess dbToWrite = new DataAccess();
                dbToWrite.addPilot(pilotToAdd);
            }
            DataGridPilotsUpdateDisplay();
        }

        private void btn_CHANGE_Click(object sender, RoutedEventArgs e)
        {
            List<Pilot> pilotsInclInactiv = new List<Pilot>();
            DataAccess dbRallPlts = new DataAccess();

            bool pltAllreadyExists = false;
            pilotsInclInactiv = dbRallPlts.getPilots(0);
            if (pilotsInclInactiv.Count > 0)
            {
                foreach (Pilot plt in pilotsInclInactiv)
                {
                    if (plt.FirstName == textBoxFirstName.Text
                        & plt.LastName == textBoxLastName.Text)
                    //& plt.LicensNr == textBoxPilotLicenseNr.Text)
                    {
                        pltAllreadyExists = true;
                    }
                }
            }

            if (actualChosenPilotID < 0)
            {
                MessageBox.Show("Error: no pilot chosen before!");
            }
            else if (!(textBoxPilotLicenseNr.Text != "" & textBoxFirstName.Text != "" & textBoxLastName.Text != ""))
            {
                MessageBox.Show("Error: not all fields completed!");
            }
            else if (pltAllreadyExists)
            {
                MessageBox.Show("Pilot with same Name and allready exists! Maybe disactivated and hidden...", "", MessageBoxButton.OK);
            }
            else
            {
                List<Pilot> pilotToUpdate = new List<Pilot>();
                pilotToUpdate.Add(new Pilot { Pilot_ID = actualChosenPilotID, LicensNr = textBoxPilotLicenseNr.Text, FirstName = textBoxFirstName.Text, LastName = textBoxLastName.Text });

                DataAccess dbToWrite = new DataAccess();
                dbToWrite.updatePilot(pilotToUpdate);

                DataGridPilotsUpdateDisplay();
            }
        }

        private void btn_DELETE_Click(object sender, RoutedEventArgs e)
        {
            if (actualChosenPilotID < 0)
            {
                MessageBox.Show("Error: no pilot chosen before!");
            }
            else
            {
                /*List<Pilot> pilotToDelete = new List<Pilot>();
                pilotToDelete.Add(new Pilot { Pilot_ID = actualChosenPilotID });

                DataAccess dbToWrite = new DataAccess();
                dbToWrite.deletePilot(pilotToDelete);*/
                List<Pilot> pilotsToDisactivate = new List<Pilot>();

                DataAccess dbToWrite = new DataAccess();

                pilotsToDisactivate = dbToWrite.getPilotByID(actualChosenPilotID);

                MessageBoxResult msgBoxResult = MessageBox.Show($"Do you really want to disactivate Pilot \"{pilotsToDisactivate[0].ComplNameAndLicence}\" ?", "delete", MessageBoxButton.OKCancel);
                if (msgBoxResult == MessageBoxResult.OK)
                {
                    if (pilotsToDisactivate.Count > 0)
                    {
                        pilotsToDisactivate[0].Disactivated = true;
                        dbToWrite.updatePilot(pilotsToDisactivate);
                    }
                    DataGridPilotsUpdateDisplay();
                }
            }
        }
    }
}
