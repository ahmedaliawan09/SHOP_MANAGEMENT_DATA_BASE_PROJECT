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
    public partial class ADD_CATEGORY : Form
    {
        OracleConnection con;
        public ADD_CATEGORY()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ADD_CATEGORY_Load(object sender, EventArgs e)
        {
            con.Open();
            string query = "SELECT RACK_ID, DESCRIPTION FROM RACKS";
            OracleCommand command = new OracleCommand(query, con);

            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "RACKS");

            dataGridView1.DataSource = dataSet.Tables["RACKS"];
            dataGridView1.Columns["RACK_ID"].HeaderText = "Rack ID";  // Set column header text
            dataGridView1.Columns["DESCRIPTION"].HeaderText = "Description";  // Set column header text


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            int rackID;
            if (!int.TryParse(textBox4.Text, out rackID))
            {
                MessageBox.Show("Please enter a valid RACK_ID");
                return;
            }

            // Check if the Rack ID exists in the RACKS table
            OracleCommand checkRack = con.CreateCommand();
            checkRack.CommandText = "SELECT COUNT(*) FROM RACKS WHERE RACK_ID = :rackID";
            checkRack.Parameters.Add(new OracleParameter("rackID", rackID));
            int racksCount = Convert.ToInt32(checkRack.ExecuteScalar());

            if (racksCount == 0)
            {
                MessageBox.Show("Invalid RACK_ID");
                return;
            }

            // Check if the CATEGORY_ID already exists in the CATEGORY table
            OracleCommand checkCategory = con.CreateCommand();
            checkCategory.CommandText = "SELECT COUNT(*) FROM CATEGORY WHERE CATEGORY_ID = :categoryID";
            checkCategory.Parameters.Add(new OracleParameter("categoryID", textBox1.Text));
            int categoryCount = Convert.ToInt32(checkCategory.ExecuteScalar());

            if (categoryCount > 0)
            {
                MessageBox.Show("CATEGORY_ID already exists. Please enter another CATEGORY_ID");
                return;
            }

            OracleCommand insertCategory = con.CreateCommand();
            insertCategory.CommandText = "INSERT INTO CATEGORY (CATEGORY_ID, CATEGORY_NAME, DESCRIPTION, RACK_ID) VALUES (:id, :name, :description, :rackId)";
            insertCategory.CommandType = CommandType.Text;

            // Add parameter values
            insertCategory.Parameters.Add(new OracleParameter("id", textBox1.Text));
            insertCategory.Parameters.Add(new OracleParameter("name", textBox2.Text));
            insertCategory.Parameters.Add(new OracleParameter("description", textBox3.Text));
            insertCategory.Parameters.Add(new OracleParameter("rackId", textBox4.Text));

            int rows = insertCategory.ExecuteNonQuery();

            if (rows > 0)
            {
                MessageBox.Show("Successfully added");
                
            }
            else
            {
                MessageBox.Show("Failed to insert data");
            }

            con.Close();
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
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










//// Check if CATEGORY_ID already exists in the table
//OracleCommand checkId = con.CreateCommand();
//checkId.CommandText = "SELECT COUNT(*) FROM CATEGORY WHERE CATEGORY_ID = :id";
//checkId.CommandType = CommandType.Text;
//checkId.Parameters.Add(new OracleParameter("id", textBox1.Text));
//int count = Convert.ToInt32(checkId.ExecuteScalar());

//if (count > 0)
//{
//    MessageBox.Show("CATEGORY_ID already exists");
//}
//else
//{
//    int rows = insertEmp.ExecuteNonQuery();
//    if (rows > 0)
//    {
//        MessageBox.Show("SUCCESSFULLY CREATED THE ACCOUNT");
//        ADD_PRODUCT a = new ADD_PRODUCT();
//        a.Show();
//    }
//    else
//    {
//        MessageBox.Show("data insert kro larky");
//    }
//}
//con.Close();
