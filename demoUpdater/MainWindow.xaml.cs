using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
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

namespace demoUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            var client = new WebClient();

            //Thread.Sleep(5000);
            File.Delete(@"C:\Users\Administrateur\Desktop\pc-cleaner-master\Wpfexample\bin\Release\Wpfexample.exe");
            //if crash , everything lost , best not delate and replace directly 
            //File.Delete(@"C:\Users\Youcode\source\repos\brief 3\brief 3\bin\Release\brief 3.exe");
            client.DownloadFile("http://localhost/pc-cleaner/Wpfexample.zip", @"C:\Users\Administrateur\Desktop\pc-cleaner-master\Wpfexample\bin\Release\Wpfexample.zip");
            string zipPath = @"C:\Users\Administrateur\Desktop\pc-cleaner-master\Wpfexample\bin\Release\Wpfexample.zip";
            //. pour racourcir 
            string extractPath = @"C:\Users\Administrateur\Desktop\pc-cleaner-master\Wpfexample\bin\Release";
            ZipFile.ExtractToDirectory(zipPath, extractPath);

            // not delate the zip file and leave it as backup by rename
            File.Delete(@"C:\Users\Administrateur\Desktop\pc-cleaner-master\Wpfexample\bin\Release\Wpfexample.zip");
            Process.Start(@"C:\Users\Administrateur\Desktop\pc-cleaner-master\Wpfexample\bin\Release\Wpfexample.exe");
            this.Close();



       
        }
    }
    }

