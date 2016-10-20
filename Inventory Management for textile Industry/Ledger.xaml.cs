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
    /// Interaction logic for Ledger.xaml
    /// </summary>
    public partial class Ledger : Window
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=shivam;Database=wpf");
        public Ledger()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            menu men = new menu();
            men.Show();
            this.Close();
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
                label8.Content = label4.Content = dr["pname"].ToString();
                label10.Content = label5.Content = dr["pqty"].ToString();
                label14.Content = label6.Content = dr["pprice"].ToString();
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
       
            Issue.Visibility = Visibility;
            deposit.Visibility = Visibility.Hidden;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {

            deposit.Visibility = Visibility;
            Issue.Visibility = Visibility.Hidden;
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
        public void change_prod_qty(int pid, int cqty)
        {
            try
            {
                conn.Open();
                string scmd = "update product set  pqty= " + cqty + " where pid=" + pid + ";";
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
        public int get_prod_qty(int pid)
        {
            int ans;
            try
            {
                conn.Open();
                string scmd = "select * from Product where pid= "+pid+";";
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                ans = Convert.ToInt32(dr["pqty"]);
                return ans;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public int get_prod_prc(int pid)
        {
            int ans;
            try
            {
                conn.Open();
                string scmd = "select * from Product where pid= " + pid + ";";
                MySqlCommand cmd = new MySqlCommand(scmd, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                ans = Convert.ToInt32(dr["pprice"]);
                return ans;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return -1;
            }
            finally
            {
                conn.Close();
            }
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
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //hjkhjk
            int lid = get_pri_key();
            int pid= Convert.ToInt32(comboBox.SelectedItem);
            string pnm = label4.Content.ToString();
            int iqty = Convert.ToInt32(textBox.Text);
            int i_uprc = Convert.ToInt32(label6.Content);
            int i_prc = iqty * i_uprc;
            string unit = "Kilograms";
            int prod_qty = get_prod_qty(pid);
            var dt = DateTime.Now;
            string i_dt = dt.ToString("yyyy-MM-dd");

            if (radioButton.IsChecked.Value && iqty<=prod_qty )
            {
                int cqty =  prod_qty- iqty;
                change_prod_qty(pid, cqty);
                MessageBox.Show("Issue successfull, Issue bill is of total "+i_prc+" Rupees");
                try
                {
                    conn.Open();
                    string scmd = "insert into ledger values("+lid+","+pid+",'"+pnm+"','"+unit + "'," + "null,null,null,null,'"+i_dt+"',"+iqty+","+i_uprc+","+i_prc+",'"+i_dt + "'," + cqty + "," + i_uprc+","+ i_prc +  ");";
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
            else
            {
                MessageBox.Show("issue Not selected");
            }           
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int lid = get_pri_key();
            string unit = "Kilograms";
            int pid = Convert.ToInt32(comboBox.SelectedItem);
            string pnm = label4.Content.ToString();
            int r_qty = Convert.ToInt32(textBox1.Text);
            int r_uprc = Convert.ToInt32(textBox2.Text);
            int prod_qty = get_prod_qty(pid);
            int prod_prc = get_prod_prc(pid);
            int cqty = r_qty + prod_qty;
            int r_prc = r_qty*r_uprc;
            float cprc = (r_uprc*r_qty+ prod_prc*prod_qty) / cqty;
            float invt_cst = cprc * cqty;
            var dt = DateTime.Now;
            string r_dt = dt.ToString("yyyy-MM-dd");
            change_prod_qty(pid, cqty);
            MessageBox.Show("Receipt successfull, receipt bill is of total " + r_prc + " Rupees");
            try
            {
                
                
                //Console.Write(r_dt);
                conn.Open();
                string scmd = "insert into ledger values(" + lid + "," + pid + ",'" + pnm + "','"+unit+"','" + r_dt + "'," + r_qty + "," + r_uprc + "," + r_prc + ",null,null,null,null,'" +r_dt+"',"+cqty+","+cprc+","+invt_cst +");";
               // MessageBox.Show(scmd);
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
            try
            {
                conn.Open();
                string scmd = "update product set pprice=" + cprc + " where pid=" + pid + ";";
             //   MessageBox.Show(scmd);
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

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Ledger_view men = new Ledger_view();
            men.Show();
            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
