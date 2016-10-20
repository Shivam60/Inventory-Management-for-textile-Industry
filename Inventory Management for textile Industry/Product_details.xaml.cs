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
    /// Interaction logic for Product_details.xaml
    /// </summary>
    public partial class Product_details : Window
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=shivam;Database=wpf");
        public Product_details()
        {
            InitializeComponent();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
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

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            textBox1.IsEnabled = false;

        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            textBox1.IsEnabled = true;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                conn.Open();
                string scmd = "Select * from product where pid=" + comboBox.SelectedValue.ToString();
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                textBox1.Text = dr["pname"].ToString();
               
                textBox3.Text = dr["pqty"].ToString();
                textBox4.Text = dr["pprice"].ToString();
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

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton1.IsChecked == true)
            {
                try
                {
                    conn.Open();
                    string pnm = textBox1.Text.ToString();
                    int pps = Convert.ToInt32(textBox3.Text);
                    int pqt = Convert.ToInt32(textBox4.Text);
                    int pid = Convert.ToInt32(comboBox.SelectedItem);
                    string scmd = "update product set pname =\"" + pnm + "\", pqty= " + pqt + ", pprice= " + pps + " where pid=" + pid + ";";
                    MySqlCommand cmd = new MySqlCommand(scmd, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    MessageBox.Show("Record Updated Succesfully");
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
            else
            {
                MessageBox.Show("Read Only Mode Selected");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            menu men = new menu();
            men.Show();
            this.Hide();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Add_new_product men = new Add_new_product();
            men.Show();
            this.Close();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton1.IsChecked == true)
            {
                try
                {
                    conn.Open();
                    var mid = Convert.ToInt32(comboBox.SelectedItem);
                    string scmd = "delete from product where pid=" + mid + ";";
                    MySqlCommand cmd = new MySqlCommand(scmd, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    MessageBox.Show("Record Deleted Succesfully");
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
            else
            {
                MessageBox.Show("Read Only Mode Selected");
            }
        }
    }
}
