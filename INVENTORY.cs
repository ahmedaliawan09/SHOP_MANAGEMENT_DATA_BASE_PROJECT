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
    public partial class INVENTORY : Form
    {
        public static string pro_id = "";
        OracleConnection con;
        public INVENTORY()
        {

            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);

        }

        private void INVENTORY_Load(object sender, EventArgs e)
        {
            con.Open();




            string query = "SELECT RACK_ID, RACK_NO,DESCRIPTION FROM RACKS";
            string queryY = "SELECT PRODUCT_ID,QUANTITY,BARCODE,PRODUCT_NAME,CATEGORY_ID,PRICE,VENDOR_ID,DESCRIPTION FROM PRODUCT";
            OracleCommand command = new OracleCommand(query, con);
            OracleCommand command1 = new OracleCommand(queryY, con);
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            OracleDataAdapter adapterR = new OracleDataAdapter(command1);
            DataSet dataSet = new DataSet();
            DataSet dataSet1 = new DataSet();
            adapter.Fill(dataSet, "RACKS");
            adapterR.Fill(dataSet1, "PRODUCT");
            dataGridView1.DataSource = dataSet.Tables["RACKS"];
            dataGridView2.DataSource = dataSet1.Tables["PRODUCT"];

            string randomNum = Generate().ToString();
            textBox1.Text = randomNum;
            textBox1.ReadOnly = true;


        }
        public static int Generate()
        {
            Random rand = new Random();
            int number;

            do
            {
                // Generate a random number between 10 and 99 (2 digits)
                number = rand.Next(10, 100);
            }
            while (number < 10 || number >= 100);

            return number;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }

            //int categoryID;
            //if (!int.TryParse(textBox4.Text, out categoryID))
            //{
            //    MessageBox.Show("Please enter a valid Category ID");
            //    return;
            //}

            //// Check if the Category ID exists in the Category table
            //OracleCommand checkCategory = con.CreateCommand();
            //checkCategory.CommandText = "SELECT COUNT(*) FROM CATEGORY WHERE CATEGORY_ID = :categoryID";
            //checkCategory.Parameters.Add(":categoryID", categoryID);
            //int categoryCount = Convert.ToInt32(checkCategory.ExecuteScalar());

            //if (categoryCount == 0)
            //{
            //    MessageBox.Show("Invalid Category ID");
            //    return;
            //}

            OracleCommand insertEmp = con.CreateCommand();
            insertEmp.CommandText = "INSERT INTO RACKS (RACK_ID, RACK_NO, DESCRIPTION) VALUES (" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "')";
            insertEmp.CommandType = CommandType.Text;
            int rows = insertEmp.ExecuteNonQuery();

            if (rows > 0)
            {
                MessageBox.Show("Successfully INSERTED THE DATA");
                ADD_PRODUCT a = new ADD_PRODUCT();
                a.Show();
            }
            else
            {
                MessageBox.Show("Failed to insert data");
            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{


            //    string selectedCategory = comboBox1.SelectedItem.ToString();
            //    string query = "SELECT PRODUCT_ID FROM PRODUCT WHERE CATEGORY_ID = (SELECT CATEGORY_ID FROM CATEGORY WHERE CATEGORY_NAME = :categoryName)";
            //     using (OracleCommand command = new OracleCommand(query, con))
            //    {
            //        command.Parameters.Add(new OracleParameter(":categoryName", selectedCategory));


            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            string selectedProductID = textBox4.Text;
            int price = int.Parse(textBox5.Text);

            // Check if the selected product exists in the PRODUCT2 table
            string checkProductQuery = "SELECT COUNT(*) FROM PRODUCT WHERE PRODUCT_ID = :productID";
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

            // Check if the item already exists in the PRODUCT1 table
            string checkItemQuery = "SELECT COUNT(*) FROM PRODUCT1 WHERE PRODUCT_ID = :productID";
            using (OracleCommand checkItemCommand = new OracleCommand(checkItemQuery, con))
            {
                checkItemCommand.Parameters.Add(new OracleParameter(":productID", selectedProductID));
                int itemCount = Convert.ToInt32(checkItemCommand.ExecuteScalar());

                if (itemCount > 0)
                {
                    MessageBox.Show("Item already exists in PRODUCT1 table");
                    return;
                }
            }

            // Insert data into PRODUCT1 table
            string insertQuery = "INSERT INTO PRODUCT1 (PRODUCT_ID, QUANTITY, BARCODE, PRODUCT_NAME, CATEGORY_ID, PRICE, VENDOR_ID, DESCRIPTION) " +
                                 "SELECT :productID, QUANTITY, BARCODE, PRODUCT_NAME, CATEGORY_ID, :price, VENDOR_ID, DESCRIPTION " +
                                 "FROM PRODUCT " +
                                 "WHERE PRODUCT_ID = :productID";
            using (OracleCommand insertCommand = new OracleCommand(insertQuery, con))
            {
                insertCommand.Parameters.Add(new OracleParameter(":productID", selectedProductID));
                insertCommand.Parameters.Add(new OracleParameter(":quantity", price));

                try
                {
                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item added successfully");

                        // Delete the purchased item from PRODUCT2 table
                        // Refresh the data grid view        
                    }
                    else
                    {
                        MessageBox.Show("Failed to add item");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 F = new Form2();
            F.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }


}
