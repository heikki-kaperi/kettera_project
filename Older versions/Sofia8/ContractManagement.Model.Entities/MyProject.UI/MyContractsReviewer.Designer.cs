namespace MyProject.UI
{
    partial class MyContractsReviewer
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbContracts;
        private System.Windows.Forms.ListBox lstBlocks;
        private System.Windows.Forms.Label lblContracts;
        private System.Windows.Forms.Label lblBlocks;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbContracts = new System.Windows.Forms.ComboBox();
            this.lstBlocks = new System.Windows.Forms.ListBox();
            this.lblContracts = new System.Windows.Forms.Label();
            this.lblBlocks = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblContracts
            this.lblContracts.AutoSize = true;
            this.lblContracts.Location = new System.Drawing.Point(20, 20);
            this.lblContracts.Text = "Select Contract:";

            // cbContracts
            this.cbContracts.Location = new System.Drawing.Point(20, 45);
            this.cbContracts.Size = new System.Drawing.Size(300, 24);
            this.cbContracts.SelectedIndexChanged += new System.EventHandler(this.cbContracts_SelectedIndexChanged);

            // lblBlocks
            this.lblBlocks.AutoSize = true;
            this.lblBlocks.Location = new System.Drawing.Point(20, 80);
            this.lblBlocks.Text = "Contract Blocks:";

            // lstBlocks
            this.lstBlocks.Location = new System.Drawing.Point(20, 105);
            this.lstBlocks.Size = new System.Drawing.Size(500, 250);

            // MyContractsReviewer
            this.ClientSize = new System.Drawing.Size(550, 380);
            this.Controls.Add(this.lblContracts);
            this.Controls.Add(this.cbContracts);
            this.Controls.Add(this.lblBlocks);
            this.Controls.Add(this.lstBlocks);
            this.Name = "MyContractsReviewer";
            this.Text = "My Contracts (Reviewer)";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
