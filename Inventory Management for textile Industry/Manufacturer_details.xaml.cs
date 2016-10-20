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
    /// Interaction logic for Manufacturer_details.xaml
    /// </summary>
    public partial class Manufacturer_details : Window
    {
        public Manufacturer_details()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=shivam;Database=wpf");
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from Manufacturer", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var valen = dr["MID"];
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
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from product", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var valen = dr["PID"];
                    if (!comboBox1.Items.Contains(valen))
                    {
                        comboBox1.Items.Add(valen);
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
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton1.IsChecked == true)
            {
                try
                {
                    conn.Open();
                    var mid = Convert.ToInt32(comboBox.SelectedItem);
                    String mnm=textBox1.Text;
                    string mcn=textBox2.Text;
                    int mprod= Convert.ToInt32(comboBox1.SelectedItem);
                    string mloc=textBox4.Text;
                    string scmd = "update manufacturer set Mname ='" + mnm + "', mphone= '" + mcn + "', mproduct= " + mprod + ", mlocation = '"+ mloc + "'where mid=" + mid + ";";
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

    private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            textBox1.IsEnabled = false;
            textBox2.IsEnabled = false;
           // comboBox1.IsEnabled = false;
            textBox4.IsEnabled = false;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            textBox1.IsEnabled = true;
            textBox2.IsEnabled = true;
            comboBox1.IsEnabled = true;
            textBox4.IsEnabled = true;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                conn.Open();
                string scmd = "Select * from manufacturer where mid=" + comboBox.SelectedValue.ToString();
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                textBox1.Text = dr["mname"].ToString();
                textBox2.Text = dr["mphone"].ToString();
                textBox4.Text = dr["mlocation"].ToString();
                label4.Content = dr["Mproduct"].ToString();
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
            menu men = new menu();
            men.Show();
            this.Hide();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                conn.Open();
                int ctid = Convert.ToInt32(comboBox1.SelectedItem);
                MySqlCommand cmd = new MySqlCommand("Select * from Product where PID=" + ctid + ";", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                label4.Content = dr["Pname"].ToString();
               // MessageBox.Show("Shown");
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
            Add_new_manufacturer men = new Add_new_manufacturer();
            men.Show();
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
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
                    string scmd = "delete from manufacturer where mid=" + mid + ";";
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
