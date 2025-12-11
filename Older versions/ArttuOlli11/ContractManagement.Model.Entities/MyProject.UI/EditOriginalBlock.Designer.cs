namespace MyProject.UI
{
    partial class EditOriginalBlock
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSelectBlock;
        private System.Windows.Forms.ComboBox cbBlocks;
        private System.Windows.Forms.Label lblBlockId;
        private System.Windows.Forms.TextBox txtBlockId;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label lblNewCategory;
        private System.Windows.Forms.ComboBox cbCategories;
        private System.Windows.Forms.Label lblBlockText;
        private System.Windows.Forms.TextBox txtBlockText;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSelectBlock = new System.Windows.Forms.Label();
            this.cbBlocks = new System.Windows.Forms.ComboBox();
            this.lblBlockId = new System.Windows.Forms.Label();
            this.txtBlockId = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblNewCategory = new System.Windows.Forms.Label();
            this.cbCategories = new System.Windows.Forms.ComboBox();
            this.lblBlockText = new System.Windows.Forms.Label();
            this.txtBlockText = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "EDIT ORIGINAL BLOCK";
            // 
            // lblSelectBlock
            // 
            this.lblSelectBlock.AutoSize = true;
            this.lblSelectBlock.Location = new System.Drawing.Point(20, 60);
            this.lblSelectBlock.Name = "lblSelectBlock";
            this.lblSelectBlock.Size = new System.Drawing.Size(72, 13);
            this.lblSelectBlock.TabIndex = 1;
            this.lblSelectBlock.Text = "Select Block:";
            // 
            // cbBlocks
            // 
            this.cbBlocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlocks.FormattingEnabled = true;
            this.cbBlocks.Location = new System.Drawing.Point(120, 57);
            this.cbBlocks.Name = "cbBlocks";
            this.cbBlocks.Size = new System.Drawing.Size(400, 21);
            this.cbBlocks.TabIndex = 2;
            this.cbBlocks.SelectedIndexChanged += new System.EventHandler(this.cbBlocks_SelectedIndexChanged);
            // 
            // lblBlockId
            // 
            this.lblBlockId.AutoSize = true;
            this.lblBlockId.Location = new System.Drawing.Point(20, 100);
            this.lblBlockId.Name = "lblBlockId";
            this.lblBlockId.Size = new System.Drawing.Size(52, 13);
            this.lblBlockId.TabIndex = 3;
            this.lblBlockId.Text = "Block ID:";
            // 
            // txtBlockId
            // 
            this.txtBlockId.Location = new System.Drawing.Point(120, 97);
            this.txtBlockId.Name = "txtBlockId";
            this.txtBlockId.ReadOnly = true;
            this.txtBlockId.Size = new System.Drawing.Size(100, 20);
            this.txtBlockId.TabIndex = 4;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(20, 130);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(92, 13);
            this.lblCategory.TabIndex = 5;
            this.lblCategory.Text = "Current Category:";
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(120, 127);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.ReadOnly = true;
            this.txtCategory.Size = new System.Drawing.Size(200, 20);
            this.txtCategory.TabIndex = 6;
            // 
            // lblNewCategory
            // 
            this.lblNewCategory.AutoSize = true;
            this.lblNewCategory.Location = new System.Drawing.Point(20, 160);
            this.lblNewCategory.Name = "lblNewCategory";
            this.lblNewCategory.Size = new System.Drawing.Size(80, 13);
            this.lblNewCategory.TabIndex = 7;
            this.lblNewCategory.Text = "New Category:";
            // 
            // cbCategories
            // 
            this.cbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategories.FormattingEnabled = true;
            this.cbCategories.Location = new System.Drawing.Point(120, 157);
            this.cbCategories.Name = "cbCategories";
            this.cbCategories.Size = new System.Drawing.Size(200, 21);
            this.cbCategories.TabIndex = 8;
            // 
            // lblBlockText
            // 
            this.lblBlockText.AutoSize = true;
            this.lblBlockText.Location = new System.Drawing.Point(20, 195);
            this.lblBlockText.Name = "lblBlockText";
            this.lblBlockText.Size = new System.Drawing.Size(62, 13);
            this.lblBlockText.TabIndex = 9;
            this.lblBlockText.Text = "Block Text:";
            // 
            // txtBlockText
            // 
            this.txtBlockText.Location = new System.Drawing.Point(120, 192);
            this.txtBlockText.Multiline = true;
            this.txtBlockText.Name = "txtBlockText";
            this.txtBlockText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBlockText.Size = new System.Drawing.Size(400, 150);
            this.txtBlockText.TabIndex = 10;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(320, 360);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 35);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(430, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditOriginalBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 420);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtBlockText);
            this.Controls.Add(this.lblBlockText);
            this.Controls.Add(this.cbCategories);
            this.Controls.Add(this.lblNewCategory);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.txtBlockId);
            this.Controls.Add(this.lblBlockId);
            this.Controls.Add(this.cbBlocks);
            this.Controls.Add(this.lblSelectBlock);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditOriginalBlock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Original Block";
            this.ResumeLayout(false);
            this.PerformLayout();
           
        }
    }
}