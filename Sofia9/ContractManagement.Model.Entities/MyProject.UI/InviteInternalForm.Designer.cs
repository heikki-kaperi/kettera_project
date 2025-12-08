namespace MyProject.UI
{
    partial class InviteInternalForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbContracts;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.CheckBox chkApproval;
        private System.Windows.Forms.Button btnInvite;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.Label lblUserId;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbContracts = new System.Windows.Forms.ComboBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.chkApproval = new System.Windows.Forms.CheckBox();
            this.btnInvite = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblContract = new System.Windows.Forms.Label();
            this.lblUserId = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblContract
            this.lblContract.AutoSize = true;
            this.lblContract.Location = new System.Drawing.Point(30, 20);
            this.lblContract.Text = "Valitse sopimus:";

            // cbContracts
            this.cbContracts.Location = new System.Drawing.Point(30, 45);
            this.cbContracts.Size = new System.Drawing.Size(250, 24);

            // lblUserId
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(30, 85);
            this.lblUserId.Text = "Käyttäjä-ID:";

            // txtUserId
            this.txtUserId.Location = new System.Drawing.Point(30, 110);
            this.txtUserId.Size = new System.Drawing.Size(250, 22);

            // chkApproval
            this.chkApproval.AutoSize = true;
            this.chkApproval.Location = new System.Drawing.Point(30, 145);
            this.chkApproval.Text = "Onko hyväksymisoikeus";

            // btnInvite
            this.btnInvite.Location = new System.Drawing.Point(30, 180);
            this.btnInvite.Size = new System.Drawing.Size(120, 30);
            this.btnInvite.Text = "Invite";
            this.btnInvite.Click += new System.EventHandler(this.btnInvite_Click);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(160, 180);
            this.btnBack.Size = new System.Drawing.Size(120, 30);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // InviteInternalForm
            this.ClientSize = new System.Drawing.Size(320, 230);
            this.Controls.Add(this.lblContract);
            this.Controls.Add(this.cbContracts);
            this.Controls.Add(this.lblUserId);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.chkApproval);
            this.Controls.Add(this.btnInvite);
            this.Controls.Add(this.btnBack);
            this.Text = "Invite Internal Reviewer";
            this.Load += new System.EventHandler(this.InviteInternalForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
