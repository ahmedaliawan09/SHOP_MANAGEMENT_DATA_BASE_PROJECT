using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;
namespace SHOP_SYSTEM
{
    public partial class CUST_SIGNUP : Form
    {
       
        OracleConnection con;
        public CUST_SIGNUP()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
            con.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text)
                || string.IsNullOrEmpty(textBox3.Text)
                || string.IsNullOrEmpty(textBox4.Text)
                || string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            if (!int.TryParse(textBox5.Text, out int phoneNumber) && textBox5.Text.Length != 11)
            {
                MessageBox.Show("Invalid phone number. Please enter a valid 11-digit number.");
                return;
            }

            OracleCommand insertEmp = null;
            try
            {
                insertEmp = con.CreateCommand();
                insertEmp.CommandText = "INSERT INTO CUSTOMER (CUST_ID, NAME, PASWOORDD, ADDRESS, PHONE) VALUES (:custID, :name, :password, :address, :phone)";
                insertEmp.CommandType = CommandType.Text;

                insertEmp.Parameters.Add(":custID", OracleDbType.Varchar2).Value = textBox1.Text;
                insertEmp.Parameters.Add(":name", OracleDbType.Varchar2).Value = textBox2.Text;
                insertEmp.Parameters.Add(":password", OracleDbType.Varchar2).Value = textBox3.Text;
                insertEmp.Parameters.Add(":address", OracleDbType.Varchar2).Value = textBox4.Text;
                insertEmp.Parameters.Add(":phone", OracleDbType.Varchar2).Value = textBox5.Text;

                int rows = insertEmp.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Customer added successfully.");
                    this.Hide();
                    CUSTOMER_HOME a = new CUSTOMER_HOME();
                    a.Show();
                }
                else
                {
                    MessageBox.Show("Failed to add customer.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while adding customer: " + ex.Message);
            }



        }
        private static readonly Random rand = new Random();
        public static int Generate()
        {
            Random rand = new Random();
            int number;

            do
            {
                // Generate a random number between 100 and 999 (3 digits)
                number = rand.Next(100, 1000);
            }
            while (number < 100 || number >= 1000);

            return number;
        }


        private void CUST_SIGNUP_Load(object sender, EventArgs e)
        {
            string randomNum = Generate().ToString();
            textBox1.Text = randomNum;
            textBox1.ReadOnly = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 C = new Form1();
            C.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
