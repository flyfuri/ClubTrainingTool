using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class WindowBelegPhoto : Window
    {
        private int counterSeconds;
        private string belegPathComplete;
        private string belegPhotoNme;

        private int defaultCamNr = 2;
        private int camNr = 0;
        //Anlegen eines Webcam-Objekte
        FilterInfoCollection videosources;
        VideoCaptureDevice videoSource;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private string return_value = "";
        public string return_string { get { return return_value; } }

        bool return_canceled = true;
        public bool wasCanceled { get { return return_canceled; } }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        //transform image to Bitmap (like old forms pictureBox.drawToBitmap
        private static void CreateBitmapFromVisual(Visual target, string fileName)
        {
            if (target == null || string.IsNullOrEmpty(fileName))
            {
                return;
            }

            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext context = visual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(target);
                //context.DrawRectangle(visualBrush, null, new Rect(Point(), bounds.Size));
                context.DrawRectangle(visualBrush, null, new Rect(0,0,renderTarget.Width, renderTarget.Height));
            }

            renderTarget.Render(visual);
            PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
            using (Stream stm = File.Create(fileName))
            {
                bitmapEncoder.Save(stm);
            }
        }

       //Window Constructor
        public WindowBelegPhoto(string belegPhotoPath, string belegPhotoName)
        {
            InitializeComponent();
            belegPathComplete = $"{belegPhotoPath}/{belegPhotoName}";
            belegPhotoNme = belegPhotoName;
            counterSeconds = 4;

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 800);  //800ms
        }

        //*************************************************************************************
        //Window Loeaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //*********************************************************

            //Anlegen einer Liste mit allen Videoquellen. (Hier können neben der gewünschten Webcam
            //auch TV-Karten, etc. auftauchen)
            videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            ComboBoxChooseCam.Items.Clear();
            for (int i = 0; i < videosources.Count; i++)
            {
                ComboBoxChooseCam.Items.Add(videosources[i].Name.ToString());
            }

            if (videosources.Count >= defaultCamNr)
            {
                ComboBoxChooseCam.SelectedIndex = defaultCamNr;
                camNr = defaultCamNr;
            }
            else
            {
                ComboBoxChooseCam.SelectedIndex = 0;
                camNr = 0;
            }
            

           if (camNr < 0) { camNr = 0; }
           startOrSwitchCamera();
        }


        private void startOrSwitchCamera()
        {
            //Überprüfen, ob mindestens eine Aufnahmequelle vorhanden ist
            if (videosources != null)
            {
                if (videoSource != null)
                {
                    if (videoSource.IsRunning)
                    {
                        videoSource.SignalToStop();
                        videoSource.WaitForStop();
                    }
                }

                buttonSnapshot.Content = "new Snapshot (live view)";

                if (camNr > videosources.Count - 1)
                {
                    camNr = videosources.Count - 1;
                }
                //get the chosen cam
                videoSource = new VideoCaptureDevice(videosources[camNr].MonikerString);

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


                //old forms: Bitmap emptyBitmap = new Bitmap(500, 500);
                //old forms: pictureBoxCam.Source = emptyBitmap;

                pictureBoxCam.Source = null;
                //timer1.Start();
                //videoSource.Start(); //Das Aufnahmegerät aktivieren
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)  //from form ..shown event
        {
            FileInfo fileInfoPic = new FileInfo(belegPathComplete);
            if (fileInfoPic.Exists)
            {
                using (var showBitmap = new Bitmap(belegPathComplete))
                {
                    //old forms: pictureBoxCam.Image = (Bitmap)showBitmap.Clone();
                    pictureBoxCam.Source = BitmapToImageSource(showBitmap);
                }
            }
        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            //Jedes ankommende Objekt als Bitmap casten und der Picturebox zuweisen
            //(Denkt an das ".Clone()", um Zugriffsverletzungen aus dem Weg zu gehen.)
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream ms = new MemoryStream();
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    bi.StreamSource = ms;
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();
                }
                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate { pictureBoxCam.Source = bi; }));
            }
            catch (Exception ex)
            {
                //catch your error here
            }

            /*old forms
            try
            {
                pictureBoxCam.Source = (Bitmap)eventArgs.Frame.Clone();
            }
            catch
            {
            }*/
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            counterSeconds--;
            labelCountdown.Content = counterSeconds.ToString();
            if (counterSeconds <= 0)
            {
                dispatcherTimer.Stop();
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                buttonSAVE.IsEnabled = true;
                counterSeconds = 4;
            }
        }

        private void buttonSTART_Click(object sender, RoutedEventArgs e)
        {
            labelCountdown.Content = counterSeconds.ToString();
            dispatcherTimer.Start();
            videoSource.Start();
            buttonSAVE.IsEnabled = false;
        }
        private void buttonCANCEL_Click(object sender, RoutedEventArgs e)
        {
            if(videoSource != null)
            {
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();
                }
            }
            return_canceled = true;
            Close();
        }

        private void buttonSAVE_Click(object sender, RoutedEventArgs e)
        {
            /*System.IO.MemoryStream ms = new System.IO.MemoryStream();
           pictureBoxCam.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
           byte[] ar = new byte[ms.Length];
           ms.Write(ar, 0, ar.Length);*/

            try
            {
                /*using (var bitmap = new Bitmap((int)pictureBoxCam.Width, (int)pictureBoxCam.Height))
                {
                    //old forms: pictureBoxCam.DrawToBitmap(bitmap, pictureBoxCam.ClientRectangle);
                    bitmap. = pictureBoxCam. Source as BitmapImage;
                    /*System.IO.FileInfo fileInfo = new System.IO.FileInfo(belegPathComplete);
                    if(fileInfo.Exists)
                    {
                        string newpath = belegPathComplete.Replace(".jpg", "_old.jpg");
                        fileInfo.MoveTo(newpath);
                    }*/
                /*try
                {
                    bitmap.Save(belegPathComplete, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"error while saving picture: {ex}");
                }
            }*/

                CreateBitmapFromVisual(pictureBoxCam, belegPathComplete);

                return_value = belegPathComplete;
                return_canceled = false;
            }
            catch
            {
                return_canceled = true;
            }
            Close();
        }

        private void buttonSnapshot_Click(object sender, RoutedEventArgs e)
        {
            if (dispatcherTimer.IsEnabled || videoSource.IsRunning)
            {
                dispatcherTimer.Stop();
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                buttonSnapshot.Content = "new Snapshot (live view)";
                buttonSAVE.IsEnabled = true;
            }
            else
            {
                buttonSnapshot.Content = "Take Snapshot (or click picture)";
                videoSource.Start();
                buttonSAVE.IsEnabled = false;
            }
        }

        private void pictureBoxCam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)  //pictureBox (from old forms) is actually an image control (WPF)
        {
            if (dispatcherTimer.IsEnabled || videoSource.IsRunning)
            {
                dispatcherTimer.Stop();
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                buttonSnapshot.Content = "new Snapshot";
                buttonSAVE.IsEnabled = true;
            }
        }

        private void ComboBoxChooseCam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            camNr = ComboBoxChooseCam.SelectedIndex;
            if (camNr < 0) { camNr = 0; }
            startOrSwitchCamera();
        }
    }
}
