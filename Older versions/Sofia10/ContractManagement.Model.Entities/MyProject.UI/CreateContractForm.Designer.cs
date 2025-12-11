using System.Windows.Forms;

namespace MyProject.UI
{
    partial class CreateContractForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblContractName;
        private TextBox txtContractName;
        private Button btnCreate;
        private Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblContractName = new System.Windows.Forms.Label();
            this.txtContractName = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(80, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(177, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "=== Create New Contract ===";
            // 
            // lblContractName
            // 
            this.lblContractName.AutoSize = true;
            this.lblContractName.Location = new System.Drawing.Point(50, 60);
            this.lblContractName.Name = "lblContractName";
            this.lblContractName.Size = new System.Drawing.Size(99, 16);
            this.lblContractName.TabIndex = 1;
            this.lblContractName.Text = "Contract Name:";
            // 
            // txtContractName
            // 
            this.txtContractName.Location = new System.Drawing.Point(50, 80);
            this.txtContractName.Name = "txtContractName";
            this.txtContractName.Size = new System.Drawing.Size(280, 22);
            this.txtContractName.TabIndex = 2;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(50, 120);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(120, 30);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(210, 120);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(120, 30);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // CreateContractForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 180);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblContractName);
            this.Controls.Add(this.txtContractName);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnBack);
            this.Name = "CreateContractForm";
            this.Text = "Create Contract";
            this.Load += new System.EventHandler(this.CreateContractForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
