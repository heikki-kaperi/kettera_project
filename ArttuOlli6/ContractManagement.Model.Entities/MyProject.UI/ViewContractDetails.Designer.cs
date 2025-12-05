namespace MyProject.UI
{
    partial class ViewContractDetails
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblContractInfo = new System.Windows.Forms.Label();
            this.dataGridViewBlocks = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBlocks)).BeginInit();
            this.SuspendLayout();
            // 
            // lblContractInfo
            // 
            this.lblContractInfo.AutoSize = true;
            this.lblContractInfo.Location = new System.Drawing.Point(12, 50);
            this.lblContractInfo.Name = "lblContractInfo";
            this.lblContractInfo.Size = new System.Drawing.Size(100, 16);
            this.lblContractInfo.TabIndex = 0;
            this.lblContractInfo.Text = "Contract Info";
            // 
            // dataGridViewBlocks
            // 
            this.dataGridViewBlocks.AllowUserToAddRows = false;
            this.dataGridViewBlocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBlocks.Location = new System.Drawing.Point(12, 120);
            this.dataGridViewBlocks.Name = "dataGridViewBlocks";
            this.dataGridViewBlocks.ReadOnly = true;
            this.dataGridViewBlocks.RowHeadersWidth = 51;
            this.dataGridViewBlocks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBlocks.Size = new System.Drawing.Size(776, 280);
            this.dataGridViewBlocks.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(713, 415);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(280, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "CONTRACT DETAILS";
            // 
            // ViewContractDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridViewBlocks);
            this.Controls.Add(this.lblContractInfo);
            this.Name = "ViewContractDetails";
            this.Text = "Contract Details";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBlocks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblContractInfo;
        private System.Windows.Forms.DataGridView dataGridViewBlocks;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
    }
}