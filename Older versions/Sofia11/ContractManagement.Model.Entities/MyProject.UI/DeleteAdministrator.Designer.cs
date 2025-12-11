namespace ContractManagement.View
{
    partial class DeleteAdministrator
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
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblAdmin = new System.Windows.Forms.Label();
            this.cmbAdministrators = new System.Windows.Forms.ComboBox();
            this.lblVoteCount = new System.Windows.Forms.Label();
            this.btnVote = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInstructions
            // 
            this.lblInstructions.Location = new System.Drawing.Point(20, 20);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(440, 60);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Text = "Select an administrator to vote for deletion.\r\n3 votes are required to delete a" +
    "n administrator.\r\nYou cannot vote to delete yourself.";
            // 
            // lblAdmin
            // 
            this.lblAdmin.AutoSize = true;
            this.lblAdmin.Location = new System.Drawing.Point(20, 90);
            this.lblAdmin.Name = "lblAdmin";
            this.lblAdmin.Size = new System.Drawing.Size(122, 13);
            this.lblAdmin.TabIndex = 1;
            this.lblAdmin.Text = "Select Administrator:";
            // 
            // cmbAdministrators
            // 
            this.cmbAdministrators.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdministrators.FormattingEnabled = true;
            this.cmbAdministrators.Location = new System.Drawing.Point(20, 115);
            this.cmbAdministrators.Name = "cmbAdministrators";
            this.cmbAdministrators.Size = new System.Drawing.Size(440, 21);
            this.cmbAdministrators.TabIndex = 2;
            this.cmbAdministrators.SelectedIndexChanged += new System.EventHandler(this.CmbAdministrators_SelectedIndexChanged);
            // 
            // lblVoteCount
            // 
            this.lblVoteCount.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblVoteCount.Location = new System.Drawing.Point(20, 150);
            this.lblVoteCount.Name = "lblVoteCount";
            this.lblVoteCount.Size = new System.Drawing.Size(440, 20);
            this.lblVoteCount.TabIndex = 3;
            // 
            // btnVote
            // 
            this.btnVote.Location = new System.Drawing.Point(20, 190);
            this.btnVote.Name = "btnVote";
            this.btnVote.Size = new System.Drawing.Size(200, 40);
            this.btnVote.TabIndex = 4;
            this.btnVote.Text = "Vote for Deletion";
            this.btnVote.UseVisualStyleBackColor = true;
            this.btnVote.Click += new System.EventHandler(this.BtnVote_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DeleteAdministrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnVote);
            this.Controls.Add(this.lblVoteCount);
            this.Controls.Add(this.cmbAdministrators);
            this.Controls.Add(this.lblAdmin);
            this.Controls.Add(this.lblInstructions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteAdministrator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Delete Administrator - Voting System";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblAdmin;
        private System.Windows.Forms.ComboBox cmbAdministrators;
        private System.Windows.Forms.Label lblVoteCount;
        private System.Windows.Forms.Button btnVote;
        private System.Windows.Forms.Button btnCancel;
    }
}