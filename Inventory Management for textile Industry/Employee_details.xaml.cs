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
    /// Interaction logic for Employee_details.xaml
    /// </summary>
    public partial class Employee_details : Window
    {
        public Employee_details()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=shivam;Database=wpf");
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from Employee", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var valen = dr["EID"];
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton.IsChecked == true)
            {
                try
                {
                    conn.Open();
                    string nm = textBox1.Text.ToString();
                    string dg = textBox2.Text.ToString();
                    int sl = Convert.ToInt32(textBox3.Text);
                    int ed = Convert.ToInt32(comboBox.SelectedItem);
                    int ag = Convert.ToInt32(textBox.Text);
                    string scmd = "update employee set name =\"" + nm + "\", salary= " + sl + ", designation= \"" + dg + "\", age="+ag+" where eid=" + ed + ";";
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                conn.Open();
                string scmd = "Select * from Employee where eid=" + comboBox.SelectedValue.ToString();
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                textBox1.Text = dr["name"].ToString();
                textBox2.Text = dr["designation"].ToString();
                textBox3.Text = dr["salary"].ToString();
                textBox.Text = dr["age"].ToString();
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
        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            textBox1.IsEnabled = true;
            textBox2.IsEnabled = true;
            textBox3.IsEnabled = true;
        }
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            ;
        }

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

        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton.IsChecked == true)
            {
                try
                {
                    conn.Open();
                    var mid = Convert.ToInt32(comboBox.SelectedItem);
                    string scmd = "delete from employee where eid=" + mid + ";";
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
