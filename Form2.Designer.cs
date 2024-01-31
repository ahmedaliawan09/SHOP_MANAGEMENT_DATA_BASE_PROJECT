
namespace SHOP_SYSTEM
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pRODUCTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pRODUCTSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iNVENTORYRECORDSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rACKSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pRODUCTSToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cUSTOMERACCOUNTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vENDORToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEPORTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pRODUCTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sALESToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bSHEETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pRODUCTToolStripMenuItem,
            this.pRODUCTSToolStripMenuItem1,
            this.iNVENTORYRECORDSToolStripMenuItem,
            this.cUSTOMERACCOUNTToolStripMenuItem,
            this.vENDORToolStripMenuItem,
            this.rEPORTSToolStripMenuItem,
            this.bSHEETToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(928, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // pRODUCTToolStripMenuItem
            // 
            this.pRODUCTToolStripMenuItem.Name = "pRODUCTToolStripMenuItem";
            this.pRODUCTToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.pRODUCTToolStripMenuItem.Text = "ADD CATEGORY";
            this.pRODUCTToolStripMenuItem.Click += new System.EventHandler(this.pRODUCTToolStripMenuItem_Click);
            // 
            // pRODUCTSToolStripMenuItem1
            // 
            this.pRODUCTSToolStripMenuItem1.Name = "pRODUCTSToolStripMenuItem1";
            this.pRODUCTSToolStripMenuItem1.Size = new System.Drawing.Size(140, 20);
            this.pRODUCTSToolStripMenuItem1.Text = "PURCHASE PRODUCTS";
            this.pRODUCTSToolStripMenuItem1.Click += new System.EventHandler(this.pRODUCTSToolStripMenuItem1_Click);
            // 
            // iNVENTORYRECORDSToolStripMenuItem
            // 
            this.iNVENTORYRECORDSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rACKSToolStripMenuItem,
            this.pRODUCTSToolStripMenuItem2});
            this.iNVENTORYRECORDSToolStripMenuItem.Name = "iNVENTORYRECORDSToolStripMenuItem";
            this.iNVENTORYRECORDSToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.iNVENTORYRECORDSToolStripMenuItem.Text = "INVENTORY ";
            this.iNVENTORYRECORDSToolStripMenuItem.Click += new System.EventHandler(this.iNVENTORYRECORDSToolStripMenuItem_Click);
            // 
            // rACKSToolStripMenuItem
            // 
            this.rACKSToolStripMenuItem.Name = "rACKSToolStripMenuItem";
            this.rACKSToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.rACKSToolStripMenuItem.Text = "RACKS";
            this.rACKSToolStripMenuItem.Click += new System.EventHandler(this.rACKSToolStripMenuItem_Click);
            // 
            // pRODUCTSToolStripMenuItem2
            // 
            this.pRODUCTSToolStripMenuItem2.Name = "pRODUCTSToolStripMenuItem2";
            this.pRODUCTSToolStripMenuItem2.Size = new System.Drawing.Size(195, 22);
            this.pRODUCTSToolStripMenuItem2.Text = "PURCHASE PRODUCTS";
            this.pRODUCTSToolStripMenuItem2.Click += new System.EventHandler(this.pRODUCTSToolStripMenuItem2_Click);
            // 
            // cUSTOMERACCOUNTToolStripMenuItem
            // 
            this.cUSTOMERACCOUNTToolStripMenuItem.Name = "cUSTOMERACCOUNTToolStripMenuItem";
            this.cUSTOMERACCOUNTToolStripMenuItem.Size = new System.Drawing.Size(138, 20);
            this.cUSTOMERACCOUNTToolStripMenuItem.Text = "CUSTOMER ACCOUNT";
            this.cUSTOMERACCOUNTToolStripMenuItem.Click += new System.EventHandler(this.cUSTOMERACCOUNTToolStripMenuItem_Click);
            // 
            // vENDORToolStripMenuItem
            // 
            this.vENDORToolStripMenuItem.Name = "vENDORToolStripMenuItem";
            this.vENDORToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.vENDORToolStripMenuItem.Text = "VENDOR ACCOUNT";
            this.vENDORToolStripMenuItem.Click += new System.EventHandler(this.vENDORToolStripMenuItem_Click);
            // 
            // rEPORTSToolStripMenuItem
            // 
            this.rEPORTSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pRODUCTSToolStripMenuItem,
            this.sALESToolStripMenuItem1});
            this.rEPORTSToolStripMenuItem.Name = "rEPORTSToolStripMenuItem";
            this.rEPORTSToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.rEPORTSToolStripMenuItem.Text = "REPORTS";
            this.rEPORTSToolStripMenuItem.Click += new System.EventHandler(this.rEPORTSToolStripMenuItem_Click);
            // 
            // pRODUCTSToolStripMenuItem
            // 
            this.pRODUCTSToolStripMenuItem.Name = "pRODUCTSToolStripMenuItem";
            this.pRODUCTSToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.pRODUCTSToolStripMenuItem.Text = "PRODUCTS PURCHASE";
            this.pRODUCTSToolStripMenuItem.Click += new System.EventHandler(this.pRODUCTSToolStripMenuItem_Click);
            // 
            // sALESToolStripMenuItem1
            // 
            this.sALESToolStripMenuItem1.Name = "sALESToolStripMenuItem1";
            this.sALESToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.sALESToolStripMenuItem1.Text = " PRODUCTS SALES";
            this.sALESToolStripMenuItem1.Click += new System.EventHandler(this.sALESToolStripMenuItem1_Click);
            // 
            // entityCommand1
            // 
            this.entityCommand1.CommandTimeout = 0;
            this.entityCommand1.CommandTree = null;
            this.entityCommand1.Connection = null;
            this.entityCommand1.EnablePlanCaching = true;
            this.entityCommand1.Transaction = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "BACK";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(872, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "HOME";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // bSHEETToolStripMenuItem
            // 
            this.bSHEETToolStripMenuItem.Name = "bSHEETToolStripMenuItem";
            this.bSHEETToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.bSHEETToolStripMenuItem.Text = "B.SHEET";
            this.bSHEETToolStripMenuItem.Click += new System.EventHandler(this.bSHEETToolStripMenuItem_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(928, 458);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pRODUCTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEPORTSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cUSTOMERACCOUNTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pRODUCTSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sALESToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iNVENTORYRECORDSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vENDORToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pRODUCTSToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rACKSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pRODUCTSToolStripMenuItem2;
        private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem bSHEETToolStripMenuItem;
    }
}