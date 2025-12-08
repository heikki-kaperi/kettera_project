namespace MyProject.UI
{
    partial class RemoveBlockForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.Label lblBlock;
        private System.Windows.Forms.ComboBox cbContracts;
        private System.Windows.Forms.ComboBox cbBlocks;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblContract = new System.Windows.Forms.Label();
            this.lblBlock = new System.Windows.Forms.Label();
            this.cbContracts = new System.Windows.Forms.ComboBox();
            this.cbBlocks = new System.Windows.Forms.ComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblContract
            this.lblContract.AutoSize = true;
            this.lblContract.Location = new System.Drawing.Point(30, 20);
            this.lblContract.Text = "Select Contract:";

            // cbContracts
            this.cbContracts.Location = new System.Drawing.Point(30, 45);
            this.cbContracts.Size = new System.Drawing.Size(300, 24);
            this.cbContracts.SelectedIndexChanged += new System.EventHandler(this.cbContracts_SelectedIndexChanged);

            // lblBlock
            this.lblBlock.AutoSize = true;
            this.lblBlock.Location = new System.Drawing.Point(30, 80);
            this.lblBlock.Text = "Select Block:";

            // cbBlocks
            this.cbBlocks.Location = new System.Drawing.Point(30, 105);
            this.cbBlocks.Size = new System.Drawing.Size(300, 24);

            // btnRemove
            this.btnRemove.Location = new System.Drawing.Point(30, 150);
            this.btnRemove.Size = new System.Drawing.Size(140, 30);
            this.btnRemove.Text = "Remove Block";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(190, 150);
            this.btnBack.Size = new System.Drawing.Size(140, 30);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // RemoveBlockForm
            this.ClientSize = new System.Drawing.Size(370, 210);
            this.Controls.Add(this.lblContract);
            this.Controls.Add(this.cbContracts);
            this.Controls.Add(this.lblBlock);
            this.Controls.Add(this.cbBlocks);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnBack);
            this.Name = "RemoveBlockForm";
            this.Text = "Remove Block from Contract";
            this.Load += new System.EventHandler(this.RemoveBlockForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
