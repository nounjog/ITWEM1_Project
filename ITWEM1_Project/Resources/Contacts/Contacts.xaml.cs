using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.StackPanel;


namespace ITWEM1_Project.Resources.Contacts
{
    public partial class Contacts : PhoneApplicationPage
    {
        public Contacts()
        {
            InitializeComponent();
        }

        public class Customer
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public int ID { get; set; }
            public Customer(string inName, string inAddress,
            int inID)
            {
                Name = inName;
                Address = inAddress;
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


            public static Customers MakeTestCustomers()
            {

                string[] firstNames = new string[] { "Rob", "Jim", "Joe", "Nigel", "Sally", "Tim" };
                string[] lastsNames = new string[] { "Smith", "Jones", "Bloggs", "Miles", "Wilkinson", "Brown" };

                Customers result = new Customers("");

                int id = 0;
                foreach (string lastName in lastsNames)
                {
                    foreach (string firstname in firstNames)
                    {
                        //Construct a customer name
                        string name = firstname + " " + lastName;
                        //Add the new customer to the list
                        result.CustomerList.Add(new Customer(name,
                        name + "'s House", id));
                        // Increase the ID for the next customer
                        id++;
                    }
                }

                return result;
            }

            public static Customers Affichage ()
            {
                Customers customers = Customers.MakeTestCustomers();
                StackPanel customersStackPanel = new StackPanel();

                foreach (Customer c in customers.CustomerList)
                {
                    TextBlock customerBlock = new TextBlock();
                    customerBlock.Text = c.Name;
                    customersStackPanel.Children.Add(customerBlock);
                }
                return customersStackPanel;
            }
        }
    }
}
    


 

