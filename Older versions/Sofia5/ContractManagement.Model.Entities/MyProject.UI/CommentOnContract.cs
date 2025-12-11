using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class CommentOnContract : Form
    {
        private CommentController _commentController;
        private int _contractId;
        private int _userId;
        private string _userType;

        public CommentOnContract(int contractId, int userId, string userType)
        {
            InitializeComponent();
            _contractId = contractId;
            _userId = userId;
            _userType = userType;
            _commentController = new CommentController();

            lblContractId.Text = $"Contract ID: {_contractId}";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string commentText = txtComment.Text.Trim();

            if (string.IsNullOrEmpty(commentText))
            {
                MessageBox.Show("Please enter a comment.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _commentController.AddComment(
                    _contractId,
                    null,
                    _userId,
                    _userType,
                    commentText);

                if (success)
                {
                    MessageBox.Show("Comment added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add comment.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}