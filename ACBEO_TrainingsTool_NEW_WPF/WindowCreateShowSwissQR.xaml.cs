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
        public WindowCreateShowSwissQR(string trainingDate, string pilotName, string billMessage)
        {
            InitializeComponent();

            // create bill data
            try
            {
                Bill bill = new Bill
                {
                    /*// creditor data
                    Account = "CH0799999999999999999",
                    Creditor = new Address
                    {
                        Name = "Some Club",
                        AddressLine1 = "test 9",
                        AddressLine2 = "3000 Bern",
                        CountryCode = "CH"
                    },

                    // payment data
                    Amount = 199.95m,
                    Currency = "CHF",

                    // debtor data
                    Debtor = new Address
                    {
                        Name = pilotName,         
                        AddressLine1 = "Grosse Marktgasse 28",
                        AddressLine2 = "9400 Rorschach",
                        CountryCode = "CH"
                    },

                    // more payment data
                    ReferenceType = "NON",
                    Reference = "0",
                    //Reference = "21000000000313947143000 9017",
                    UnstructuredMessage = "Abonnement für 2020"*/

                    
                    // creditor data
                    
                    Creditor = new Address
                    {
                        Name = "Some Club",
                        AddressLine1 = "",
                        AddressLine2 = "3000 Bern",
                        CountryCode = "CH"
                    },

                    // payment data
                    Amount = 199.95m,
                    Currency = "CHF",

                    // debtor data
                    Debtor = new Address
                    {
                        Name = "Pia-Maria Rutschmann-Schnyder",
                        AddressLine1 = "Grosse Marktgasse 28",
                        AddressLine2 = "9400 Rorschach",
                        CountryCode = "CH"
                    },

                    // more payment data
                    ReferenceType = Bill.ReferenceTypeNoRef,
                    Reference = "0",
                    //Reference = "21000000000313947143000 9017",
                    UnstructuredMessage = "Abonnement für 2020"
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
