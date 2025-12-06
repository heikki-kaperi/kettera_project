namespace MyProject.UI
{
    partial class CopyBlock
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBlockId = new System.Windows.Forms.TextBox();
            this.btnViewBlocks = new System.Windows.Forms.Button();
            this.dataGridViewBlocks = new System.Windows.Forms.DataGridView();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBlocks)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(300, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "COPY BLOCK";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Original Block ID to copy:";
            // 
            // txtBlockId
            // 
            this.txtBlockId.Location = new System.Drawing.Point(220, 67);
            this.txtBlockId.Name = "txtBlockId";
            this.txtBlockId.Size = new System.Drawing.Size(150, 22);
            this.txtBlockId.TabIndex = 2;
            // 
            // btnViewBlocks
            // 
            this.btnViewBlocks.Location = new System.Drawing.Point(380, 67);
            this.btnViewBlocks.Name = "btnViewBlocks";
            this.btnViewBlocks.Size = new System.Drawing.Size(140, 23);
            this.btnViewBlocks.TabIndex = 3;
            this.btnViewBlocks.Text = "View All Blocks";
            this.btnViewBlocks.UseVisualStyleBackColor = true;
            this.btnViewBlocks.Click += new System.EventHandler(this.btnViewBlocks_Click);
            // 
            // dataGridViewBlocks
            // 
            this.dataGridViewBlocks.AllowUserToAddRows = false;
            this.dataGridViewBlocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBlocks.Location = new System.Drawing.Point(12, 110);
            this.dataGridViewBlocks.Name = "dataGridViewBlocks";
            this.dataGridViewBlocks.ReadOnly = true;
            this.dataGridViewBlocks.RowHeadersWidth = 51;
            this.dataGridViewBlocks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBlocks.Size = new System.Drawing.Size(776, 280);
            this.dataGridViewBlocks.TabIndex = 4;
            this.dataGridViewBlocks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBlocks_CellClick);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(638, 405);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(719, 405);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CopyBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.dataGridViewBlocks);
            this.Controls.Add(this.btnViewBlocks);
            this.Controls.Add(this.txtBlockId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CopyBlock";
            this.Text = "Copy Block";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBlocks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBlockId;
        private System.Windows.Forms.Button btnViewBlocks;
        private System.Windows.Forms.DataGridView dataGridViewBlocks;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCancel;
    }
}