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
    public partial class vendor_products : Form
    {
        OracleConnection con;
        public vendor_products()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
            con.Open();
        }

        private void vendor_products_Load(object sender, EventArgs e)
        {

            textBox7.Text = v_sign_in.venid;
            string query = "SELECT PRODUCT_ID,QUANTITY,BARCODE,PRODUCT_NAME,CATEGORY_ID,PRICE,VENDOR_ID,DESCRIPTION FROM PRODUCT2";
            OracleCommand command = new OracleCommand(query, con);

            OracleDataAdapter adapter = new OracleDataAdapter(command);

            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet, "PRODUCT2");

            dataGridView1.DataSource = dataSet.Tables["PRODUCT2"];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string productId = textBox1.Text;
            string  vendorId = textBox7.Text;
            // Execute a query to retrieve the customer's name and address based on the entered ID
            string query = "SELECT QUANTITY,BARCODE,PRODUCT_NAME, PRICE ,DESCRIPTION FROM PRODUCT2 WHERE PRODUCT_ID = :productId and VENDOR_ID= :vendorid";
            using (OracleCommand command = new OracleCommand(query, con))
            {
                command.Parameters.Add(new OracleParameter(":productId", productId));
                command.Parameters.Add(new OracleParameter(":vendorid", vendorId));
                try
                {
                     
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
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string PROId = textBox1.Text;
            string vendId = textBox7.Text;
            
            // Perform the update query
            try
            {
                string query = "UPDATE PRODUCT2 SET PRODUCT_NAME = :name, QUANTITY = :quantity, BARCODE=:barcode, PRICE=:price, DESCRIPTION=:description WHERE PRODUCT_ID=:PROId ";

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
                    this.Refresh();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("PRODUCT information updated successfully.");

                        // Retrieve all data from the PRODUCT1 table
                        string selectQuery = "SELECT PRODUCT_ID, QUANTITY, BARCODE, PRODUCT_NAME, CATEGORY_ID, PRICE, VENDOR_ID, DESCRIPTION FROM PRODUCT2 WHERE VENDOR_ID=:vendid and PRODUCT_ID=: PROId";
                        
                        using (OracleDataAdapter adapter = new OracleDataAdapter(selectQuery, con))
                        {
                            command.Parameters.Add(new OracleParameter(":PROID", PROId));
                            command.Parameters.Add(new OracleParameter(":vend", vendId));
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;
                        }

                        //dataGridView1.Refresh(); // Refresh the DataGridView
                        //this.Refresh();
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
            string vid = textBox7.Text;
            
            // Perform the delete query
            try
            {
                string query = "DELETE FROM PRODUCT2 WHERE PRODUCT_ID = :proId";

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
                        string selectQuery = "SELECT PRODUCT_ID, QUANTITY, BARCODE, PRODUCT_NAME, CATEGORY_ID, PRICE, VENDOR_ID, DESCRIPTION FROM PRODUCT2";
                        using (OracleDataAdapter adapter = new OracleDataAdapter(selectQuery, con))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;
                        }

                        dataGridView1.Refresh(); // Refresh the DataGridView

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

        private void label8_Click(object sender, EventArgs e)
        {
             VENODR_HOME v =new VENODR_HOME();
            v.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
