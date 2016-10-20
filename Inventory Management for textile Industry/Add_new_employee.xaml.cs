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
    /// Interaction logic for Add_new_employee.xaml
    /// </summary>
    public partial class Add_new_employee : Window
    {
        public Add_new_employee()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=shivam;Database=wpf");
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            menu men = new menu();
            men.Show();
            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                int id = Convert.ToInt32(textBox4.Text);
                string nm = textBox1.Text;
                string dsg = textBox2.Text;
                int ag = Convert.ToInt32(textBox.Text);
                int sal = Convert.ToInt32(textBox3.Text);
                string scmd = "insert into employee values( " + id + ",'" + nm + "'," + sal + ",'"+dsg+"',"+ag+ ");";
                //MessageBox.Show(scmd);
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                MessageBox.Show("Successfull Added Employee");
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
    }
}
