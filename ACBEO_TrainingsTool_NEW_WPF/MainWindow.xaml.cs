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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool trainingOpen;
        public static int actYEAR;
        public Training actualTraining { get; set; } //actual training 
        public MainWindow()
        {
            InitializeComponent();

            actYEAR = DateTime.Today.Year;
            updateWindowTitle();
            trainingOpen = false;

            MainFrame.Content = new PageTrainingg();
        }


        public void updateWindowTitle() 
        {
            this.Title = actYEAR.ToString();
        }
                
/**********Main Menue Events**********************************************************/
        private void MenueTraining_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)  //routed event (tunneling mode) see https://docs.microsoft.com/en-us/archive/msdn-magazine/2008/september/advanced-wpf-understanding-routed-events-and-commands-in-wpf
        {   
            if (((MainFrame.Content as Page).Name.Equals("Page_Training") == false) & (e.Source as ItemsControl).Name.Equals(MenueTraining.Name))
            {
                foreach (ItemsControl m in MenueMain.Items)
                {
                    m.IsEnabled = true;
                }
                MainFrame.Content = new PageTrainingg();
            }
        }
        
        private void MenueTraining_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items) 
            {
                m.IsEnabled = true;
            }
            MainFrame.Content = new PageTrainingg();
        }
        private void MenueScan_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items)
            {
                if (m.Equals(e.Source as ItemsControl) == false)
                {
                   m.IsEnabled = true;
                }
                else
                {
                   m.IsEnabled = false;
                }
            }
            MainFrame.Content = new PageParticipant ();
        }

        private void MenueTurns_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items)
            {
                if (m.Equals(e.Source as ItemsControl) == false)
                {
                    m.IsEnabled = true;
                }
                else
                {
                    m.IsEnabled = false;
                }
            }
            MainFrame.Content = new PageTurns();
        }

        private void MenueBuy_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items)
            {
                if (m.Equals(e.Source as ItemsControl) == false)
                {
                    m.IsEnabled = true;
                }
                else
                {
                    m.IsEnabled = false;
                }
            }
            MainFrame.Content = new PageBuy();
        }

        private void MenuePay_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemsControl m in MenueMain.Items)
            {
                if (m.Equals(e.Source as ItemsControl) == false)
                {
                    m.IsEnabled = true;
                }
                else
                {
                    m.IsEnabled = false;
                }
            }
            MainFrame.Content = new PageDayPilotCosts();
        }

    /**********Menue Events Training Menues**********************************************************/
        private void MenuItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowDialogKeyNumDecimal formBuyKeyNumInt = new WindowDialogKeyNumDecimal(true, actYEAR);
            formBuyKeyNumInt.ShowDialog();
            if (formBuyKeyNumInt.wasCanceled == false)
            {
                decimal formAnswer = Decimal.ToInt32(formBuyKeyNumInt.return_decimal);
                if (formAnswer < 2017)
                {
                    actYEAR = 2017;
                }
                else if (formAnswer > DateTime.Today.Year)
                {
                    actYEAR = DateTime.Today.Year;
                }
                else
                {
                    actYEAR = Decimal.ToInt32(formAnswer);
                }
                updateWindowTitle();
            }
        }
    }
}

