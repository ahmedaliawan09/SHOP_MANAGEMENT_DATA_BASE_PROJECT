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
    public partial class bsheet : Form
    {
        OracleConnection con;
        public bsheet()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
            //con.Open();
        }

        private void bsheet_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
           

            try
            {
                con.Open(); // Open the connection to the Oracle database

                // Retrieve the balance sheet data from the "SALES" table
                string query = "SELECT SALE_DATE, CATEGORY_NAME, DEBIT, CREDIT FROM SALES";
                OracleCommand command = con.CreateCommand();
                command.CommandText = query;

                OracleDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    // Iterate through the data and add rows to the DataGridView
                    while (reader.Read())
                    {
                        DateTime? saleDate = reader.IsDBNull(reader.GetOrdinal("SALE_DATE")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("SALE_DATE"));
                        string categoryName = reader.IsDBNull(reader.GetOrdinal("CATEGORY_NAME")) ? string.Empty : reader.GetString(reader.GetOrdinal("CATEGORY_NAME"));
                        decimal? debit = reader.IsDBNull(reader.GetOrdinal("DEBIT")) ? null : (decimal?)reader.GetDecimal(reader.GetOrdinal("DEBIT"));
                        decimal? credit = reader.IsDBNull(reader.GetOrdinal("CREDIT")) ? null : (decimal?)reader.GetDecimal(reader.GetOrdinal("CREDIT"));

                        dataGridView1.Rows.Add(saleDate.HasValue ? saleDate.Value.ToString("yyyy-MM-dd") : string.Empty, categoryName, debit.HasValue ? debit.Value.ToString() : string.Empty, credit.HasValue ? credit.Value.ToString() : string.Empty);
                    }
                }
                else
                {
                    MessageBox.Show("No data found in the 'SALES' table.");
                }

                reader.Close();
                con.Close(); // Close the connection to the Oracle database
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Oracle Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while retrieving balance sheet data: " + ex.Message);
            }
        }
        
    }
}
