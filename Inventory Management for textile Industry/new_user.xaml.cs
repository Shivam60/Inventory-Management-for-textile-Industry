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
    /// Interaction logic for new_user.xaml
    /// </summary>
    public partial class new_user : Window
    {
        public new_user()
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
                int id = Convert.ToInt32(textBox3.Text);
                string scmd = "insert into login values("+id+",'"+user+"','"+pas+"')"+";";
                MessageBox.Show("User Created");
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();             
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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Login men = new Login();
            men.Show();
            this.Close();
        }
    }
    
}
