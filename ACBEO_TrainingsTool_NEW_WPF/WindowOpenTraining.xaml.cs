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
    /// Interaction logic for WindowOpenTraining.xaml
    /// </summary>
    public partial class WindowOpenTraining : Window
    {
        
        int return_value;
        public int return_TrainingsID { get { return return_value; } }

        bool return_canceled;
        public bool wasCanceled { get { return return_canceled; } }

        DataAccess db = new DataAccess();
        List<Training> trainings = new List<Training>();

        public WindowOpenTraining(int actYear)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            trainings = db.getTrainingByYEAR(actYear);
            comboBoxChoseToOpen.Items.Clear();
            foreach (Training training in trainings)
            {
                comboBoxChoseToOpen.Items.Add(training.TrainingDate.ToShortDateString());
            }
            comboBoxChoseToOpen.SelectedIndex = 0;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            return_value = (int)trainings[comboBoxChoseToOpen.SelectedIndex].TrainingID;
            return_canceled = false;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            return_value = -1;
            return_canceled = true;
            Close();
        }

        private void comboBoxChoseToOpen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBox1.Text = comboBoxChoseToOpen.SelectedIndex.ToString();
        }
    }
}
