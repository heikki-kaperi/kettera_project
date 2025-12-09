namespace MyProject.UI
{
    partial class BlockCategory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.createCategoryBtn = new System.Windows.Forms.Button();
            this.viewCategoriesBtn = new System.Windows.Forms.Button();
            this.createOriginalBlockBtn = new System.Windows.Forms.Button();
            this.viewOriginalBlocksBtn = new System.Windows.Forms.Button();
            this.viewBlocksByCategoryBtn = new System.Windows.Forms.Button();
            this.copyBlockBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.btnEditOriginalBlock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "BLOCK AND CATEGORY MANAGEMENT";
            // 
            // createCategoryBtn
            // 
            this.createCategoryBtn.Location = new System.Drawing.Point(238, 101);
            this.createCategoryBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createCategoryBtn.Name = "createCategoryBtn";
            this.createCategoryBtn.Size = new System.Drawing.Size(140, 19);
            this.createCategoryBtn.TabIndex = 1;
            this.createCategoryBtn.Text = "Create Category";
            this.createCategoryBtn.UseVisualStyleBackColor = true;
            this.createCategoryBtn.Click += new System.EventHandler(this.createCategoryBtn_Click);
            // 
            // viewCategoriesBtn
            // 
            this.viewCategoriesBtn.Location = new System.Drawing.Point(238, 126);
            this.viewCategoriesBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.viewCategoriesBtn.Name = "viewCategoriesBtn";
            this.viewCategoriesBtn.Size = new System.Drawing.Size(140, 19);
            this.viewCategoriesBtn.TabIndex = 2;
            this.viewCategoriesBtn.Text = "View All Categories";
            this.viewCategoriesBtn.UseVisualStyleBackColor = true;
            this.viewCategoriesBtn.Click += new System.EventHandler(this.viewCategoriesBtn_Click);
            // 
            // createOriginalBlockBtn
            // 
            this.createOriginalBlockBtn.Location = new System.Drawing.Point(238, 150);
            this.createOriginalBlockBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createOriginalBlockBtn.Name = "createOriginalBlockBtn";
            this.createOriginalBlockBtn.Size = new System.Drawing.Size(140, 19);
            this.createOriginalBlockBtn.TabIndex = 3;
            this.createOriginalBlockBtn.Text = "Create Original Block";
            this.createOriginalBlockBtn.UseVisualStyleBackColor = true;
            this.createOriginalBlockBtn.Click += new System.EventHandler(this.createOriginalBlockBtn_Click);
            // 
            // viewOriginalBlocksBtn
            // 
            this.viewOriginalBlocksBtn.Location = new System.Drawing.Point(238, 174);
            this.viewOriginalBlocksBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.viewOriginalBlocksBtn.Name = "viewOriginalBlocksBtn";
            this.viewOriginalBlocksBtn.Size = new System.Drawing.Size(140, 19);
            this.viewOriginalBlocksBtn.TabIndex = 4;
            this.viewOriginalBlocksBtn.Text = "View All Original Blocks";
            this.viewOriginalBlocksBtn.UseVisualStyleBackColor = true;
            this.viewOriginalBlocksBtn.Click += new System.EventHandler(this.viewOriginalBlocksBtn_Click);
            // 
            // viewBlocksByCategoryBtn
            // 
            this.viewBlocksByCategoryBtn.Location = new System.Drawing.Point(238, 199);
            this.viewBlocksByCategoryBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.viewBlocksByCategoryBtn.Name = "viewBlocksByCategoryBtn";
            this.viewBlocksByCategoryBtn.Size = new System.Drawing.Size(140, 19);
            this.viewBlocksByCategoryBtn.TabIndex = 5;
            this.viewBlocksByCategoryBtn.Text = "View Blocks by Category";
            this.viewBlocksByCategoryBtn.UseVisualStyleBackColor = true;
            this.viewBlocksByCategoryBtn.Click += new System.EventHandler(this.viewBlocksByCategoryBtn_Click);
            // 
            // copyBlockBtn
            // 
            this.copyBlockBtn.Location = new System.Drawing.Point(238, 222);
            this.copyBlockBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.copyBlockBtn.Name = "copyBlockBtn";
            this.copyBlockBtn.Size = new System.Drawing.Size(140, 19);
            this.copyBlockBtn.TabIndex = 6;
            this.copyBlockBtn.Text = "Copy Block";
            this.copyBlockBtn.UseVisualStyleBackColor = true;
            this.copyBlockBtn.Click += new System.EventHandler(this.copyBlockBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.Location = new System.Drawing.Point(280, 278);
            this.backBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(56, 19);
            this.backBtn.TabIndex = 7;
            this.backBtn.Text = "Back";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // btnEditOriginalBlock
            // 
            this.btnEditOriginalBlock.Location = new System.Drawing.Point(238, 246);
            this.btnEditOriginalBlock.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEditOriginalBlock.Name = "btnEditOriginalBlock";
            this.btnEditOriginalBlock.Size = new System.Drawing.Size(140, 19);
            this.btnEditOriginalBlock.TabIndex = 8;
            this.btnEditOriginalBlock.Text = "Modify Existing Block";
            this.btnEditOriginalBlock.UseVisualStyleBackColor = true;
            this.btnEditOriginalBlock.Click += new System.EventHandler(this.btnEditOriginalBlock_Click);
            // 
            // BlockCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.btnEditOriginalBlock);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.copyBlockBtn);
            this.Controls.Add(this.viewBlocksByCategoryBtn);
            this.Controls.Add(this.viewOriginalBlocksBtn);
            this.Controls.Add(this.createOriginalBlockBtn);
            this.Controls.Add(this.viewCategoriesBtn);
            this.Controls.Add(this.createCategoryBtn);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "BlockCategory";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.BlockCategory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button createCategoryBtn;
        private System.Windows.Forms.Button viewCategoriesBtn;
        private System.Windows.Forms.Button createOriginalBlockBtn;
        private System.Windows.Forms.Button viewOriginalBlocksBtn;
        private System.Windows.Forms.Button viewBlocksByCategoryBtn;
        private System.Windows.Forms.Button copyBlockBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button btnEditOriginalBlock;
    }
}