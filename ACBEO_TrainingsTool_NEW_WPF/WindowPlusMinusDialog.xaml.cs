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
    /// Interaction logic for WindowPlusMinusDialog.xaml
    /// </summary>
    public partial class WindowPlusMinusDialog : Window
    {
        string return_value;
        public string return_string { get { return return_value; } }
        public WindowPlusMinusDialog()
        {
            InitializeComponent();
        }

        

        private void buttonMinus_Click(object sender, RoutedEventArgs e)
        {
            return_value = "Minus";
            Close();
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            return_value = "Plus";
            Close();
        }

        private void buttonCANCEL_Click(object sender, RoutedEventArgs e)
        {
            return_value = "Canceled";
            Close();
        }
    }
}
