namespace MyProject.UI
{
    partial class EditBlockForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cbContracts;
        private System.Windows.Forms.ComboBox cbBlocks;
        private System.Windows.Forms.TextBox txtBlockText;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblContracts;
        private System.Windows.Forms.Label lblBlocks;
        private System.Windows.Forms.Label lblBlockText;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbContracts = new System.Windows.Forms.ComboBox();
            this.cbBlocks = new System.Windows.Forms.ComboBox();
            this.txtBlockText = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblContracts = new System.Windows.Forms.Label();
            this.lblBlocks = new System.Windows.Forms.Label();
            this.lblBlockText = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblContracts
            this.lblContracts.AutoSize = true;
            this.lblContracts.Location = new System.Drawing.Point(20, 20);
            this.lblContracts.Text = "Valitse Contract:";

            // cbContracts
            this.cbContracts.Location = new System.Drawing.Point(20, 40);
            this.cbContracts.Size = new System.Drawing.Size(300, 24);
            this.cbContracts.SelectedIndexChanged += new System.EventHandler(this.cbContracts_SelectedIndexChanged);

            // lblBlocks
            this.lblBlocks.AutoSize = true;
            this.lblBlocks.Location = new System.Drawing.Point(20, 80);
            this.lblBlocks.Text = "Valitse Block:";

            // cbBlocks
            this.cbBlocks.Location = new System.Drawing.Point(20, 100);
            this.cbBlocks.Size = new System.Drawing.Size(300, 24);
            this.cbBlocks.SelectedIndexChanged += new System.EventHandler(this.cbBlocks_SelectedIndexChanged);

            // lblBlockText
            this.lblBlockText.AutoSize = true;
            this.lblBlockText.Location = new System.Drawing.Point(20, 140);
            this.lblBlockText.Text = "Block Text:";

            // txtBlockText
            this.txtBlockText.Location = new System.Drawing.Point(20, 160);
            this.txtBlockText.Size = new System.Drawing.Size(300, 100);
            this.txtBlockText.Multiline = true;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(20, 280);
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(140, 280);
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // EditBlockForm
            this.ClientSize = new System.Drawing.Size(360, 330);
            this.Controls.Add(this.lblContracts);
            this.Controls.Add(this.cbContracts);
            this.Controls.Add(this.lblBlocks);
            this.Controls.Add(this.cbBlocks);
            this.Controls.Add(this.lblBlockText);
            this.Controls.Add(this.txtBlockText);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBack);
            this.Name = "EditBlockForm";
            this.Text = "Edit Block in Contract";
            this.Load += new System.EventHandler(this.EditBlockForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
