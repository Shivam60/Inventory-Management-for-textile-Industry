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
    /// Interaction logic for Add_new_product.xaml
    /// </summary>
    public partial class Add_new_product : Window
    {
        public Add_new_product()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            menu men = new menu();
            men.Show();
            this.Close();
        }
        public int get_pri_key()
        {
            try
            {
                conn.Open();
                string scmd = "select * from ledger";
                int i = 1;
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ++i;
                }
                return i;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return 1;
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
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=shivam;Database=wpf");
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(textBox.Text);
            string nm = textBox1.Text;
            int qty = Convert.ToInt32(textBox2.Text);
            int prc = Convert.ToInt32(textBox3.Text);
            try
            {
                conn.Open();
                
                string scmd = "insert into product values( " + id + ",'" + nm + "'," + qty + "," + prc + ");";
              // MessageBox.Show(scmd);
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                MessageBox.Show("Successfull Added Product");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            int lid= get_pri_key();
            string unit = "Kilogram";
            try
            {
                var dt = DateTime.Now;
                string i_dt = dt.ToString("yyyy-MM-dd");
                conn.Open();
                string scmd = "insert into ledger values(" + lid + "," + id + ",'" + nm + "','" +unit+ "',null,null,null,null,null,null,null,null,'" + i_dt + "'," + qty + "," + prc + ","+qty*prc+");";
                MessageBox.Show(scmd);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           
        }

        
    }
}
