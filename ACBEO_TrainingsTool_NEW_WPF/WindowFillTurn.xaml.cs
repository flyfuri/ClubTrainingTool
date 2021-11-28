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
    /// Interaction logic for WindowFillTurn.xaml
    /// </summary>
    public partial class WindowFillTurn : Window
    {
        string return_value;
        public string return_string { get { return return_value; } }
        public WindowFillTurn()
        {
            InitializeComponent();
        }

        private void buttonNone_Click(object sender, RoutedEventArgs e)
        {
            return_value = "";
            Close();
        }

        private void buttonFlight_Click(object sender, RoutedEventArgs e)
        {
            return_value = "FLIGHT";
            Close();
        }

        private void buttonBoat_Click(object sender, RoutedEventArgs e)
        {
            return_value = "BOAT";
            Close();
        }

        private void buttonBus_Click(object sender, RoutedEventArgs e)
        {
            return_value = "BUS";
            Close();
        }

        private void buttonBandB_Click(object sender, RoutedEventArgs e)
        {
            return_value = "BnB:";
            Close();
        }

        private void buttonFlightnBus_Click(object sender, RoutedEventArgs e)
        {
            return_value = "FnBus:" + textBoxBusdriver.Text.ToString();
            Close();
        }

        private void buttonFlightnBoat_Click(object sender, RoutedEventArgs e)
        {
            return_value = "FnBoat:" + textBoxBoatdriver.Text.ToString();
            Close();
        }

        private void buttonFlightAndBusAndBoat_Click(object sender, RoutedEventArgs e)
        {
            return_value = "FnBnB:" + textBoxBusdriver.Text.ToString() + ";" + textBoxBoatdriver.Text.ToString();
            Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            return_value = "CANCEL";
            Close();
        }
    }
}
