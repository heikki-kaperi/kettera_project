using System.Windows.Forms;

namespace MyProject.UI
{
    partial class ViewMyContractsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private ListView listViewContracts;
        private Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.listViewContracts = new ListView();
            this.btnBack = new Button();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Text = "=== My Contracts ===";
            this.lblTitle.Location = new System.Drawing.Point(90, 20);

            // 
            // listViewContracts
            // 
            this.listViewContracts.Location = new System.Drawing.Point(20, 60);
            this.listViewContracts.Size = new System.Drawing.Size(460, 300);
            this.listViewContracts.View = View.Details;
            this.listViewContracts.FullRowSelect = true;
            this.listViewContracts.GridLines = true;
            this.listViewContracts.Columns.Add("ID", 50);
            this.listViewContracts.Columns.Add("Name", 180);
            this.listViewContracts.Columns.Add("Creator ID", 80);
            this.listViewContracts.Columns.Add("Created Date", 120);

            // 
            // btnBack
            // 
            this.btnBack.Text = "Back";
            this.btnBack.Location = new System.Drawing.Point(380, 370);
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // 
            // ViewMyContractsForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 420);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.listViewContracts);
            this.Controls.Add(this.btnBack);
            this.Name = "ViewMyContractsForm";
            this.Text = "My Contracts";
            this.Load += new System.EventHandler(this.ViewMyContractsForm_Load);
        }
    }
}
