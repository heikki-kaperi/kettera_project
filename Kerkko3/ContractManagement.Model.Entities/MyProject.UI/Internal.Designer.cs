namespace MyProject.UI
{
    partial class Internal
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBlockCategory = new System.Windows.Forms.Button();
            this.contractManagementBtn = new System.Windows.Forms.Button();
            this.myContractsBtn = new System.Windows.Forms.Button();
            this.viewContractDetailsBtn = new System.Windows.Forms.Button();
            this.commentContractBtn = new System.Windows.Forms.Button();
            this.approveContractBtn = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(309, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "INTERNAL USER MENU";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 1;
            // 
            // btnBlockCategory
            // 
            this.btnBlockCategory.Location = new System.Drawing.Point(312, 132);
            this.btnBlockCategory.Name = "btnBlockCategory";
            this.btnBlockCategory.Size = new System.Drawing.Size(223, 23);
            this.btnBlockCategory.TabIndex = 2;
            this.btnBlockCategory.Text = "Block And Category Management";
            this.btnBlockCategory.UseVisualStyleBackColor = true;
            this.btnBlockCategory.Click += new System.EventHandler(this.blockCategoryBtn_Click);
            // 
            // contractManagementBtn
            // 
            this.contractManagementBtn.Location = new System.Drawing.Point(312, 161);
            this.contractManagementBtn.Name = "contractManagementBtn";
            this.contractManagementBtn.Size = new System.Drawing.Size(152, 23);
            this.contractManagementBtn.TabIndex = 3;
            this.contractManagementBtn.Text = "Contract Management";
            this.contractManagementBtn.UseVisualStyleBackColor = true;
            this.contractManagementBtn.Click += new System.EventHandler(this.contractManagementBtn_Click);
            // 
            // myContractsBtn
            // 
            this.myContractsBtn.Location = new System.Drawing.Point(312, 191);
            this.myContractsBtn.Name = "myContractsBtn";
            this.myContractsBtn.Size = new System.Drawing.Size(181, 23);
            this.myContractsBtn.TabIndex = 4;
            this.myContractsBtn.Text = "My Contracts (as Reviewer)";
            this.myContractsBtn.UseVisualStyleBackColor = true;
            this.myContractsBtn.Click += new System.EventHandler(this.myContractsBtn_Click);
            // 
            // viewContractDetailsBtn
            // 
            this.viewContractDetailsBtn.Location = new System.Drawing.Point(315, 221);
            this.viewContractDetailsBtn.Name = "viewContractDetailsBtn";
            this.viewContractDetailsBtn.Size = new System.Drawing.Size(149, 23);
            this.viewContractDetailsBtn.TabIndex = 5;
            this.viewContractDetailsBtn.Text = "View Contract Details";
            this.viewContractDetailsBtn.UseVisualStyleBackColor = true;
            this.viewContractDetailsBtn.Click += new System.EventHandler(this.viewContractDetailsBtn_Click);
            // 
            // commentContractBtn
            // 
            this.commentContractBtn.Location = new System.Drawing.Point(315, 251);
            this.commentContractBtn.Name = "commentContractBtn";
            this.commentContractBtn.Size = new System.Drawing.Size(149, 23);
            this.commentContractBtn.TabIndex = 6;
            this.commentContractBtn.Text = "Comment on Contract";
            this.commentContractBtn.UseVisualStyleBackColor = true;
            this.commentContractBtn.Click += new System.EventHandler(this.commentContractBtn_Click);
            // 
            // approveContractBtn
            // 
            this.approveContractBtn.Location = new System.Drawing.Point(315, 281);
            this.approveContractBtn.Name = "approveContractBtn";
            this.approveContractBtn.Size = new System.Drawing.Size(122, 23);
            this.approveContractBtn.TabIndex = 7;
            this.approveContractBtn.Text = "Approve Contract";
            this.approveContractBtn.UseVisualStyleBackColor = true;
            this.approveContractBtn.Click += new System.EventHandler(this.approveContractBtn_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.Location = new System.Drawing.Point(315, 311);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(60, 23);
            this.logoutBtn.TabIndex = 8;
            this.logoutBtn.Text = "Logout";
            this.logoutBtn.UseVisualStyleBackColor = true;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // Internal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.approveContractBtn);
            this.Controls.Add(this.commentContractBtn);
            this.Controls.Add(this.viewContractDetailsBtn);
            this.Controls.Add(this.myContractsBtn);
            this.Controls.Add(this.contractManagementBtn);
            this.Controls.Add(this.btnBlockCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Internal";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Internal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBlockCategory;
        private System.Windows.Forms.Button contractManagementBtn;
        private System.Windows.Forms.Button myContractsBtn;
        private System.Windows.Forms.Button viewContractDetailsBtn;
        private System.Windows.Forms.Button commentContractBtn;
        private System.Windows.Forms.Button approveContractBtn;
        private System.Windows.Forms.Button logoutBtn;
    }
}