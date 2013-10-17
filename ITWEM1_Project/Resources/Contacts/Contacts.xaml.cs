using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using System.Data;
using System.Windows.Input;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using System.Device.Location; // Provides the GeoCoordinate class.
using Windows.Devices.Geolocation; //Provides the Geocoordinate class.
using System.Windows.Media;
using System.Windows.Shapes;



namespace ITWEM1_Project.Resources.Contacts
{
    public partial class Contacts : PhoneApplicationPage
    {
        //bool mouseDown;
        public Contacts()
        {
            InitializeComponent();

            // Customers customers = Customers.MakeTestCustomers();
            //StackPanel customersStackPanel = new StackPanel();


            getContact();
        }


        String getContact()
        {
            String result = "";

            Uri uri = new Uri("http://pierrelt.fr/WindowsPhone/getUser.php");
            WebClient webClient = new WebClient();
            // Register the callback
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(uri);
            return result;

        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            if (e.Error == null)
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(e.Result);



                foreach (var child in jsonObj.Children())
                {
                    //System.Diagnostics.Debug.WriteLine("");
                    string test = child.login.Value;
                    TextBlock customerBlock = new TextBlock();
                    customerBlock.Text = test;

                    customersStackPanel.Children.Add(customerBlock);
                    customerBlock.Name = child.id.Value;
                    customerBlock.MouseLeftButtonUp += customerBlock_MouseLeftButtonUp;


                }
            }
        }
        void customerBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock customerBlock = (TextBlock)sender;
            string id_receveur = customerBlock.Name;
            GetCoordinates(id_receveur);

        }

        private async void GetCoordinates(string id_receveur)
        {
            // Get the phone's current location.
            Geolocator MyGeolocator = new Geolocator();
            MyGeolocator.DesiredAccuracyInMeters = 5;
            Geoposition MyGeoPosition = null;
            try
            {
                MyGeoPosition = await MyGeolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                //MyCoordinates.Add(new GeoCoordinate(MyGeoPosition.Coordinate.Latitude, MyGeoPosition.Coordinate.Longitude));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Location is disabled in phone settings or capabilities are not checked.");
            }
            catch (Exception ex)
            {
                // Something else happened while acquiring the location.
                MessageBox.Show(ex.Message);
            }

            String res = webServicesConnect("http://pierrelt.fr/WindowsPhone/localisation2.php?latitude=" + MyGeoPosition.Coordinate.Latitude + "&longitude=" + MyGeoPosition.Coordinate.Longitude + "&id=" + id_receveur + "&id_e=" + MainPage.id);


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

                if ("OK".Equals(res))
                {
                    System.Diagnostics.Debug.WriteLine("SUCCES");


                }

            }

        }
    }

    //void customerBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    //{
    //    mouseDown = true
    //}

    public class Customer
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public Customer(string inName,
        int inID)
        {
            Name = inName;
            ID = inID;
        }
    }
    public class Customers
    {
        public string Name { get; set; }


        public Customers(string inName)
        {
            Name = inName;
            CustomerList = new List<Customer>();
        }
        public List<Customer> CustomerList;


        public static Customers MakeTestCustomers(string[] firstNames)
        {
            Customers result = new Customers("list");
            int id = 0;

            foreach (string firstname in firstNames)
            {
                //Construct a customer name
                string name = firstname;
                //Add the new customer to the list
                result.CustomerList.Add(new Customer(name, id));
                // Increase the ID for the next customer
                id++;
            }

            return result;
        }
        /*  Customers customers = Customers.MakeTestCustomers();
          StackPanel customersStackPanel = new StackPanel();

          foreach (Customer c in customers.CustomerList)
          {
              TextBlock customerBlock = new TextBlock();
              customerBlock.Text = c.Name;
              customersStackPanel.Children.Add(customerBlock);
          }

          return result;
      }*/
    }



}




