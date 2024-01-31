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
    public partial class CUSTOMER_ACCOUNT : Form
    {
        OracleConnection con;
         
        public CUSTOMER_ACCOUNT()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
             
        }

        private void CUSTOMER_ACCOUNT_Load(object sender, EventArgs e)
        {
            string query = "SELECT CUST_ID, NAME ,ADDRESS FROM CUSTOMER";
            OracleCommand command = new OracleCommand(query, con);

            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "CUSTOMER");

            dataGridView1.DataSource = dataSet.Tables["CUSTOMER"];
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string customerId = textBox1.Text;

            // Execute a query to retrieve the customer's name and address based on the entered ID
            string query = "SELECT NAME, ADDRESS FROM CUSTOMER WHERE CUST_ID = :customerId";
            using (OracleCommand command = new OracleCommand(query, con))
            {
                command.Parameters.Add(new OracleParameter(":customerId", customerId));

                try
                {
                    con.Open();
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox2.Text = reader["NAME"].ToString();
                            textBox3.Text = reader["ADDRESS"].ToString();
                        }
                        else
                        {
                            textBox2.Text = string.Empty;
                            textBox3.Text = string.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string custId = textBox1.Text;
            con.Open();
            // Perform the update query
            try
            {
                string query = "UPDATE CUSTOMER SET NAME = :name, ADDRESS = :address WHERE CUST_ID = :custId";

                using (OracleCommand command = new OracleCommand(query, con))
                {
                    // Assuming you have corresponding textboxes for name and address
                    command.Parameters.Add(":name", textBox2.Text);
                    command.Parameters.Add(":address", textBox3.Text);
                    command.Parameters.Add(":custId", custId);

                    // Open the connection before executing the command
                   

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer information updated successfully.");
                        string selectQuery = "SELECT CUST_ID, NAME, ADDRESS FROM CUSTOMER";
                        using (OracleDataAdapter adapter = new OracleDataAdapter(selectQuery, con))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;
                        }
                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No customer found with the provided customer ID.");
                        this.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer information: " + ex.Message);
                this.Refresh();
            }
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string custId = textBox1.Text;
             con.Open();
            // Perform the delete query
            try
            {
                string query = "DELETE FROM CUSTOMER WHERE CUST_ID = :custId";

                using (OracleCommand command = new OracleCommand(query, con))
                {
                    command.Parameters.Add(":custId", custId);

                    // Open the connection before executing the command
                    

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer deleted successfully.");

                        // Clear the textboxes
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";

                        // Reload the data from the database and rebind to the DataGridView
                        string selectQuery = "SELECT CUST_ID, NAME, ADDRESS FROM CUSTOMER";
                        using (OracleDataAdapter adapter = new OracleDataAdapter(selectQuery, con))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;
                        }
                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No customer found with the provided customer ID.");
                        this.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer: " + ex.Message);
                this.Refresh();
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CUST_SIGNUP c = new CUST_SIGNUP();
            c.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 F = new Form2();
            F.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
