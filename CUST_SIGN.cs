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
namespace SHOP_SYSTEM
{
    public partial class CUST_SIGN : Form
    {
        public static string cus_id = "";
        //private int loggedInCustomerID;

        OracleConnection con;
        public CUST_SIGN()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter a username and password");
            }
            else
            {
                string query = "SELECT COUNT(*) FROM CUSTOMER WHERE CUST_ID = :CUST_ID AND PASWOORDD= :PASWoORDD";
                OracleCommand command = new OracleCommand(query, con);
                command.Parameters.Add(":CUST_ID", textBox1.Text);
                command.Parameters.Add(":PASWOORDD", textBox2.Text);

                // Open the connection before executing the command
                  con.Open();

                object result = command.ExecuteScalar();
                int count = result == null ? 0 : Convert.ToInt32(result);
                 
                if (count > 0)
                {
                    // Close the connection before opening the new form
                    con.Close();
                    this.Hide();
                    CUSTOMER_HOME A = new CUSTOMER_HOME();
                    A.Show();
                   
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
                con.Close();
            }

        }
        private void button1_Click(object sender, EventArgs e) {
            cus_id = textBox1.Text.ToString();
            loginButton_Click(sender, e);
    }

        private void CUST_SIGN_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
