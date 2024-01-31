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
    public partial class ADD_PRODUCT : Form
    {
        OracleConnection con;
        public ADD_PRODUCT()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private static readonly Random rand = new Random();

        public static int Generate()
        {
            Random rand = new Random();
            int number;

            do
            {
                // Generate a random number between 100 and 999 (3 digits)
                number = rand.Next(100, 1000);
            }
            while (number < 100 || number >= 1000);

            return number;
        }

        private void ADD_PRODUCT_Load(object sender, EventArgs e)
        {
            textBox7.Text = v_sign_in.venid;

            string query = "SELECT CATEGORY_ID, CATEGORY_NAME FROM Category";
            OracleCommand command = new OracleCommand(query, con);

            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Category");

            dataGridView1.DataSource = dataSet.Tables["Category"];
            string randomNum = Generate().ToString();
            textBox1.Text = randomNum;
            textBox1.ReadOnly = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if all fields are filled
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text)
                || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text)
                || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text)
                || string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            con.Open();

            // Check if category ID exists in the category table
            OracleCommand checkCategoryId = con.CreateCommand();
            checkCategoryId.CommandText = "SELECT COUNT(*) FROM CATEGORY WHERE CATEGORY_ID = " + textBox2.Text;
            int count = Convert.ToInt32(checkCategoryId.ExecuteScalar());
            if (count == 0)
            {
                MessageBox.Show("Category ID is incorrect");
                con.Close();
                return;
            }

            // Check if barcode already exists in the product table
            OracleCommand checkBarcode = con.CreateCommand();
            checkBarcode.CommandText = "SELECT COUNT(*) FROM PRODUCT2 WHERE BARCODE = " + textBox5.Text;
            count = Convert.ToInt32(checkBarcode.ExecuteScalar());
            if (count > 0)
            {
                MessageBox.Show("Barcode already exists");
                con.Close();
                return;
            }

            // Insert data into the product table
            OracleCommand insertEmp = con.CreateCommand();
            insertEmp.CommandText = "INSERT INTO PRODUCT1(PRODUCT_ID, QUANTITY, BARCODE, PRODUCT_NAME, CATEGORY_ID, PRICE, VENDOR_ID, DESCRIPTION) VALUES (" + textBox1.Text + "," + textBox4.Text + "," + textBox5.Text + ", '" + textBox3.Text + "'," + textBox2.Text + ",'" + textBox6.Text + "'," + textBox7.Text + ",'" + textBox8.Text + "')";
            insertEmp.CommandType = CommandType.Text;
            int rows = insertEmp.ExecuteNonQuery();

            if (rows > 0)
            {
                MessageBox.Show("Successfully inserted the data");
                this.Hide();
                ADD_PRODUCT a = new ADD_PRODUCT();
                a.Show();
            }
            else
            {
                MessageBox.Show("Failed to insert the data");
            }

            con.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            vendor F = new vendor();
            F.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}



 








