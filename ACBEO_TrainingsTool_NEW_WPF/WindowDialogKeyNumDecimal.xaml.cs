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
    public partial class WindowDialogKeyNumDecimal : Window
    {
        private string oldText;
        private bool useDefVal;
        private decimal defDecimal;

        private decimal return_value;
        public decimal return_decimal { get { return return_value; } }

        private bool return_canceled;
        public bool wasCanceled { get { return return_canceled; } }
        public WindowDialogKeyNumDecimal(bool useDefaultValue, decimal defaultDecimal)
        {
            InitializeComponent();
            useDefVal = useDefaultValue;
            defDecimal = defaultDecimal;

            if (useDefVal)
            {
                TextBoxNummeric.Text = defDecimal.ToString();
            }
            TextBoxNummeric.Focus();
            TextBoxNummeric.SelectAll();
        }

        private bool checkIfParsableToDecimal(string stringValue)
        {
            decimal numValue;
            bool parsed = decimal.TryParse(stringValue, out numValue);

            if (stringValue == "" || stringValue == "-")
            {
                parsed = true;
            }
            else
            {
                if (!parsed)
                {
                    MessageBox.Show("Value must be numeric!! Nur Zahlen erlaubt");
                }
            }
            return parsed;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            return_value = 0;
            return_canceled = true;
            Close();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (checkIfParsableToDecimal(TextBoxNummeric.Text))
            {
                oldText = TextBoxNummeric.Text.ToString();
                if (TextBoxNummeric.Text.ToString() == "" || TextBoxNummeric.Text.ToString() == "-")
                {
                    TextBoxNummeric.Text = "0";
                }
                return_value = decimal.Parse(TextBoxNummeric.Text.ToString());
                return_canceled = false;
                Close();
            }
            else
            {
                TextBoxNummeric.Text = oldText;
            }
        }
    }
}
