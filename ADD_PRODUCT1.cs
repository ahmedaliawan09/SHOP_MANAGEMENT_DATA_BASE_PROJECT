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
    public partial class ADD_PRODUCT1 : Form
    {
        public static String PID = "";
        OracleConnection con;
        public ADD_PRODUCT1()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
        }

        private void ADD_PRODUCT1_Load(object sender, EventArgs e)
        {
            PID = INVENTORY.pro_id;
         
            string query = "SELECT PRODUCT_ID,QUANTITY,BARCODE,PRODUCT_NAME,CATEGORY_ID,PRICE,VENDOR_ID,DESCRIPTION FROM PRODUCT1";
            OracleCommand command = new OracleCommand(query, con);
          
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            
            DataSet dataSet = new DataSet();
         
            adapter.Fill(dataSet, "PRODUCT1");
         
            dataGridView2.DataSource = dataSet.Tables["PRODUCT1"];

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string productId = textBox1.Text;

            // Execute a query to retrieve the customer's name and address based on the entered ID
            string query = "SELECT QUANTITY,BARCODE,PRODUCT_NAME, PRICE ,DESCRIPTION FROM PRODUCT1 WHERE PRODUCT_ID = :productId";
            using (OracleCommand command = new OracleCommand(query, con))
            {
                command.Parameters.Add(new OracleParameter(":productId", productId));

                try
                {
                    con.Open();
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox2.Text = reader["QUANTITY"].ToString();
                            textBox3.Text = reader["BARCODE"].ToString();
                            textBox4.Text = reader["PRODUCT_NAME"].ToString();
                            textBox5.Text = reader["PRICE"].ToString();
                            textBox6.Text = reader["DESCRIPTION"].ToString();

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
            string PROId = textBox1.Text;
            con.Open();
            // Perform the update query
            try
            {
                string query = "UPDATE PRODUCT1 SET PRODUCT_NAME = :name, QUANTITY = :quantity, BARCODE=:barcode, PRICE=:price, DESCRIPTION=:description WHERE PRODUCT_ID=:PROId";

                using (OracleCommand command = new OracleCommand(query, con))
                {
                    // Assuming you have corresponding textboxes for name and address
                    command.Parameters.Add(":name", textBox4.Text);
                    command.Parameters.Add(":quantity", textBox2.Text);
                    command.Parameters.Add(":barcode", textBox3.Text);
                    command.Parameters.Add(":price", textBox5.Text);
                    command.Parameters.Add(":description", textBox6.Text);
                    command.Parameters.Add(":PROID", PROId);

                    // Open the connection before executing the command

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("PRODUCT information updated successfully.");

                        // Retrieve all data from the PRODUCT1 table
                        string selectQuery = "SELECT PRODUCT_ID, QUANTITY, BARCODE, PRODUCT_NAME, CATEGORY_ID, PRICE, VENDOR_ID, DESCRIPTION FROM PRODUCT1";
                        using (OracleDataAdapter adapter = new OracleDataAdapter(selectQuery, con))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView2.DataSource = dataTable;
                        }

                        dataGridView2.Refresh(); // Refresh the DataGridView
                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No PRODUCT found with the provided PRODUCT ID.");
                        this.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product information: " + ex.Message);
                this.Refresh();
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string proid = textBox1.Text;
            con.Open();
            // Perform the delete query
            try
            {
                string query = "DELETE FROM PRODUCT1 WHERE PRODUCT_ID = :proId";

                using (OracleCommand command = new OracleCommand(query, con))
                {
                    command.Parameters.Add(":proID", proid);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product deleted from shop.");

                        // Clear the textboxes
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";

                        // Reload the data from the database and rebind to the DataGridView
                        string selectQuery = "SELECT PRODUCT_ID, QUANTITY, BARCODE, PRODUCT_NAME, CATEGORY_ID, PRICE, VENDOR_ID, DESCRIPTION FROM PRODUCT1";
                        using (OracleDataAdapter adapter = new OracleDataAdapter(selectQuery, con))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView2.DataSource = dataTable;
                        }

                        dataGridView2.Refresh(); // Refresh the DataGridView

                        // Close the connection after refreshing the DataGridView
                        con.Close();

                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No product found with the provided product ID.");
                        this.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
                this.Refresh();
                con.Close();
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2();
            f.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
