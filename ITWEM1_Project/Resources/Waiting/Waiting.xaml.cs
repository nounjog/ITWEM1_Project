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
