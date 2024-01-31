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
    public partial class PRODUCTS_REPOTS : Form
    {
        OracleConnection con;
        public PRODUCTS_REPOTS()
        {
            InitializeComponent(); 
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
            con.Open();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PRODUCTS_REPOTS_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
            // Create a DataTable to hold the results
            DataTable dataTable = new DataTable();
             
            string query = "SELECT * FROM SALES WHERE VEND_ID IS NOT NULL ";

            // Create the OracleCommand
            OracleCommand command = con.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            // Create the OracleDataAdapter
            OracleDataAdapter adapter = new OracleDataAdapter(command);

            try
            {
                // Fill the DataTable with the data from the database
                adapter.Fill(dataTable);

                // Bind the DataTable to the DataGridView or display the data in any other way
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while retrieving sales data: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT SUM(DEBIT) FROM SALES";

            // Create the OracleCommand
            OracleCommand command = con.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            try
            {
                // Execute the query and get the sum value
                object result = command.ExecuteScalar();

                // Check if the result is not null
                if (result != null && result != DBNull.Value)
                {
                    // Convert the result to the desired data type and display it in the TextBox
                    textBox1.Text = result.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while calculating the sum: " + ex.Message);
            }
        }
    }
}
