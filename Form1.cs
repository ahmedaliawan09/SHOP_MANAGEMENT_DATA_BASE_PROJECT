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
    public partial class Form1 : Form
    {
        OracleConnection connn;   
        public Form1()
        {
            InitializeComponent();
        }
       // OleDbConnection conn = new OleDbConnection("Provider=MSDAORA;Data Source=XE;Persist Security Info=True;User ID=21f9245;Password=123456789;Unicode=True");
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginButton_Click(sender, e);
        }
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter a username and password");
            }
            else
            {
                string query = "SELECT COUNT(*) FROM aadmin WHERE userr = :userr AND passwordd= :passwordd";
                OracleCommand command = new OracleCommand(query, connn);
                command.Parameters.Add(":userr", textBox1.Text);
                command.Parameters.Add(":passwordd", textBox2.Text);

                // Open the connection before executing the command
              //  connn.Open();

                object result = command.ExecuteScalar();
                int count = result == null ? 0 : Convert.ToInt32(result);

                if (count > 0)
                {
                    // Close the connection before opening the new form
                    connn.Close();

                    this.Hide();
                    Form2 d = new Form2();
                    d.Show();
                   // this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
            //    string sql = "SELECT COUNT(*) FROM t1 WHERE userr = :userr AND passwordd = :passwordd";
            //    OracleCommand command = new OracleCommand(sql, connn);
            //    command.Parameters.Add(new OracleParameter("userr", textBox1.Text));
            //    command.Parameters.Add(new OracleParameter("passwordd", textBox2.Text));
            //    object result = command.ExecuteScalar();
            //    int count = result == null ? 0 : Convert.ToInt32(result);
            //    if (count == 1)
            //    {
            //        MessageBox.Show("Login successful!");
            //        Form2 f = new Form2();
            //        f.Show();
            //        connn.Close();
            //    }
            //    else
            //    {
            //        connn.Close();
            //        MessageBox.Show("Invalid username or password.");
            //    }
           
            }

            private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string conStr= @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            connn = new OracleConnection(conStr);
            connn.Open();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            CUST_SIGN c = new CUST_SIGN();
            c.Show();
            this.Hide();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            v_sign_in v1 = new v_sign_in();
            v1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                this.Hide();
                vendor v4 = new vendor();
                v4.Show();
                //this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            try
            {
                this.Hide();
                CUST_SIGNUP c1 = new CUST_SIGNUP();
                c1.Show();
                //this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
           
        }

      
    }
}
