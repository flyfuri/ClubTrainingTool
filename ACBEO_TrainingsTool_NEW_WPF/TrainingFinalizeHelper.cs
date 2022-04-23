using System;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    class TrainingFinalizeHelper
    {
        private Training _actTraining;
        public TrainingFinalizeHelper(Training actTraining)
        {
            _actTraining = actTraining;
        }
        public SolidColorBrush calcBGBrush()
        {
            System.Drawing.Color drawingbrushcolor = new System.Drawing.Color();//.LightGray);
            drawingbrushcolor = System.Drawing.Color.LightGray;
            System.Windows.Media.Color mediacolor = new System.Windows.Media.Color();
            SolidColorBrush mediabrush = new SolidColorBrush();
            
            if (!_actTraining.Finalized & _actTraining.TrainingDate.CompareTo(DateTime.Now.Date) < 0) //earlier then today..
            {
                drawingbrushcolor = System.Drawing.Color.Orange;
            }
            else if (!_actTraining.Finalized)
            {
                drawingbrushcolor = System.Drawing.Color.LightGray;
            }
            else if (_actTraining.Finalized & _actTraining.TrainingDate.CompareTo(DateTime.Now.Date) < 0)//erlier than today..
            {
                drawingbrushcolor = System.Drawing.Color.DarkOrange;
            }
            else if (_actTraining.Finalized)
            {
                drawingbrushcolor = System.Drawing.Color.LightGreen;
            }

            mediacolor.A = drawingbrushcolor.A;
            mediacolor.R = drawingbrushcolor.R;
            mediacolor.B = drawingbrushcolor.B;
            mediacolor.G = drawingbrushcolor.G;
            mediabrush.Color = mediacolor;
            return mediabrush;
        }
        
        public bool checkNotFinalized()
        {
            if (!_actTraining.Finalized) 
            {
                return true;
            }
            else
            {
                MessageBoxResult msgBoxResult = MessageBox.Show("This training is allready finallized and can not be modified anymore",
                                                                "",
                                                                MessageBoxButton.OK,
                                                                MessageBoxImage.Warning);
                // = !(msgBoxResult.Equals(MessageBoxResult.Yes));
                return false;
            }
        }
    }
}
