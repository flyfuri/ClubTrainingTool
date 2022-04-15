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
    /// Interaction logic for WindowFinalizeTraining.xaml
    /// </summary>
    public partial class WindowFinalizeTraining : Window
    {
        private bool _already_finalized;
        string return_value;
        public string return_string { get { return return_value; } }
        public WindowFinalizeTraining(bool already_finalized)
        {
            _already_finalized = already_finalized;
            InitializeComponent();

            if (_already_finalized)
            {
                buttonFinalize.Content = "UNFINALIZE";
                passwordbox_key.IsEnabled = true;
            }
            else
            {
                buttonFinalize.Content = "FINALIZE";
                passwordbox_key.IsEnabled = false;
            }
        }

        private void buttonFinalize_Click(object sender, RoutedEventArgs e)
        {
            if (_already_finalized)
            {
                if(passwordbox_key.Password == "GlobiKlub")
                {
                    return_value = "UNFINALIZE";
                }
                else
                {
                    return_value = "CANCEL";
                }
            }
            else
            {
                return_value = "FINALIZE";
            }
            Close();
        }

        private void buttonCANCEL_Click(object sender, RoutedEventArgs e)
        {
            return_value = "CANCEL";
            Close();
        }
    }
}
