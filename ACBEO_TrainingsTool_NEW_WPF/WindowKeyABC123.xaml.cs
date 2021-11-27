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
    /// Interaction logic for WindowKeyABC123.xaml
    /// </summary>
    public partial class WindowKeyABC123 : Window
    {
        bool useDefVal;
        string defInt;

        private bool shift_ative;
        private bool caps_locked;

        string return_value;
        public string return_string { get { return return_value; } }

        private bool return_canceled;
        public bool wasCanceled { get { return return_canceled; } }

        public WindowKeyABC123(bool useDefaultValue, string defaultString)
        {
            InitializeComponent();
            useDefVal = useDefaultValue;
            defInt = defaultString;

            if (useDefVal)
            {
                TextBoxABC.Text = defInt;
            }
            TextBoxABC.Focus();
            TextBoxABC.SelectAll();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            return_value = "";
            return_canceled = true;
            Close();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            return_value = TextBoxABC.Text;
            return_canceled = false;
            Close();
        }
    }
}
