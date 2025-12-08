namespace MyProject.UI
{
    partial class AddBlockForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.Label lblOriginalBlock;
        private System.Windows.Forms.ComboBox cbContracts;
        private System.Windows.Forms.ComboBox cbOriginalBlocks;
        private System.Windows.Forms.Button btnAdd;
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
            this.lblOriginalBlock = new System.Windows.Forms.Label();
            this.cbContracts = new System.Windows.Forms.ComboBox();
            this.cbOriginalBlocks = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblContract
            this.lblContract.AutoSize = true;
            this.lblContract.Location = new System.Drawing.Point(30, 20);
            this.lblContract.Text = "Select Contract:";

            // cbContracts
            this.cbContracts.Location = new System.Drawing.Point(30, 45);
            this.cbContracts.Size = new System.Drawing.Size(300, 24);

            // lblOriginalBlock
            this.lblOriginalBlock.AutoSize = true;
            this.lblOriginalBlock.Location = new System.Drawing.Point(30, 80);
            this.lblOriginalBlock.Text = "Select Original Block:";

            // cbOriginalBlocks
            this.cbOriginalBlocks.Location = new System.Drawing.Point(30, 105);
            this.cbOriginalBlocks.Size = new System.Drawing.Size(300, 24);

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(30, 150);
            this.btnAdd.Size = new System.Drawing.Size(140, 30);
            this.btnAdd.Text = "Add Block";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(190, 150);
            this.btnBack.Size = new System.Drawing.Size(140, 30);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // AddBlockForm
            this.ClientSize = new System.Drawing.Size(370, 210);
            this.Controls.Add(this.lblContract);
            this.Controls.Add(this.cbContracts);
            this.Controls.Add(this.lblOriginalBlock);
            this.Controls.Add(this.cbOriginalBlocks);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnBack);
            this.Name = "AddBlockForm";
            this.Text = "Add Block to Contract";
            this.Load += new System.EventHandler(this.AddBlockForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
