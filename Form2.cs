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

    public partial class Form2 : Form
    {
        OracleConnection con;
        public Form2()
        {
            InitializeComponent();
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID = 21F9245; PASSWORD=123456789";
            con = new OracleConnection(conStr);
            con.Open();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

       
        private void pRODUCTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ADD_CATEGORY a2 = new ADD_CATEGORY();
            a2.Show();
        }


        private void sALESToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iNVENTORYRECORDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void cUSTOMERACCOUNTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            CUSTOMER_ACCOUNT c = new CUSTOMER_ACCOUNT();
            c.Show();
        }

        private void vENDORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            VENDOR_ACCOUNTS_MANAGEMENT vm = new VENDOR_ACCOUNTS_MANAGEMENT();
            vm.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pURCHASEDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ADD_PRODUCT A = new ADD_PRODUCT();
            A.Show();
        }

        private void sALEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CUSTOMER C = new CUSTOMER();
            C.Show();
        }

        private void rACKSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            INVENTORY V = new INVENTORY();
            V.Show();
        }

        private void pRODUCTSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PURCHASE_PRODUCT P = new PURCHASE_PRODUCT();
            P.Show();
        }

        private void pRODUCTSToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ADD_PRODUCT1 A = new ADD_PRODUCT1();
            A.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            CUSTOMER C = new CUSTOMER();
            C.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void sALESToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            REPORTS r = new REPORTS();
            r.Show();
        }

        private void rEPORTSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pRODUCTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PRODUCTS_REPOTS B = new PRODUCTS_REPOTS();
            B.Show();
        }

        private void bSHEETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bsheet b = new bsheet();
            b.Show();
        }
    }
}
