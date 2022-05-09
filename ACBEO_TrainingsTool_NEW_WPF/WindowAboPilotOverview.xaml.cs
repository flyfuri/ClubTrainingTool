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
using System.Windows.Shapes;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for WindowAboPilotOverview.xaml
    /// </summary>
    public partial class WindowAboPilotOverview : Window
    {
        private int pltID;
        private string pltName;
        private int actYear;
        public WindowAboPilotOverview(int pilotID, string pilotNameToShow, int actualYear)
        {
            InitializeComponent();
            pltID = pilotID;
            pltName = pilotNameToShow;
            actYear = actualYear;
        }

        DataTable display = new DataTable();
        List<AboFlight> aboFlights = new List<AboFlight>();

        private void FormAboPilotOverview_Loaded(object sender, RoutedEventArgs e)
        {
            labelPilotName.Content = pltName.ToString();
            //add columns
            if (display.Columns.Count == 0)
            {
                display.Columns.Add();
                display.Columns[0].ColumnName = "Abo";
                display.Columns.Add();
                display.Columns[1].ColumnName = "gekauft am";
                display.Columns.Add();
                display.Columns[2].ColumnName = "bezahlt mit am";
            }

            DataAccess db = new DataAccess();
            aboFlights = db.getAllAboFlightsByPilotIDinValidPeriod(pltID, actYear);
            DateTime dateUnusedAbos = new DateTime(2000, 1, 1, 0, 0, 0);

            if (aboFlights.Count > 0)
            {
                foreach (AboFlight aboFlight in aboFlights)
                {
                    display.Rows.Add();
                    display.Rows[display.Rows.Count - 1][0] = aboFlight.AboYearNr;
                    display.Rows[display.Rows.Count - 1][1] = aboFlight.DateBought.ToShortDateString();

                    if (aboFlight.DateFlightPayedWith.CompareTo(dateUnusedAbos) > 0)
                    {
                        display.Rows[display.Rows.Count - 1][2] = aboFlight.DateFlightPayedWith.ToShortDateString();
                    }
                    else
                    {
                        display.Rows[display.Rows.Count - 1][2] = "noch gültig";
                    }
                }
                dataGridViewOverview.ItemsSource = display.DefaultView;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonShowQR_Click(object sender, RoutedEventArgs e)
        {
            string codeToShow = $"{pltName}__{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}";
            WindowShowQRCode formShowQrCode = new WindowShowQRCode(codeToShow);
            formShowQrCode.ShowDialog();
        }
    }
}
