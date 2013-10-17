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


namespace ITWEM1_Project.Resources.Contacts
{
    public partial class Contacts : PhoneApplicationPage
    {
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
                    Button customerBlock = new Button();
           
                    customerBlock.Content = test;

                    customersStackPanel.Children.Add(customerBlock);

                }

            }

        }


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

}





