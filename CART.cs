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
    public partial class CART : Form
    {
        OracleConnection con;
        string num = "";

        public CART()
        {


            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
            con.Open();
        }

        private void CART_Load(object sender, EventArgs e)
        {


            num = CUST_SIGN.cus_id;

            string query = "SELECT *from CART WHERE CUSTOMER_ID = :customerID";
            OracleCommand command = new OracleCommand(query, con);
            command.Parameters.Add(new OracleParameter(":customerID", num));

            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Category");

            dataGridView1.DataSource = dataSet.Tables["Category"];


            string queryy = "SELECT ADDRESS FROM CUSTOMER WHERE CUST_ID = :customerID";
            OracleCommand command1 = new OracleCommand(queryy, con);
            command1.Parameters.Add(new OracleParameter(":customerID", num));

            OracleDataAdapter adapterr = new OracleDataAdapter(command1);
            DataSet dataSet1 = new DataSet();
            adapterr.Fill(dataSet1, "CART");

            dataGridView2.DataSource = dataSet1.Tables["CART"];




        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void CalculatePrice()
        {
            // Get the product ID and quantity from the textboxes
            int productID = 0;
            int quantity = 0;

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {

            }
            else
            {
                productID = Convert.ToInt32(textBox1.Text);
                quantity = Convert.ToInt32(textBox2.Text);
            }
            // Retrieve the price from the database based on the product ID
            string getPriceQuery = "SELECT PRICE FROM PRODUCT WHERE PRODUCT_ID = :productID";
            using (OracleCommand command = new OracleCommand(getPriceQuery, con))
            {
                command.Parameters.Add(new OracleParameter(":productID", productID));

                try
                {
                    object result = command.ExecuteScalar();
                    if (result == null)
                    {

                    }
                    else
                    {
                        decimal price = Convert.ToDecimal(result);
                        decimal totalPrice = price * quantity;

                        // Display the calculated price in the textbox
                        textBox4.Text = totalPrice.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving price: " + ex.Message);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {

            }
            else
                CalculatePrice();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            string customerID = num;
            // Perform the update query
            try
            {
                string query = "UPDATE CUSTOMER SET ADDRESS = :address WHERE CUST_ID = :customerID";

                using (OracleCommand command = new OracleCommand(query, con))
                {
                    command.Parameters.Add(new OracleParameter(":address", textBox3.Text));
                    command.Parameters.Add(new OracleParameter(":customerID", customerID));

                    // Open the connection before executing the command


                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer information updated successfully.");

                        // Refresh the grid with the updated information
                        RefreshGrid();
                    }
                    else
                    {
                        MessageBox.Show("No customer found with the provided customer ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer information: " + ex.Message);
            }




        }
        public void RefreshGrid()
        {
            // Retrieve the updated customer information from the database
            string query = "SELECT ADDRESS FROM CUSTOMER WHERE CUST_ID = :customerID";
            using (OracleCommand command = new OracleCommand(query, con))
            {
                command.Parameters.Add(new OracleParameter(":customerID", num));

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "CUSTOMER");

                // Display the updated information in the grid
                dataGridView2.DataSource = dataSet.Tables["CUSTOMER"];
            }

        }

        private bool IsProductInCart(int productID)
        {
            string query = "SELECT COUNT(*) FROM CART WHERE PRODUCT_ID = :productID";
            using (OracleCommand command = new OracleCommand(query, con))
            {
                command.Parameters.Add(new OracleParameter(":productID", productID));

                try
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking product in CART table: " + ex.Message);
                    return false;
                }
            }
        }

        public bool IsQuantityAvailable(int productID, int quantity)
        {
            string checkQuantityQuery = "SELECT QUANTITY FROM PRODUCT WHERE PRODUCT_ID = :productID";
            using (OracleCommand command = new OracleCommand(checkQuantityQuery, con))
            {
                command.Parameters.Add(new OracleParameter(":productID", productID));

                try
                {
                    int availableQuantity = Convert.ToInt32(command.ExecuteScalar());
                    return quantity <= availableQuantity;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking quantity availability in PRODUCT table: " + ex.Message);
                    return false;
                }
            }
        }

        public bool DeleteProduct(int productID, OracleConnection con)
        {
            string deleteQuery = "DELETE FROM PRODUCT WHERE PRODUCT_ID = :productID";

            using (OracleCommand command = new OracleCommand(deleteQuery, con))
            {
                command.Parameters.Add(new OracleParameter(":productID", productID));

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting product: " + ex.Message);
                    return false;
                }
            }
        }

        public bool quantity_updated(int productID, int quantity, OracleConnection con)
        {
            string updateQuery = "UPDATE PRODUCT1 SET QUANTITY = QUANTITY - :quantity WHERE PRODUCT_ID = :productID";

            using (OracleCommand command = new OracleCommand(updateQuery, con))
            {
                command.Parameters.Add(new OracleParameter(":quantity", quantity));
                command.Parameters.Add(new OracleParameter(":productID", productID));

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the quantity is still greater than zero
                    return rowsAffected > 0 && GetProductQuantity(productID, con) > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating product quantity: " + ex.Message);
                    return false;
                }
            }
        }

        public int GetProductQuantity(int productID, OracleConnection con)
        {
            string query = "SELECT QUANTITY FROM PRODUCT1 WHERE PRODUCT_ID = :productID";

            using (OracleCommand command = new OracleCommand(query, con))
            {
                command.Parameters.Add(new OracleParameter(":productID", productID));

                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int quantity))
                    {
                        return quantity;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving product quantity: " + ex.Message);
                }

                return 0;
            }
        }


        public bool IsProductQuantityZero(int productID, OracleConnection con)
        {
            string checkQuery = "SELECT QUANTITY FROM PRODUCT1 WHERE PRODUCT_ID = :productID";

            using (OracleCommand command = new OracleCommand(checkQuery, con))
            {
                command.Parameters.Add(new OracleParameter(":productID", productID));

                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int quantity))
                    {
                        return quantity == 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking product quantity: " + ex.Message);
                }

                return false;
            }
        }


        // Function to delete a product from the PRODUCT1 table
        private bool DeleteProduct1(int productID, OracleConnection con)
        {
            string deleteProduct1Query = "DELETE FROM PRODUCT1 WHERE PRODUCT_ID = :productID";
            using (OracleCommand deleteProduct1Command = new OracleCommand(deleteProduct1Query, con))
            {
                deleteProduct1Command.Parameters.Add(new OracleParameter(":productID", productID));
                try
                {
                    int rowsDeletedFromProduct1 = deleteProduct1Command.ExecuteNonQuery();
                    if (rowsDeletedFromProduct1 > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting product from PRODUCT1 table: " + ex.Message);
                    return false;
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
             

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please enter all the required fields.");
                return;
            }

            // Get the values from the textboxes
            int productID;
            if (!int.TryParse(textBox1.Text, out productID))
            {
                MessageBox.Show("Invalid product ID.");
                return;
            }

            int customerID;
            if (!int.TryParse(CUST_SIGN.cus_id, out customerID))
            {
                MessageBox.Show("Invalid customer ID.");
                return;
            }

            decimal price;
            if (!decimal.TryParse(textBox4.Text, out price))
            {
                MessageBox.Show("Invalid price.");
                return;
            }

            int quantity;
            if (!int.TryParse(textBox2.Text, out quantity))
            {
                MessageBox.Show("Invalid quantity.");
                return;
            }

            // Check if the product exists in the CART table
            if (!IsProductInCart(productID))
            {
                MessageBox.Show("Product does not exist in CART table.");
                return;
            }

            // Check if the quantity is available in the PRODUCT1 table
            if (!IsQuantityAvailable(productID, quantity))
            {
                MessageBox.Show("Not enough items available.");
                return;
            }

            // Get the current date
            DateTime currentDate = DateTime.Now;
            string saleDate = currentDate.ToString("dd-MMM-yy");

            // Retrieve the category name based on the product ID
            // Retrieve the category name based on the product ID
            string categoryName = string.Empty;
            string getCategoryQuery = "SELECT c.CATEGORY_NAME FROM CATEGORY c JOIN PRODUCT1 p ON c.CATEGORY_ID = p.CATEGORY_ID WHERE p.PRODUCT_ID = :productID";
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

            int availableQuantity = GetProductQuantity(productID, con);

            int purchaseQuantity = quantity; // Assuming 'quantity' is already an integer

            // Validate the purchase quantity
            if (purchaseQuantity <= 0)
            {
                MessageBox.Show("Invalid quantity. Please enter a positive quantity.");
                return;
            }
            else if (purchaseQuantity > availableQuantity)
            {
                MessageBox.Show("Insufficient quantity. Only " + availableQuantity + " available.");
                return;
            }

            string insertQuery = "INSERT INTO SALES (SALE_DATE, CATEGORY_NAME, QUANTITY, CUST_ID, DEBIT, CREDIT, ADDRESS, PRODUCT_ID) " +
                                 "VALUES (:saleDate, :categoryName, :quantity, :customerID, 0, :price, :address, :productID)";

            using (OracleCommand command = new OracleCommand(insertQuery, con))
            {
                command.Parameters.Add(new OracleParameter(":saleDate", saleDate));
                command.Parameters.Add(new OracleParameter(":categoryName", categoryName));
                command.Parameters.Add(new OracleParameter(":quantity", quantity.ToString()));
                command.Parameters.Add(new OracleParameter(":customerID", customerID));
                command.Parameters.Add(new OracleParameter(":price", price));
                command.Parameters.Add(new OracleParameter(":address", textBox3.Text));
                command.Parameters.Add(new OracleParameter(":productID", productID));

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data inserted into SALES table successfully.");

                        // Delete the purchased product from the cart
                        string deleteCartQuery = "DELETE FROM CART WHERE PRODUCT_ID = :productID";
                        using (OracleCommand deleteCartCommand = new OracleCommand(deleteCartQuery, con))
                        {
                            deleteCartCommand.Parameters.Add(new OracleParameter(":productID", productID));
                            try
                            {
                                int rowsDeleted = deleteCartCommand.ExecuteNonQuery();
                                if (rowsDeleted > 0)
                                {
                                    MessageBox.Show("Product deleted from the cart.");

                                    // Update the quantity in the PRODUCT1 table
                                    if (!quantity_updated(productID, quantity, con))
                                    {
                                        MessageBox.Show("Error updating product quantity.");
                                    }
                                    int productQuantity = GetProductQuantity(productID, con);
                                    if (productQuantity <= 0)
                                    {
                                        // Delete the product from both PRODUCT and PRODUCT1 tables
                                        if (DeleteProduct(productID, con) && DeleteProduct1(productID, con))
                                        {
                                            MessageBox.Show("Product deleted from PRODUCT and PRODUCT1 tables.");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Error deleting product from PRODUCT and PRODUCT1 tables.");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Product quantity updated successfully.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Product not found in the cart.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error deleting product from the cart: " + ex.Message);
                            }
                        }
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

        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            CUSTOMER_HOME C = new CUSTOMER_HOME();
            C.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
