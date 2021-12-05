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
    /// Interaction logic for WindowSetLeiter.xaml
    /// </summary>
    public partial class WindowSetLeiter : Window
    {
        private List<Participant> actParticipants;
        private int chsnLeiterID;

        Participant return_valueParticipant;
        public Participant return_participant { get { return return_valueParticipant; } }

        bool return_canceled;
        public bool wasCanceled { get { return return_canceled; } }

        public WindowSetLeiter(List<Participant> actualParticipants, int choosenLeiterID)
        {
            InitializeComponent();
            actParticipants = actualParticipants;
            chsnLeiterID = choosenLeiterID;

            List<string> comboBoxTexts = new List<string>();
            comboBoxTexts.Clear();
            foreach (Participant participant in actParticipants)
            {
                comboBoxTexts.Add(participant.ComplNameAndLicence);
            }
            comboBoxChoseLeiter.ItemsSource = comboBoxTexts;
            int i = 0;
            foreach (Participant participant in actParticipants)
            {
                if (participant.ParticipantID == chsnLeiterID)
                {
                    comboBoxChoseLeiter.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            return_canceled = false;
            return_valueParticipant = actParticipants[comboBoxChoseLeiter.SelectedIndex];
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            return_canceled = true;
            return_valueParticipant = actParticipants[comboBoxChoseLeiter.SelectedIndex];
            Close();
        }
    }
}
