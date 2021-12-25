using Codecrete.SwissQRBill.Generator;
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
    /// Interaction logic for WindowCreateShowSwissQR.xaml
    /// </summary>
    public partial class WindowCreateShowSwissQR : Window
    {
        private decimal _billAmount;
        private string _billMessage;
        private bool _includeTwint;
        private string _twintTitle = "twint";
        private string _twintLink_part1 = "twint/light/02:...#";
        private string _twintLink_part2 = "rn/twint/a...A/rn";

        public WindowCreateShowSwissQR(decimal billAmount, string billMessage, bool includeTwint)
        {
            InitializeComponent();
            _billAmount = billAmount;
            _billMessage = billMessage;
            _includeTwint = includeTwint;
            refreshDisplay();
        }

        private void refreshDisplay()
        { 
            AlternativeScheme twintScheme = new AlternativeScheme();
            twintScheme.Name = _twintTitle;
            twintScheme.Instruction = _twintLink_part1;  //first part twint link
            List<AlternativeScheme> altSchemes = new List<AlternativeScheme>();
            altSchemes.Clear();
            string bmessage;
            if (_includeTwint)
            {
                altSchemes.Add(twintScheme);
                if (_billMessage.Length > 138 - _twintLink_part2.Length)
                {
                    bmessage = $"{_billMessage.Substring(0, 138 - _twintLink_part2.Length)}  {_twintLink_part2}";
                }
                else
                {
                    bmessage = $"{_billMessage}  {_twintLink_part2}";
                }
            }
            else 
            {
                if (_billMessage.Length > 140)
                {
                    bmessage = _billMessage.Substring(0, 140);
                }
                else
                {
                    bmessage = _billMessage;
                }
            }
            // create bill data
            try
            {
                Bill bill = new Bill
                {
                    // creditor data
                    Account = "CH0799999999999999999",
                    Creditor = new Address
                    {
                        Name = "Some Club",
                        AddressLine1 = "",
                        AddressLine2 = "3000 Bern",
                        CountryCode = "CH"
                    },

                    // payment data
                    Amount = _billAmount,
                    Currency = "CHF",

                    // debtor data  (only working when all lines used)
                    /*Debtor = new Address
                    {
                        Name = pilotName,
                        AddressLine1 = "0",
                        AddressLine2 = "0",
                        CountryCode = "0"
                    },*/

                    // more payment data
                    ReferenceType = Bill.ReferenceTypeNoRef,  
                    Reference = null,  //needs to be Null, otherwise the dll sets Reference Type to automatically back to ReferenceTypeQrRef and the IBAN will not be accepted!
                    UnstructuredMessage = bmessage,
                    AlternativeSchemes = altSchemes
                };
                billImage.Source = QrBillImage.CreateImage(bill);
            }
            catch (Exception exept)
            {
                MessageBox.Show("Error creating QR: '{exept}'");
            }
        }

        private void checkBoxTwintInQR_Checked(object sender, RoutedEventArgs e)
        {
            _includeTwint = true;
            refreshDisplay();
        }

        private void checkBoxTwintInQR_Unchecked(object sender, RoutedEventArgs e)
        {
            _includeTwint = false;
            refreshDisplay();
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
