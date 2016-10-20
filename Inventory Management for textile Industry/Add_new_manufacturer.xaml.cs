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
    /// Interaction logic for Add_new_manufacturer.xaml
    /// </summary>
    public partial class Add_new_manufacturer : Window
    {
        public Add_new_manufacturer()
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
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                int id = Convert.ToInt32(textBox.Text);
                string nm = textBox1.Text;
                string phn = textBox2.Text;
                string loc = textBox3.Text;
                int pid= Convert.ToInt32(comboBox.SelectedItem);
                string scmd = "insert into manufacturer values( " + id + ",'" + nm + "','" + phn + "','" + loc + "'," + pid + ");";
                //MessageBox.Show(scmd);
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                MessageBox.Show("Successfull Added Manufacturer");
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
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from product", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var valen = dr["PID"];
                    if (!comboBox.Items.Contains(valen))
                    {
                        comboBox.Items.Add(valen);
                    }
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

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                conn.Open();
                int ctid = Convert.ToInt32(comboBox.SelectedItem);
                MySqlCommand cmd = new MySqlCommand("Select * from Product where PID=" + ctid + ";", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                label3.Content = dr["Pname"].ToString();
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
