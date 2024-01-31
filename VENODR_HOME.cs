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
    public partial class VENODR_HOME : Form
    {
        OracleConnection con;
        public VENODR_HOME()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
            con.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
             
            ADD_PRODUCT a = new ADD_PRODUCT();
            a.Show();
        }

        private void VENODR_HOME_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            vendor_products v = new vendor_products();
            v.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
