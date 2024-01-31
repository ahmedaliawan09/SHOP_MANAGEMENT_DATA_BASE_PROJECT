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
    public partial class PURCHASE_PRODUCT : Form
    {
        OracleConnection con;
        string num = "";
        public PURCHASE_PRODUCT()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
            con.Open();
        }

        private void PURCHASE_PRODUCT_Load(object sender, EventArgs e)
        {
            num = v_sign_in.venid;
            string query = "SELECT * FROM PRODUCT2 ";
            OracleCommand command = new OracleCommand(query, con);
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "PRODUCT2");
            dataGridView1.DataSource = dataSet.Tables["PRODUCT2"];

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)


        {
            int productID;
            if (!int.TryParse(textBox1.Text, out productID))
            {
                MessageBox.Show("Invalid product ID.");
                return;
            }

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Please fill all fields");
                    return;
                }

                string selectedProductID = textBox1.Text;
                int quantity = int.Parse(textBox2.Text);

                // Check if the selected product exists in the PRODUCT2 table
                string checkProductQuery = "SELECT COUNT(*) FROM PRODUCT2 WHERE PRODUCT_ID = :productID";
                using (OracleCommand checkProductCommand = new OracleCommand(checkProductQuery, con))
                {
                    checkProductCommand.Parameters.Add(new OracleParameter(":productID", selectedProductID));
                    int productCount = Convert.ToInt32(checkProductCommand.ExecuteScalar());

                    if (productCount == 0)
                    {
                        MessageBox.Show("No product found with the specified ID");
                        return;
                    }
                }



            

            // Check if there are enough items available
            string checkQuantityQuery = "SELECT QUANTITY FROM PRODUCT2 WHERE PRODUCT_ID = :productID";
                using (OracleCommand checkQuantityCommand = new OracleCommand(checkQuantityQuery, con))
                {
                    checkQuantityCommand.Parameters.Add(new OracleParameter(":productID", selectedProductID));
                    int availableQuantity = Convert.ToInt32(checkQuantityCommand.ExecuteScalar());

                    if (quantity > availableQuantity)
                    {
                        MessageBox.Show("Not enough items available");
                        return;
                    }
                }
            decimal price = 0;
            string getPriceQuery = "SELECT PRICE FROM PRODUCT2 WHERE PRODUCT_ID = :productID";
            using (OracleCommand command = new OracleCommand(getPriceQuery, con))
            {
                command.Parameters.Add(new OracleParameter(":productID", textBox1.Text));

                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null && decimal.TryParse(result.ToString(), out price))
                    {
                        // Price retrieved successfully
                    }
                    else
                    {
                        MessageBox.Show("Unable to retrieve price for the given product ID.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving price: " + ex.Message);
                    return;
                }
            }

            // Calculate the debit (price * quantity)
            decimal debit = price * decimal.Parse(textBox2.Text);

            // Retrieve the vendor ID based on the product ID
            int vendorID = 0;
            string getVendorIDQuery = "SELECT VENDOR_ID FROM PRODUCT2 WHERE PRODUCT_ID = :productID";
            using (OracleCommand command = new OracleCommand(getVendorIDQuery, con))
            {
                command.Parameters.Add(new OracleParameter(":productID", textBox1.Text));

                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out vendorID))
                    {
                        // Vendor ID retrieved successfully
                    }
                    else
                    {
                        MessageBox.Show("Unable to retrieve vendor ID for the given product ID.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving vendor ID: " + ex.Message);
                    return;
                }
            }

            string categoryName = string.Empty;
            string getCategoryQuery = "SELECT c.CATEGORY_NAME FROM CATEGORY c JOIN PRODUCT2 p ON c.CATEGORY_ID = p.CATEGORY_ID WHERE p.PRODUCT_ID = :productID";
            using (OracleCommand command = new OracleCommand(getCategoryQuery, con))
            {
                command.Parameters.Add(new OracleParameter(":productID", productID));

                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        categoryName = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving category name: " + ex.Message);
                    return;
                }
            }












            DateTime currentDate = DateTime.Now;
            string saleDate = currentDate.ToString("dd-MMM-yy");
            // Insert data into SALES table
            string insertQueryy = "INSERT INTO SALES (SALE_DATE, CATEGORY_NAME, QUANTITY, VEND_ID, DEBIT, PRODUCT_ID) " +
                                 "VALUES (:saleDate, :categoryName, :quantity, :vendorID, :debit, :productID)";

            using (OracleCommand command = new OracleCommand(insertQueryy, con))
            {
                command.Parameters.Add(new OracleParameter(":saleDate", saleDate));
                command.Parameters.Add(new OracleParameter(":categoryName", categoryName));
                command.Parameters.Add(new OracleParameter(":quantity", textBox2.Text));
                command.Parameters.Add(new OracleParameter(":vendorID", vendorID));
                command.Parameters.Add(new OracleParameter(":debit", debit));
                command.Parameters.Add(new OracleParameter(":productID", textBox1.Text));

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data inserted into SALES table successfully.");

                        // Update the quantity in the PRODUCT2 table
                    }
                    else
                    {
                        MessageBox.Show("Error inserting data into SALES table.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting data into SALES table: " + ex.Message);
                }
            }














            // Insert data into PRODUCT table
            string insertQuery = "INSERT INTO PRODUCT (PRODUCT_ID, QUANTITY, BARCODE, PRODUCT_NAME, CATEGORY_ID, PRICE, VENDOR_ID,DESCRIPTION) " +
                                     "SELECT :productID, :quantity, BARCODE, PRODUCT_NAME, CATEGORY_ID, PRICE, VENDOR_ID,DESCRIPTION " +
                                     "FROM PRODUCT2 " +
                                     "WHERE PRODUCT_ID = :productID";
                using (OracleCommand insertCommand = new OracleCommand(insertQuery, con))
                {
                    insertCommand.Parameters.Add(new OracleParameter(":productID", selectedProductID));
                    insertCommand.Parameters.Add(new OracleParameter(":quantity", quantity));

                    try
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Item purchased");

                            // Delete the purchased item from PRODUCT2 table
                            string deleteQuery = "DELETE FROM PRODUCT2 WHERE PRODUCT_ID = :productID";
                            using (OracleCommand deleteCommand = new OracleCommand(deleteQuery, con))
                            {
                                deleteCommand.Parameters.Add(new OracleParameter(":productID", selectedProductID));
                                int deleteRowsAffected = deleteCommand.ExecuteNonQuery();

                                if (deleteRowsAffected > 0)
                                {
                                    // MessageBox.Show("Item removed from PRODUCT2 table");
                                }
                                else
                                {
                                    MessageBox.Show("Failed to remove item from PRODUCT2 table");
                                }
                            }

                            // Refresh the data grid view
                            string refreshQuery = "SELECT * FROM PRODUCT2";
                            OracleCommand refreshCommand = new OracleCommand(refreshQuery, con);
                            OracleDataAdapter adapter = new OracleDataAdapter(refreshCommand);
                            DataSet dataSet = new DataSet();
                            adapter.Fill(dataSet, "PRODUCT2");
                            dataGridView1.DataSource = dataSet.Tables["PRODUCT2"];
                        }
                        else
                        {
                            MessageBox.Show("Failed to purchase item");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }


        
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2();
            f.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
