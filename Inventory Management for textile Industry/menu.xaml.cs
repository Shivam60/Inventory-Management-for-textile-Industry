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

namespace Inventory_Management_for_textile_Industry
{
    /// <summary>
    /// Interaction logic for menu.xaml
    /// </summary>
    public partial class menu : Window
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Login men = new Login();
            men.Show();
            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Ledger men = new Ledger();
            men.Show();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Employee_details men = new Employee_details();
            men.Show();
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Manufacturer_details men = new Manufacturer_details();
            men.Show();
            this.Close();

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Product_details men = new Product_details();
            men.Show();
            this.Close();
            
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Add_new_employee men = new Add_new_employee();
            men.Show();
            this.Close();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Add_new_product men = new Add_new_product();
            men.Show();
            this.Close();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            Add_new_manufacturer men = new Add_new_manufacturer();
            men.Show();
            this.Close();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            Employee_view men = new Employee_view();
            men.Show();
            this.Close();
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            Product__view men = new Product__view();
            men.Show();
            this.Close();
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            Manufacturer__view men = new Manufacturer__view();
            men.Show();
            this.Close();
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            Ledger_view men = new Ledger_view();
            men.Show();
            this.Close();
        }
    }
}
