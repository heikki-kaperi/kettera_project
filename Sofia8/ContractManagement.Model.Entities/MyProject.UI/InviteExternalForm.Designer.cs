namespace MyProject.UI
{
    partial class InviteExternalForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbContracts;
        private System.Windows.Forms.TextBox txtExternalUserId;
        private System.Windows.Forms.Button btnInvite;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.Label lblExternalUserId;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbContracts = new System.Windows.Forms.ComboBox();
            this.txtExternalUserId = new System.Windows.Forms.TextBox();
            this.btnInvite = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblContract = new System.Windows.Forms.Label();
            this.lblExternalUserId = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblContract
            this.lblContract.AutoSize = true;
            this.lblContract.Location = new System.Drawing.Point(30, 20);
            this.lblContract.Text = "Valitse sopimus:";

            // cbContracts
            this.cbContracts.Location = new System.Drawing.Point(30, 45);
            this.cbContracts.Size = new System.Drawing.Size(250, 24);

            // lblExternalUserId
            this.lblExternalUserId.AutoSize = true;
            this.lblExternalUserId.Location = new System.Drawing.Point(30, 85);
            this.lblExternalUserId.Text = "Ulkoinen käyttäjä-ID:";

            // txtExternalUserId
            this.txtExternalUserId.Location = new System.Drawing.Point(30, 110);
            this.txtExternalUserId.Size = new System.Drawing.Size(250, 22);

            // btnInvite
            this.btnInvite.Location = new System.Drawing.Point(30, 150);
            this.btnInvite.Size = new System.Drawing.Size(120, 30);
            this.btnInvite.Text = "Invite";
            this.btnInvite.Click += new System.EventHandler(this.btnInvite_Click);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(160, 150);
            this.btnBack.Size = new System.Drawing.Size(120, 30);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // InviteExternalForm
            this.ClientSize = new System.Drawing.Size(320, 200);
            this.Controls.Add(this.lblContract);
            this.Controls.Add(this.cbContracts);
            this.Controls.Add(this.lblExternalUserId);
            this.Controls.Add(this.txtExternalUserId);
            this.Controls.Add(this.btnInvite);
            this.Controls.Add(this.btnBack);
            this.Text = "Invite External User";
            this.Load += new System.EventHandler(this.InviteExternalForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
