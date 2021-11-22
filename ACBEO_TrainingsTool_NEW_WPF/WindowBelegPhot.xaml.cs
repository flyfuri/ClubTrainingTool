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
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    /// <summary>
    /// Interaction logic for WindowBelegPhot.xaml
    /// </summary>
    public partial class WindowBelegPhot : Window
    {
        private int counterSeconds;
        private string belegPathComplete;
        private string belegPhotoNme;

        private string return_value = "";
        public string return_string { get { return return_value; } }

        bool return_canceled = true;
        public bool wasCanceled { get { return return_canceled; } }

        public WindowBelegPhot(string belegPhotoPath, string belegPhotoName)
        {
            InitializeComponent();
            belegPathComplete = $"{belegPhotoPath}/{belegPhotoName}";
            belegPhotoNme = belegPhotoName;
            counterSeconds = 4;
        }

        

        //*************************************************************************************


        //Anlegen eines Webcam-Objektes
        VideoCaptureDevice videoSource;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         /**   //*********************************************************

            //Anlegen einer Liste mit allen Videoquellen. (Hier können neben der gewünschten Webcam
            //auch TV-Karten, etc. auftauchen)
            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //Überprüfen, ob mindestens eine Aufnahmequelle vorhanden ist
            if (videosources != null)
            {
                //Die erste Aufnahmequelle an unser Webcam Objekt binden
                //(habt ihr mehrere Quellen, muss nicht immer die erste Quelle die
                //gewünschte Webcam sein!)
                videoSource = new VideoCaptureDevice(videosources[0].MonikerString);

                try
                {
                    //Überprüfen ob die Aufnahmequelle eine Liste mit möglichen Aufnahme-
                    //Auflösungen mitliefert.

                    if (videoSource.VideoCapabilities.Length > 0)
                    {
                        string highestSolution = "0;0";
                        //Das Profil mit der höchsten Auflösung suchen
                        for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                        {
                            if (videoSource.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestSolution.Split(';')[0]))
                            {
                                highestSolution = (videoSource.VideoCapabilities[0].FrameSize.Width.ToString() + ";" + videoSource.VideoCapabilities[4].FrameSize.Height.ToString());
                            }
                        }
                        //Dem Webcam Objekt ermittelte Auflösung übergeben
                        //textBoxResVideo.Text = highestSolution;
                        videoSource.VideoResolution = videoSource.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])];
                    }
                }
                catch { }

                //NewFrame Eventhandler zuweisen anlegen.
                //(Dieser registriert jeden neuen Frame der Webcam)
                videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(videoSource_NewFrame);

                System.Drawing. Bitmap emptyBitmap = new Bitmap(500, 500);
                pictureBoxCam.Source = emptyBitmap;
                //timer1.Start();
                //videoSource.Start(); //Das Aufnahmegerät aktivieren
            }**/
        }

        private void buttonCANCEL_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonSAVE_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonSTART_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
