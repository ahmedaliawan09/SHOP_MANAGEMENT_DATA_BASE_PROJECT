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
    public partial class v_sign_in : Form
    {
        public static string venid = "";
        OracleConnection conn;
        public v_sign_in()
        {
            InitializeComponent();
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
                string query = "SELECT COUNT(*) FROM vendor WHERE vendor_id = :vednor_id AND PASWORDD= :PASWORDD";
                OracleCommand command = new OracleCommand(query, conn);
                command.Parameters.Add(":vendor_id", textBox1.Text);
                command.Parameters.Add(":PASWORDD", textBox2.Text);

                // Open the connection before executing the command
                 // conn.Open();

                object result = command.ExecuteScalar();
                int count = result == null ? 0 : Convert.ToInt32(result);

                if (count > 0)
                {
                    // Close the connection before opening the new form
                    conn.Close();

                    this.Hide();
                    VENODR_HOME A = new VENODR_HOME();
                    A.Show();
                    // this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
        }
            private void button1_Click(object sender, EventArgs e)
        {
            venid = textBox1.Text.ToString();
            loginButton_Click(sender, e);
                
            
        }

        private void v_sign_in_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            conn = new OracleConnection(conStr);
            conn.Open();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }
    }
}
