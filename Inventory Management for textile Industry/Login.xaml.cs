using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Inventory_Management_for_textile_Industry
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=shivam;Database=wpf");
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                string user = textBox.Text;
                string pas = textBox1.Text;
                string scmd = "select password from login where username= \"" + user + "\";";
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                string pass = dr["password"].ToString();
                if (pass == pas)
                {
                    MessageBox.Show("Logged in");
                    menu men = new menu();
                    men.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error in authentication");
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome To Inventory Management for Textile Industry");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            new_user men = new new_user();
            men.Show();
            this.Close();
        }
    }
}
