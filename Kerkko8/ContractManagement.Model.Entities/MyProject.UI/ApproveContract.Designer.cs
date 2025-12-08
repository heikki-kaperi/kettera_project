namespace MyProject.UI
{
    partial class ApproveContract
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbContracts;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.TextBox txtContractName;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbContracts = new System.Windows.Forms.ComboBox();
            this.lblContract = new System.Windows.Forms.Label();
            this.txtContractName = new System.Windows.Forms.TextBox();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // cbContracts
            this.cbContracts.Location = new System.Drawing.Point(20, 40);
            this.cbContracts.Size = new System.Drawing.Size(300, 24);
            this.cbContracts.SelectedIndexChanged += new System.EventHandler(this.cbContracts_SelectedIndexChanged);

            // lblContract
            this.lblContract.AutoSize = true;
            this.lblContract.Location = new System.Drawing.Point(20, 20);
            this.lblContract.Text = "Select Contract:";

            // txtContractName
            this.txtContractName.Location = new System.Drawing.Point(20, 80);
            this.txtContractName.Size = new System.Drawing.Size(300, 22);
            this.txtContractName.ReadOnly = true;

            // btnApprove
            this.btnApprove.Location = new System.Drawing.Point(20, 120);
            this.btnApprove.Size = new System.Drawing.Size(100, 30);
            this.btnApprove.Text = "Approve";
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(140, 120);
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // ApproveContract Form
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.cbContracts);
            this.Controls.Add(this.lblContract);
            this.Controls.Add(this.txtContractName);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.btnBack);
            this.Name = "ApproveContract";
            this.Text = "Approve Contract";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
