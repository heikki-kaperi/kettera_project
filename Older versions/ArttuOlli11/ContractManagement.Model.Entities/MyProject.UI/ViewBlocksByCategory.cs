using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class ViewBlocksByCategory : Form
    {
        private BlockController _blockController;

        public ViewBlocksByCategory()
        {
            InitializeComponent();
            _blockController = new BlockController();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedValue == null)
            {
                MessageBox.Show("Please select a category.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string category = cmbCategory.SelectedValue.ToString();
            LoadBlocksByCategory(category);
        }

        private void LoadBlocksByCategory(string category)
        {
            try
            {
                var blocks = _blockController.GetOriginalBlocksByCategory(category);

                if (blocks.Count == 0)
                {
                    MessageBox.Show($"No blocks found in category '{category}'.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridViewBlocks.DataSource = null;
                    return;
                }

                dataGridViewBlocks.DataSource = blocks;
                dataGridViewBlocks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading blocks: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}