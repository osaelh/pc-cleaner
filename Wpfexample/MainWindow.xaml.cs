using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Timer = System.Threading.Timer;

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
            MessageBox.Show("Some URL");
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
            spaceToClean.Content = "The size of " + di.Name + " is \n" + b / 1000000 + " Megabytes.\n";
            

            if ((string)lbl.Content == "Analyse du PC necessaire")
            {
                lbl.Content = "Analyse en cours...";
                
            }
           

            btnUpd.Visibility = Visibility.Hidden;
            btnNet.Visibility = Visibility.Hidden;
            btnHis.Visibility = Visibility.Hidden;
            date.Visibility = Visibility.Hidden;

            analyser.Content = "Arreter";



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
    }
}
