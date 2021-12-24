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
        private string _billMessage;
        private string _twintTitle = "twint";
        private string _twintLink_part1 = "twint/light/02:...#";
        private string _twintLink_part2 = "rn/twint/a...A/rn";

        public WindowCreateShowSwissQR(decimal billAmount, string billMessage, bool includeTwint)
        {
            InitializeComponent();

            AlternativeScheme twintScheme = new AlternativeScheme();
            twintScheme.Name = _twintTitle;
            twintScheme.Instruction = _twintLink_part1;  //first part twint link
            List<AlternativeScheme> altSchemes = new List<AlternativeScheme>();
            altSchemes.Clear();
            if (includeTwint)
            {
                altSchemes.Add(twintScheme);
                if (billMessage.Length > 138 - _twintLink_part2.Length)
                {
                    _billMessage = $"{billMessage.Substring(0, 138 - _twintLink_part2.Length)}  {_twintLink_part2}";
                }
                else
                {
                    _billMessage = $"{billMessage}  {_twintLink_part2}";
                }
            }
            else 
            {
                if (billMessage.Length > 140)
                {
                    _billMessage = billMessage.Substring(0, 140);
                }
                else
                {
                    _billMessage = billMessage;
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
                    Amount = billAmount,
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
                    UnstructuredMessage = _billMessage,
                    AlternativeSchemes = altSchemes
                };
                billImage.Source = QrBillImage.CreateImage(bill);
            }
            catch (Exception exept)
            {
                MessageBox.Show("Error parsing CostBuy : '{exept}'");
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
