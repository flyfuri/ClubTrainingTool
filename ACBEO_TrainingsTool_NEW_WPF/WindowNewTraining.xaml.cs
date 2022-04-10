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
    /// Interaction logic for WindowNewTraining.xaml
    /// </summary>
    public partial class WindowNewTraining : Window
    {
        Training return_value;
        public Training lastNewTrainigWithID { get { return return_value; } }

        bool return_canceled;
        public bool wasCanceled { get { return return_canceled; } }

        DataAccess db = new DataAccess();
        public WindowNewTraining()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            datePickerTrainingDate.SelectedDate = DateTime.Today.Date;
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            List<Training> trainings = new List<Training>();

            trainings = db.getTrainingsOrderedByDate(1);

            decimal parseDecResult;
            if (!(decimal.TryParse(textBoxCashBefore.Text, out parseDecResult)))
            {
                parseDecResult = -1;
            }

            bool found = false;
            foreach (Training training in trainings)
            {
                if (training.TrainingDate == datePickerTrainingDate.SelectedDate)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                MessageBox.Show("Error: Training allready exists!");
            }
            /*else if ((dateTimePicker1.Value.Date.CompareTo(DateTime.Today)) < 0)
            {
                string msgBoxAnswer = "":
                MessageBox.Show("Error: Training is in the past!", msgBoxAnswer, MessageBoxButtons.OKCancel,MessageBoxIcon.None ,MessageBoxDefaultButton.Button1);
                msgBoxAnswer.
            }*/
            else if (parseDecResult < 0)
            {
                MessageBox.Show("Error: invalid CashBeforeTraining/ ungültige Eingabe Stand Kasse vor Training ");
            }
            else
            {
                List<Training> listTrainingsToAdd = new List<Training>();
                Training trainingToAdd = new Training();
                Training trainingJustAdded = new Training();

                trainingToAdd.TrainingDate = datePickerTrainingDate.SelectedDate.Value;
                trainingToAdd.CashAtBegin = parseDecResult;
                listTrainingsToAdd.Add(trainingToAdd);
                db.addTrainings(listTrainingsToAdd);

                listTrainingsToAdd = db.getTrainingByDate(trainingToAdd.TrainingDate);
                trainingJustAdded = listTrainingsToAdd[0];
                return_value = trainingJustAdded;

                return_canceled = false;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            //List<Training> listTrainingsToAdd = new List<Training>();
            Training trainingToAdd = new Training();

            trainingToAdd.TrainingDate = DateTime.MinValue;
            //listTrainingsToAdd.Add(trainingToAdd);
            //db.addTrainings(listTrainingsToAdd);

            trainingToAdd.TrainingID = -1;
            return_value = trainingToAdd;
            return_canceled = true;
            Close();
        }

        private void textBoxCashBefore_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool boolFormWasCancled = false;
            decimal decimalFormKeyDecResult = 0;
            decimal defaultValue = 0;

            decimal numValue;
            bool parsed = decimal.TryParse(textBoxCashBefore.Text, out numValue);

            if (parsed)
            {
                defaultValue = numValue;
            }
            else
            {
                defaultValue = 0;
            }

            WindowDialogKeyNumDecimal formBuyKeyNumInt = new WindowDialogKeyNumDecimal(false,0);
            formBuyKeyNumInt.Owner = App.Current.MainWindow;
            formBuyKeyNumInt.Title = "Enter cash in briefcase before training (please count!)";
            formBuyKeyNumInt.ShowDialog();
            boolFormWasCancled = formBuyKeyNumInt.wasCanceled;
            decimalFormKeyDecResult = formBuyKeyNumInt.return_decimal;
            textBoxCashBefore.Text = decimalFormKeyDecResult.ToString();
        }
    }
}
