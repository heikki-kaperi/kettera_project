namespace MyProject.UI
{
    partial class Internal
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBlockCategory;
        private System.Windows.Forms.Button contractManagementBtn;
        private System.Windows.Forms.Button myContractsBtn;
        private System.Windows.Forms.Button viewContractDetailsBtn;
        private System.Windows.Forms.Button commentContractBtn;
        private System.Windows.Forms.Button approveContractBtn;
        private System.Windows.Forms.Button logoutBtn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnBlockCategory = new System.Windows.Forms.Button();
            this.contractManagementBtn = new System.Windows.Forms.Button();
            this.myContractsBtn = new System.Windows.Forms.Button();
            this.viewContractDetailsBtn = new System.Windows.Forms.Button();
            this.commentContractBtn = new System.Windows.Forms.Button();
            this.approveContractBtn = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(280, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 16);
            this.label1.Text = "INTERNAL USER MENU";

            // btnBlockCategory
            this.btnBlockCategory.Location = new System.Drawing.Point(280, 100);
            this.btnBlockCategory.Size = new System.Drawing.Size(240, 30);
            this.btnBlockCategory.Text = "Block & Category Management";
            this.btnBlockCategory.Click += new System.EventHandler(this.blockCategoryBtn_Click);

            // contractManagementBtn
            this.contractManagementBtn.Location = new System.Drawing.Point(280, 140);
            this.contractManagementBtn.Size = new System.Drawing.Size(240, 30);
            this.contractManagementBtn.Text = "Contract Management";
            this.contractManagementBtn.Click += new System.EventHandler(this.contractManagementBtn_Click);

            // myContractsBtn
            this.myContractsBtn.Location = new System.Drawing.Point(280, 180);
            this.myContractsBtn.Size = new System.Drawing.Size(240, 30);
            this.myContractsBtn.Text = "My Contracts (Reviewer)";
            this.myContractsBtn.Click += new System.EventHandler(this.myContractsBtn_Click);

            // viewContractDetailsBtn
            this.viewContractDetailsBtn.Location = new System.Drawing.Point(280, 220);
            this.viewContractDetailsBtn.Size = new System.Drawing.Size(240, 30);
            this.viewContractDetailsBtn.Text = "View Contract Details";
            this.viewContractDetailsBtn.Click += new System.EventHandler(this.openContractDetailsBtn_Click);

            // commentContractBtn
            this.commentContractBtn.Location = new System.Drawing.Point(280, 260);
            this.commentContractBtn.Size = new System.Drawing.Size(240, 30);
            this.commentContractBtn.Text = "Comment on Contract";
            this.commentContractBtn.Click += new System.EventHandler(this.commentContractBtn_Click);

            // approveContractBtn
            this.approveContractBtn.Location = new System.Drawing.Point(280, 300);
            this.approveContractBtn.Size = new System.Drawing.Size(240, 30);
            this.approveContractBtn.Text = "Approve Contract";
            this.approveContractBtn.Click += new System.EventHandler(this.approveContractBtn_Click);

            // logoutBtn
            this.logoutBtn.Location = new System.Drawing.Point(330, 350);
            this.logoutBtn.Size = new System.Drawing.Size(120, 30);
            this.logoutBtn.Text = "Logout";
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);

            // Internal Form
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.approveContractBtn);
            this.Controls.Add(this.commentContractBtn);
            this.Controls.Add(this.viewContractDetailsBtn);
            this.Controls.Add(this.myContractsBtn);
            this.Controls.Add(this.contractManagementBtn);
            this.Controls.Add(this.btnBlockCategory);
            this.Controls.Add(this.label1);
            this.Name = "Internal";
            this.Text = "Internal User Menu";
            this.Load += new System.EventHandler(this.Internal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
