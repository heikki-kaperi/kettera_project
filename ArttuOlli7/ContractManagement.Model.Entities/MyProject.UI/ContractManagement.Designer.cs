namespace MyProject.UI
{
    partial class ContractManagement
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button createContractBtn;
        private System.Windows.Forms.Button viewMyContractsBtn;
        private System.Windows.Forms.Button addBlockBtn;
        private System.Windows.Forms.Button removeBlockBtn;
        private System.Windows.Forms.Button editBlockBtn;
        private System.Windows.Forms.Button inviteInternalBtn;
        private System.Windows.Forms.Button inviteExternalBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Label titleLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.createContractBtn = new System.Windows.Forms.Button();
            this.viewMyContractsBtn = new System.Windows.Forms.Button();
            this.addBlockBtn = new System.Windows.Forms.Button();
            this.removeBlockBtn = new System.Windows.Forms.Button();
            this.editBlockBtn = new System.Windows.Forms.Button();
            this.inviteInternalBtn = new System.Windows.Forms.Button();
            this.inviteExternalBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // titleLabel
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(80, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(200, 16);
            this.titleLabel.Text = "=== CONTRACT MANAGEMENT ===";

            // createContractBtn
            this.createContractBtn.Location = new System.Drawing.Point(50, 60);
            this.createContractBtn.Size = new System.Drawing.Size(260, 30);
            this.createContractBtn.Text = "1. Create New Contract";
            this.createContractBtn.Click += new System.EventHandler(this.createContractBtn_Click);

            // viewMyContractsBtn
            this.viewMyContractsBtn.Location = new System.Drawing.Point(50, 100);
            this.viewMyContractsBtn.Size = new System.Drawing.Size(260, 30);
            this.viewMyContractsBtn.Text = "2. View My Contracts";
            this.viewMyContractsBtn.Click += new System.EventHandler(this.viewMyContractsBtn_Click);

            // addBlockBtn
            this.addBlockBtn.Location = new System.Drawing.Point(50, 140);
            this.addBlockBtn.Size = new System.Drawing.Size(260, 30);
            this.addBlockBtn.Text = "3. Add Block to Contract";
            this.addBlockBtn.Click += new System.EventHandler(this.addBlockBtn_Click);

            // removeBlockBtn
            this.removeBlockBtn.Location = new System.Drawing.Point(50, 180);
            this.removeBlockBtn.Size = new System.Drawing.Size(260, 30);
            this.removeBlockBtn.Text = "4. Remove Block from Contract";
            this.removeBlockBtn.Click += new System.EventHandler(this.removeBlockBtn_Click);

            // editBlockBtn
            this.editBlockBtn.Location = new System.Drawing.Point(50, 220);
            this.editBlockBtn.Size = new System.Drawing.Size(260, 30);
            this.editBlockBtn.Text = "5. Edit Block in Contract";
            this.editBlockBtn.Click += new System.EventHandler(this.editBlockBtn_Click);

            // inviteInternalBtn
            this.inviteInternalBtn.Location = new System.Drawing.Point(50, 260);
            this.inviteInternalBtn.Size = new System.Drawing.Size(260, 30);
            this.inviteInternalBtn.Text = "6. Invite Internal Reviewers";
            this.inviteInternalBtn.Click += new System.EventHandler(this.inviteInternalBtn_Click);

            // inviteExternalBtn
            this.inviteExternalBtn.Location = new System.Drawing.Point(50, 300);
            this.inviteExternalBtn.Size = new System.Drawing.Size(260, 30);
            this.inviteExternalBtn.Text = "7. Invite External Users";
            this.inviteExternalBtn.Click += new System.EventHandler(this.inviteExternalBtn_Click);

            // backBtn
            this.backBtn.Location = new System.Drawing.Point(50, 340);
            this.backBtn.Size = new System.Drawing.Size(260, 30);
            this.backBtn.Text = "0. Back";
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);

            // ContractManagement
            this.ClientSize = new System.Drawing.Size(360, 400);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.createContractBtn);
            this.Controls.Add(this.viewMyContractsBtn);
            this.Controls.Add(this.addBlockBtn);
            this.Controls.Add(this.removeBlockBtn);
            this.Controls.Add(this.editBlockBtn);
            this.Controls.Add(this.inviteInternalBtn);
            this.Controls.Add(this.inviteExternalBtn);
            this.Controls.Add(this.backBtn);
            this.Name = "ContractManagement";
            this.Text = "Contract Management";
            this.Load += new System.EventHandler(this.ContractManagement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
