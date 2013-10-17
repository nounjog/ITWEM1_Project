using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ITWEM1_Project.Resources;

namespace ITWEM1_Project
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor

        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();



        }
       static public int id = 1;
        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            if (e.Error == null)
            {
                verifyAuth(e.Result);
            }
           
        }
        String webServicesConnect(String url)
        {
                    String result="";

            Uri uri = new Uri(url);
            WebClient webClient = new WebClient();
            // Register the callback
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(uri);
            return result;

        }
        public void verifyAuth(String res){
            System.Diagnostics.Debug.WriteLine(res);
            int id = int.Parse(res);
           if(id!=0){
               System.Diagnostics.Debug.WriteLine("SUCCES");
               MainPage.id = id;
           }
        }

        private void Button_LogIn_Click_1(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/Resources/Contacts/Contacts.xaml", UriKind.Relative));

            var login = textBox1.Text;
            var pass = textBox2.Text;
            String res = webServicesConnect("http://pierrelt.fr/WindowsPhone/test.php?login="+login+"&password="+pass);
           
          

        }

        public String loadHTMLCallback(Object sender, DownloadStringCompletedEventArgs e)
        {
            var textData = (string)e.Result;
            // Do cool stuff with result
            return textData;
        }




        private void Button_CreateAccount_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Resources/Registration/Registration.xaml", UriKind.Relative));
            System.Diagnostics.Debug.WriteLine("Hello");
        }



        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}