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
using QRCoder;
using System.Drawing;
using MyWPFExtentions;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for WindowShowQRCode.xaml
    /// </summary>
    /// 
    public partial class WindowShowQRCode : Window
    {
        private string _stringQRCode;
        public WindowShowQRCode(string strQRCodeString)
        {
            InitializeComponent();
            _stringQRCode = strQRCodeString;
            textBox_stringQRCode.Text = strQRCodeString;
        }

        private void buttonCLOSE_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_ShowQRCode_Loaded(object sender, RoutedEventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(_stringQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            ///pictureBox_ShowCode.Image = qrCode.GetGraphic(12);
            pictureBox_ShowCode.Source = (qrCode.GetGraphic(12)).toImageSource();

            buttonClose.Focus();
        }

        private void checkBoxTwintInQR_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
