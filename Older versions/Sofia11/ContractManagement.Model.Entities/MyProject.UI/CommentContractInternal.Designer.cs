namespace MyProject.UI
{
    partial class CommentContractInternal
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbContracts;
        private System.Windows.Forms.Label lblSelectContract;
        private System.Windows.Forms.ListBox lstComments;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.TextBox txtNewComment;
        private System.Windows.Forms.Label lblNewComment;
        private System.Windows.Forms.Button btnAddComment;
        private System.Windows.Forms.Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbContracts = new System.Windows.Forms.ComboBox();
            this.lblSelectContract = new System.Windows.Forms.Label();
            this.lstComments = new System.Windows.Forms.ListBox();
            this.lblComments = new System.Windows.Forms.Label();
            this.txtNewComment = new System.Windows.Forms.TextBox();
            this.lblNewComment = new System.Windows.Forms.Label();
            this.btnAddComment = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblSelectContract
            this.lblSelectContract.AutoSize = true;
            this.lblSelectContract.Location = new System.Drawing.Point(20, 20);
            this.lblSelectContract.Text = "Select Contract:";

            // cbContracts
            this.cbContracts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbContracts.Location = new System.Drawing.Point(150, 17);
            this.cbContracts.Size = new System.Drawing.Size(300, 24);
            this.cbContracts.SelectedIndexChanged += new System.EventHandler(this.cbContracts_SelectedIndexChanged);

            // lblComments
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(20, 60);
            this.lblComments.Text = "Comments:";

            // lstComments
            this.lstComments.Location = new System.Drawing.Point(20, 85);
            this.lstComments.Size = new System.Drawing.Size(500, 180);

            // lblNewComment
            this.lblNewComment.AutoSize = true;
            this.lblNewComment.Location = new System.Drawing.Point(20, 280);
            this.lblNewComment.Text = "New Comment:";

            // txtNewComment
            this.txtNewComment.Location = new System.Drawing.Point(20, 305);
            this.txtNewComment.Size = new System.Drawing.Size(500, 22);

            // btnAddComment
            this.btnAddComment.Location = new System.Drawing.Point(20, 340);
            this.btnAddComment.Size = new System.Drawing.Size(100, 30);
            this.btnAddComment.Text = "Add Comment";
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_Click);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(130, 340);
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // CommentContractInternal
            this.ClientSize = new System.Drawing.Size(550, 400);
            this.Controls.Add(this.lblSelectContract);
            this.Controls.Add(this.cbContracts);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.lstComments);
            this.Controls.Add(this.lblNewComment);
            this.Controls.Add(this.txtNewComment);
            this.Controls.Add(this.btnAddComment);
            this.Controls.Add(this.btnBack);
            this.Name = "CommentContractInternal";
            this.Text = "Comment on Contract (Internal)";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
