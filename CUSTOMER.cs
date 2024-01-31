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
    public partial class CUSTOMER : Form
    {
        OracleConnection con;
      
        public CUSTOMER()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
            con.Open();
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;

        }

        private void product_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Items.Clear(); // Clear previous items

                string selectedCategory = comboBox1.SelectedItem.ToString();
                string query = "SELECT PRODUCT_NAME FROM PRODUCT1 WHERE CATEGORY_ID = (SELECT CATEGORY_ID FROM CATEGORY WHERE CATEGORY_NAME = :categoryName)";

                using (OracleCommand command = new OracleCommand(query, con))
                {
                    command.Parameters.Add(new OracleParameter(":categoryName", selectedCategory));

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader["PRODUCT_NAME"].ToString());
                        }   
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void CUSTOMER_Load(object sender, EventArgs e)
        {
                textBox3.Text = CUST_SIGN.cus_id;

                
            try
            {
                string query = "SELECT CATEGORY_NAME FROM CATEGORY WHERE CATEGORY_ID IN (SELECT CATEGORY_ID FROM PRODUCT1)";
                

                using (OracleCommand command = new OracleCommand(query, con))
        {
            using (OracleDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["CATEGORY_NAME"].ToString());
                }
            }
        }
           }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message);
    }
            try
            {
                string query1 = "SELECT PRODUCT_NAME FROM PRODUCT1";

                using (OracleCommand command = new OracleCommand(query1, con))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader["PRODUCT_NAME"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedItem != null)
                {
                    string selectedProduct = comboBox2.SelectedItem.ToString();
                    string query = "SELECT PRODUCT_ID FROM PRODUCT1 WHERE PRODUCT_NAME = :productName";

                    using (OracleCommand command = new OracleCommand(query, con))
                    {
                        command.Parameters.Add(new OracleParameter(":productName", selectedProduct));

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox2.Text = reader["PRODUCT_ID"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            if (comboBox2.SelectedItem != null)
            {
                string selectedProduct = comboBox2.SelectedItem.ToString();
                string query = "SELECT QUANTITY, PRICE FROM PRODUCT1 WHERE PRODUCT_NAME = :productName";

                using (OracleCommand command = new OracleCommand(query, con))
                {
                    command.Parameters.Add(new OracleParameter(":productName", selectedProduct));

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = reader["QUANTITY"].ToString();
                            textBox4.Text = reader["PRICE"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No product found with the selected name.");
                        }
                    }
                }
            }


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string productName = comboBox2.SelectedItem?.ToString();
            string categoryName = comboBox1.SelectedItem?.ToString();
            int quantity = 0;
            int productID;  
            int customerID;  

            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(productName))
            {
                MessageBox.Show("Please select a category and product.");
                return;
            }
            else
                  productID = Convert.ToInt32(textBox2.Text);
                  customerID = Convert.ToInt32(textBox3.Text);

            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                if (!int.TryParse(textBox5.Text, out quantity) || quantity < 0)
                {
                    MessageBox.Show("Invalid quantity value. Please enter a non-negative integer.");
                    return;
                }

                // Check if the entered quantity is available in the PRODUCT1 table
                string checkQuantityQuery = "SELECT QUANTITY FROM PRODUCT1 WHERE PRODUCT_NAME = :productName";
                using (OracleCommand checkQuantityCommand = new OracleCommand(checkQuantityQuery, con))
                {
                    checkQuantityCommand.Parameters.Add(new OracleParameter(":productName", productName));

                    try
                    {
                        object result = checkQuantityCommand.ExecuteScalar();
                        if (result != null)
                        {
                            int availableQuantity = Convert.ToInt32(result);
                            // Check if the entered quantity exceeds the available quantity
                            if (quantity > availableQuantity)
                            {
                                MessageBox.Show("Insufficient quantity available for the selected product.");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No product found with the specified name.");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error checking product quantity: " + ex.Message);
                        return;
                    }
                }
            }

            // Check if the product with the given productID already exists in the cart
            string checkProductExistsQuery = "SELECT COUNT(*) FROM CART WHERE PRODUCT_ID = :productID AND CUSTOMER_ID = :customerID";
            using (OracleCommand checkProductExistsCommand = new OracleCommand(checkProductExistsQuery, con))
            {
                checkProductExistsCommand.Parameters.Add(new OracleParameter(":productID", productID));
                checkProductExistsCommand.Parameters.Add(new OracleParameter(":customerID", customerID));

                try
                {
                    int productCount = Convert.ToInt32(checkProductExistsCommand.ExecuteScalar());
                    if (productCount > 0)
                    {
                        MessageBox.Show("Product with the given ID already exists in the cart for the specified customer.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking product existence in cart: " + ex.Message);
                    return;
                }
            }

            MessageBox.Show("Validation successful. Proceed with adding item to cart.");

            // Rest of the code to insert the item into the cart

            string insertQuery = "INSERT INTO CART (PRODUCT_NAME, PRODUCT_CATEGORY, QUANTITY, PRODUCT_ID, CUSTOMER_ID) " +
                          "VALUES (:productName, :categoryName, :quantity, :productID, :customerID)";

            using (OracleCommand command = new OracleCommand(insertQuery, con))
            {
                command.Parameters.Add(new OracleParameter(":productName", productName));
                command.Parameters.Add(new OracleParameter(":categoryName", categoryName));
                command.Parameters.Add(new OracleParameter(":quantity", quantity));
                command.Parameters.Add(new OracleParameter(":productID", productID));
                command.Parameters.Add(new OracleParameter(":customerID", customerID));

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item added to cart successfully.");
                        CUSTOMER_HOME C = new CUSTOMER_HOME();
                        C.Show();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add item to cart.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding item to cart: " + ex.Message);
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (comboBox2.SelectedItem != null)
            //    {
            //        string selectedProduct = comboBox2.SelectedItem.ToString();
            //        string query = "SELECT PRICE FROM PRODUCT1 WHERE PRODUCT_NAME = :productName";

            //        using (OracleCommand command = new OracleCommand(query, con))
            //        {
            //            command.Parameters.Add(new OracleParameter(":productName", selectedProduct));

            //            object priceObj = command.ExecuteScalar();
            //            if (priceObj != null)
            //            {
            //                textBox4.Text = priceObj.ToString();
            //            }
            //            else
            //            {
            //                MessageBox.Show("No price found for the selected product.");
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = comboBox3.SelectedItem.ToString();

            // Clear the existing items in comboBox2
            comboBox2.Items.Clear();

            // Build the SQL query based on the selected price filter
            string query = "SELECT PRODUCT_NAME FROM PRODUCT1 ";

            switch (selectedFilter)
            {
                case "HIGH TO LOW":
                    query += "ORDER BY PRICE DESC";
                    break;
                case "LOW TO HIGH":
                    query += "ORDER BY PRICE ASC";
                    break;
                case "AVERAGE":
                    query += "ORDER BY PRICE";
                    break;
            }

            // Execute the query and populate comboBox2 with the sorted product names
            using (OracleCommand command = new OracleCommand(query, con))
            {
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader["PRODUCT_NAME"].ToString());
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
//           string selectedProductID = textBox2.Text;
//string selectedProductName = comboBox2.SelectedItem.ToString();

//string selectQuery = "SELECT QUANTITY FROM PRODUCT1 WHERE PRODUCT_ID = :productID AND PRODUCT_NAME = :productName";

//using (OracleCommand command = new OracleCommand(selectQuery, con))
//{
//    command.Parameters.Add(new OracleParameter(":productID", selectedProductID));
//    command.Parameters.Add(new OracleParameter(":productName", selectedProductName));

//    try
//    {
//        textBox1.Text = ""; // Clear the text box before retrieving the quantity

//        using (OracleDataReader reader = command.ExecuteReader())
//        {
//            if (reader.Read())
//            {
//                int quantity = reader.GetInt32(0);
//                textBox1.Text = quantity.ToString();
//            }
//            else
//            {
//                textBox1.Text = "0";
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show("Error retrieving quantity: " + ex.Message);
//    }
//}

            }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Hide();
            CUSTOMER_HOME C = new CUSTOMER_HOME();
            C.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }

    }
    

