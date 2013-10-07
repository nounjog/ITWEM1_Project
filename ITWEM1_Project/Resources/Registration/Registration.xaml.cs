using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ITWEM1_Project.Resources.Registration
{
    public partial class Registration : PhoneApplicationPage
     {
        public Registration()
        {
            InitializeComponent();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";       
        }
        public class UserInfo
        {
            string uname;
            string pwd;
            string email;
            int mno;
            public string Username
            {
                get { return uname; }
                set { uname = value; }
            }
            public string Password
            {
                get { return pwd; }
                set { pwd = value; }
            }
            public string EmailAdd
            {
                get { return email; }
                set { email = value; }
            }
 
            public int Mobile
            {
                get { return mno; }
                set { mno = value; }
            }
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "") { MessageBox.Show("Plz. enter the username"); }
            if (textBox2.Text == "") { MessageBox.Show("Plz. enter the Password"); }
            if (textBox3.Text == "") { MessageBox.Show("Plz. enter the e-mail add"); }
            if (textBox4.Text == "") { MessageBox.Show("Plz. enter the mobile number");
        }
    }
}