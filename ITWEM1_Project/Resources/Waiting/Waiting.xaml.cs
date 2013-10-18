using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;


namespace ITWEM1_Project.Resources.Waiting
{
    public partial class Waiting : PhoneApplicationPage
    {

       static public string status = "";
        public Waiting()
        {
            InitializeComponent();

        }

        String webServicesConnect(String url)
        {
            String result = "";

            Uri uri = new Uri(url);
            WebClient webClient = new WebClient();
            // Register the callback
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(uri);
            return result;

        }
   
        private void Click_Refresh(object sender, RoutedEventArgs e)
        {
            String res = webServicesConnect("http://pierrelt.fr/WindowsPhone/refresh.php?id=" + MainPage.id);
        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Result);

            int id = int.Parse(e.Result);

            if (e.Result != "")
            {
                status = e.Result;
                NavigationService.Navigate(new Uri("/Resources/Maps/Maps.xaml", UriKind.Relative));

                }
            }
        }
    }
