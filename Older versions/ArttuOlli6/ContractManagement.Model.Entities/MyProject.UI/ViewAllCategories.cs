using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class ViewAllCategories : Form
    {
        private BlockController _blockController;

        public ViewAllCategories()
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
                    MessageBox.Show("No categories found.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dataGridViewCategories.DataSource = categories;
                dataGridViewCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}