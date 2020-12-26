using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Wpfexample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            webSite.Click += WebSite_Click;
            
        }

        private void WebSite_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Some URL");
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            DirectoryInfo di = new DirectoryInfo("C:\\Windows\\Temp");
            FileInfo[] fiArr = di.GetFiles();
            long b = 0;
            foreach (var fi in fiArr)
            {
                b += fi.Length;
            }
            


            if ((string)lbl.Content == "Analyse du PC necessaire")
            {
                lbl.Content = "Analyse en cours...";

            }


            btnUpd.Visibility = Visibility.Hidden;
            btnNet.Visibility = Visibility.Hidden;
            btnHis.Visibility = Visibility.Hidden;
            date.Visibility = Visibility.Hidden;
            lastAnalysis.Visibility = Visibility.Hidden;

           /* analyser.Content = "Arreter";*/



            Progress.Visibility = Visibility.Visible;

            Thread.Sleep(1000);
            Progress.Value = 0;

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(50);
                    this.Dispatcher.Invoke(() => //Use Dispather to Update UI Immediately  
                    {
                        Progress.Value = i;
                        if (i == 99)
                        {
                            spaceToClean.Content = "The size of " + di.Name + " is \n" + b / 1000000 + " Megabytes.\n";
                            date.Visibility = Visibility.Hidden;
                            lbl.Content = "Analyse terminer!";
                           
                            btnUpd.Visibility = Visibility.Visible;
                            btnNet.Visibility = Visibility.Visible;
                            btnHis.Visibility = Visibility.Visible;
                            analyser.Content = "Analyser";
                           
                            Progress.Visibility = Visibility.Hidden;

                        }

                    });

                }
            });
            File.AppendAllText(@"C:\Users\Administrateur\Desktop\pc-cleaner-master\Wpfexample\history.txt", "Analyse réalisée le : " + DateTime.Now.ToString() + Environment.NewLine);
        }

        private void btnNet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo("C:\\Users\\Administrateur\\Downloads\\hola");
                FileInfo[] fiArr = di.GetFiles();
                foreach (var item in fiArr)
                {
                    item.Delete();
                }
            }
            catch (Exception)
            {

                throw new ArgumentNullException("try closing all apps");
            }
        }

        private void btnHis_Click(object sender, RoutedEventArgs e)
        {
            String line;
        /*    btnHis.Visibility = Visibility.Hidden;
            btnNet.Visibility = Visibility.Hidden;
            btnUpd.Visibility = Visibility.Hidden;*/

            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(@"C:\Users\Administrateur\Desktop\pc-cleaner-master\Wpfexample\history.txt");

            //Read the first line of text
            line = sr.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {
                /*history.Visibility = Visibility.Visible;*/
                //write the lie to console window
                /*txt_output.Items.Add(line);*/
                date.Visibility = Visibility.Visible;
                date.Content = line;
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();

            try
            {
                if (!webClient.DownloadString("http://localhost/pc-cleaner/version.txt").Contains("v0.0.0"))
                {
                    if (System.Windows.Forms.MessageBox.Show("Looks like there is an update! Do you want to download it?", "Demo", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) using (var client = new WebClient())

                        {
                            Process.Start(@".\demoUpdater.exe");
                            this.Close();
                        }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Vous êtes déjà à jour !");
                }
            }
            catch
            {

            }
        }
    }
}
    