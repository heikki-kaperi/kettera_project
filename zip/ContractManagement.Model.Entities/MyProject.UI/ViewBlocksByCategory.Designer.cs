namespace MyProject.UI
{
    partial class ViewBlocksByCategory
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
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.btnViewCategories = new System.Windows.Forms.Button();
            this.dataGridViewBlocks = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBlocks)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(250, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "VIEW BLOCKS BY CATEGORY";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Category:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(170, 67);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(250, 24);
            this.cmbCategory.TabIndex = 2;
            // 
            // btnViewCategories
            // 
            this.btnViewCategories.Location = new System.Drawing.Point(430, 67);
            this.btnViewCategories.Name = "btnViewCategories";
            this.btnViewCategories.Size = new System.Drawing.Size(140, 23);
            this.btnViewCategories.TabIndex = 3;
            this.btnViewCategories.Text = "View All Categories";
            this.btnViewCategories.UseVisualStyleBackColor = true;
            this.btnViewCategories.Click += new System.EventHandler(this.btnViewCategories_Click);
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
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(580, 67);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(713, 405);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ViewBlocksByCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dataGridViewBlocks);
            this.Controls.Add(this.btnViewCategories);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ViewBlocksByCategory";
            this.Text = "View Blocks By Category";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBlocks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Button btnViewCategories;
        private System.Windows.Forms.DataGridView dataGridViewBlocks;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClose;
    }
}