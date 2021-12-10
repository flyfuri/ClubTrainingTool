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
using System.Windows.Shapes;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for WindowPayWithAbo.xaml
    /// </summary>
    public partial class WindowPayWithAbo : Window
    {
        private Training actTrning;
        private int pltID;
        private int flghtsToPay;
        private Participant actPartpnt;
        List<AboFlight> validAboFlights = new List<AboFlight>();
        DataAccess dbAboPay = new DataAccess();

        public WindowPayWithAbo(Training actualTraining, Participant actParticipant, int flightsToPay)
        {
            InitializeComponent();

            actTrning = actualTraining;
            pltID = actParticipant.PilotID;
            flghtsToPay = flightsToPay;
            actPartpnt = actParticipant;
        }


        private void Window_PayWithAbo_Loaded(object sender, RoutedEventArgs e)
        {
            buttonPay.IsEnabled = false;

            validAboFlights = dbAboPay.getUsableAboFlightsByPilotID(pltID, MainWindow.actYEAR);

            comboBoxNbFlightsToPay.Items.Clear();
            if (flghtsToPay == 0)
            {
                List<string> comboboxString = new List<string>();
                comboboxString.Add("keine Flüge zu bezahlen");
                comboBoxNbFlightsToPay.ItemsSource = comboboxString;
                ///comboBoxNbFlightsToPay.Update();
            }
            else if (flghtsToPay < 0) //negative flights means pilot gets additinal flights
            {
                List<int> comboboxInt = new List<int>();
                for (int i = -1; i >= flghtsToPay; i--)
                {
                    comboboxInt.Add(i);
                }
                comboBoxNbFlightsToPay.ItemsSource = comboboxInt;
                ///comboBoxNbFlightsToPay.Update();
                comboBoxNbFlightsToPay.SelectedIndex = comboBoxNbFlightsToPay.Items.Count - 1;
                buttonPay.IsEnabled = true;
            }
            else if (validAboFlights.Count <= 0)
            {
                List<string> comboboxString = new List<string>();
                comboboxString.Add("kein Abo vorhanden");
                comboBoxNbFlightsToPay.ItemsSource = comboboxString;
                ///comboBoxNbFlightsToPay.Update();
            }
            else
            {
                List<int> comboboxInt = new List<int>();
                for (int i = 1; i <= flghtsToPay; i++)
                {
                    comboboxInt.Add(i);
                }
                comboBoxNbFlightsToPay.ItemsSource = comboboxInt;
                ///comboBoxNbFlightsToPay.Update();
                comboBoxNbFlightsToPay.SelectedIndex = comboBoxNbFlightsToPay.Items.Count - 1;
                if (comboboxInt[comboBoxNbFlightsToPay.SelectedIndex] > 0)
                {
                    buttonPay.IsEnabled = true;
                }
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonPay_Click(object sender, RoutedEventArgs e)
        {
            int choosenNbFlightToPay;
            validAboFlights = dbAboPay.getUsableAboFlightsByPilotID(pltID, MainWindow.actYEAR);

            if (flghtsToPay > 0)
            {
                choosenNbFlightToPay = comboBoxNbFlightsToPay.SelectedIndex + 1;

                for (int i = 1; i <= choosenNbFlightToPay; i++)
                {
                    if (i <= validAboFlights.Count)
                    {
                        List<AboFlight> aboFlightToPayWith = new List<AboFlight>();
                        aboFlightToPayWith.Clear();
                        aboFlightToPayWith.Add(validAboFlights[i - 1]);
                        aboFlightToPayWith[0].DateFlightPayedWith = actTrning.TrainingDate;
                        dbAboPay.updateAboFlightsDatePayedWith(aboFlightToPayWith);
                    }
                    else
                    {
                        MessageBox.Show($"Es können nur {(i - 1).ToString()} von {choosenNbFlightToPay.ToString()} Flüge bezahlt werden!");
                        break;
                    }
                }
            }
            else //get payback-flights on abo 0
            {
                choosenNbFlightToPay = (comboBoxNbFlightsToPay.SelectedIndex * (-1)) - 1;

                int nbrFlightsAbo0 = 0;
                foreach (AboFlight validAboFlight in validAboFlights)
                {
                    if (validAboFlight.Abo_NrInYear == 0)
                    {
                        nbrFlightsAbo0++;
                    }
                }


                int sellingPilotID = 0;
                List<Participant> participants = new List<Participant>();
                participants = dbAboPay.getParticipants(actTrning.TrainingID);
                foreach (Participant participant in participants)
                {
                    if (participant.ParticipantID == actTrning.Leiter1_ID)
                    {
                        sellingPilotID = participant.PilotID;
                    }
                }
                for (int i = -1; i >= choosenNbFlightToPay; i--)
                {
                    //if ((i*(-1)) <= validAboFlights.Count)
                    //{
                    List<AboFlight> newAboFlights = new List<AboFlight>();
                    AboFlight newAboFlight = new AboFlight();
                    newAboFlight.PilotID = pltID;
                    newAboFlight.Abo_Year = DateTime.Today.Year;
                    nbrFlightsAbo0++;
                    newAboFlight.FlightNrOnAbo = nbrFlightsAbo0;
                    newAboFlight.Abo_NrInYear = 0;
                    newAboFlight.DateBought = actTrning.TrainingDate;
                    newAboFlight.DateFlightPayedWith = DateTime.Parse("1753-01-01");
                    newAboFlight.Comment = "Flug aus überzähligen Diensten";
                    newAboFlight.SellerID = sellingPilotID;
                    newAboFlights.Clear();
                    newAboFlights.Add(newAboFlight);
                    dbAboPay.addAboFlights(newAboFlights);
                    //}
                }
            }

            string codeToShow = $"{actPartpnt.ComplNameAndLicence}__{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}";
            validAboFlights = dbAboPay.getUsableAboFlightsByPilotID(actPartpnt.PilotID, MainWindow.actYEAR);
            foreach (AboFlight validAboFlight in validAboFlights)
            {
                if (!codeToShow.Contains(validAboFlight.AboYearNr))
                {
                    codeToShow = $"{codeToShow}__{validAboFlight.AboYearNr}";
                }
            }
            codeToShow = $"{codeToShow}__{validAboFlights.Count.ToString()}";
            WindowShowQRCode formShowQrCode = new WindowShowQRCode(codeToShow);
            formShowQrCode.ShowDialog();
            Close();
        }
    }
}
