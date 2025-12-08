namespace MyProject.UI
{
    partial class ContractDetailsInternal
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbContracts;
        private System.Windows.Forms.Label lblSelectContract;
        private System.Windows.Forms.Label lblContractName;
        private System.Windows.Forms.TextBox txtContractName;
        private System.Windows.Forms.Label lblCreatorId;
        private System.Windows.Forms.TextBox txtCreatorId;
        private System.Windows.Forms.Label lblCreatedDate;
        private System.Windows.Forms.TextBox txtCreatedDate;
        private System.Windows.Forms.Label lblContractStatus;
        private System.Windows.Forms.TextBox txtContractStatus;
        private System.Windows.Forms.Label lblBlocks;
        private System.Windows.Forms.ListBox lstBlocks;
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
            this.lblSelectContract = new System.Windows.Forms.Label();
            this.lblContractName = new System.Windows.Forms.Label();
            this.txtContractName = new System.Windows.Forms.TextBox();
            this.lblCreatorId = new System.Windows.Forms.Label();
            this.txtCreatorId = new System.Windows.Forms.TextBox();
            this.lblCreatedDate = new System.Windows.Forms.Label();
            this.txtCreatedDate = new System.Windows.Forms.TextBox();
            this.lblContractStatus = new System.Windows.Forms.Label();
            this.txtContractStatus = new System.Windows.Forms.TextBox();
            this.lblBlocks = new System.Windows.Forms.Label();
            this.lstBlocks = new System.Windows.Forms.ListBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // cbContracts
            this.cbContracts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbContracts.Location = new System.Drawing.Point(150, 20);
            this.cbContracts.Size = new System.Drawing.Size(300, 24);
            this.cbContracts.SelectedIndexChanged += new System.EventHandler(this.cbContracts_SelectedIndexChanged);

            // lblSelectContract
            this.lblSelectContract.AutoSize = true;
            this.lblSelectContract.Location = new System.Drawing.Point(20, 23);
            this.lblSelectContract.Text = "Select Contract:";

            // lblContractName
            this.lblContractName.AutoSize = true;
            this.lblContractName.Location = new System.Drawing.Point(20, 60);
            this.lblContractName.Text = "Contract Name:";

            // txtContractName
            this.txtContractName.Location = new System.Drawing.Point(150, 57);
            this.txtContractName.Size = new System.Drawing.Size(300, 22);
            this.txtContractName.ReadOnly = true;

            // lblCreatorId
            this.lblCreatorId.AutoSize = true;
            this.lblCreatorId.Location = new System.Drawing.Point(20, 95);
            this.lblCreatorId.Text = "Creator ID:";

            // txtCreatorId
            this.txtCreatorId.Location = new System.Drawing.Point(150, 92);
            this.txtCreatorId.Size = new System.Drawing.Size(100, 22);
            this.txtCreatorId.ReadOnly = true;

            // lblCreatedDate
            this.lblCreatedDate.AutoSize = true;
            this.lblCreatedDate.Location = new System.Drawing.Point(20, 130);
            this.lblCreatedDate.Text = "Created Date:";

            // txtCreatedDate
            this.txtCreatedDate.Location = new System.Drawing.Point(150, 127);
            this.txtCreatedDate.Size = new System.Drawing.Size(150, 22);
            this.txtCreatedDate.ReadOnly = true;

            // lblContractStatus
            this.lblContractStatus.AutoSize = true;
            this.lblContractStatus.Location = new System.Drawing.Point(20, 165);
            this.lblContractStatus.Text = "Status:";

            // txtContractStatus
            this.txtContractStatus.Location = new System.Drawing.Point(150, 162);
            this.txtContractStatus.Size = new System.Drawing.Size(150, 22);
            this.txtContractStatus.ReadOnly = true;

            // lblBlocks
            this.lblBlocks.AutoSize = true;
            this.lblBlocks.Location = new System.Drawing.Point(20, 200);
            this.lblBlocks.Text = "Contract Blocks:";

            // lstBlocks
            this.lstBlocks.Location = new System.Drawing.Point(20, 225);
            this.lstBlocks.Size = new System.Drawing.Size(500, 180);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(20, 420);
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // ContractDetailsInternal
            this.ClientSize = new System.Drawing.Size(550, 470);
            this.Controls.Add(this.cbContracts);
            this.Controls.Add(this.lblSelectContract);
            this.Controls.Add(this.lblContractName);
            this.Controls.Add(this.txtContractName);
            this.Controls.Add(this.lblCreatorId);
            this.Controls.Add(this.txtCreatorId);
            this.Controls.Add(this.lblCreatedDate);
            this.Controls.Add(this.txtCreatedDate);
            this.Controls.Add(this.lblContractStatus);
            this.Controls.Add(this.txtContractStatus);
            this.Controls.Add(this.lblBlocks);
            this.Controls.Add(this.lstBlocks);
            this.Controls.Add(this.btnBack);
            this.Name = "ContractDetailsInternal";
            this.Text = "Contract Details (Internal)";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
