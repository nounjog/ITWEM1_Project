using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ITWEM1_Project.Resources.Waiting
{
    public partial class Waiting : PhoneApplicationPage
    {
        public Waiting()
        {
            InitializeComponent();
            String res = webServicesConnect("http://pierrelt.fr/WindowsPhone/refresh.php?id=" + MainPage.id);

        }

        String webServicesConnect(String url)
        {
            String result = "";

            Uri uri = new Uri(url);
            WebClient webClient = new WebClient();
            // Register the callback
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_downloadStringCompleted);
            webClient.DownloadStringAsync(uri);
            return result;

        }
        void webClient_downloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            if (e.Error == null)
            {
                String res = e.Result;

                System.Diagnostics.Debug.WriteLine(e.Result);

            }

        }
        private void Click_Refresh(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Resources/Waiting/Waiting.xaml?Refresh=true", UriKind.Relative));
        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            if (e.Result == "OK")
            {
                NavigationService.Navigate(new Uri("/Resources/Maps/Maps.xaml", UriKind.Relative));

                }
            }
        }
    }
