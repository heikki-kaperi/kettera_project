using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class CreateOriginalBlock : Form
    {
        private BlockController _blockController;
        private int _userId;

        public CreateOriginalBlock(int userId)
        {
            InitializeComponent();
            _blockController = new BlockController();
            _userId = userId;
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _blockController.GetAllCategories();

                if (categories.Count == 0)
                {
                    MessageBox.Show("No categories found. Please create a category first.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cmbCategory.DisplayMember = "Category_name";
                cmbCategory.ValueMember = "Category_name";
                cmbCategory.DataSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewCategories_Click(object sender, EventArgs e)
        {
            ViewAllCategories viewForm = new ViewAllCategories();
            viewForm.ShowDialog();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string category = cmbCategory.Text;
            string text = txtBlockText.Text.Trim();

            if (string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Please select a category.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Please enter block text.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _blockController.CreateOriginalBlock(category, text, _userId);

                if (success)
                {
                    MessageBox.Show("Block created successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create block.", "Error",
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