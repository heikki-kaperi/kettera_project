using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class CreateCategory : Form
    {
        private BlockController _blockController;

        public CreateCategory(BlockController blockController)
        {
            InitializeComponent();
            _blockController = blockController;
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();
            string description = txtCategoryDescription.Text.Trim();

            // Tarkista kentät
            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please fill in all fields.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Luo kategoria controllerin kautta
            bool success = _blockController.CreateCategory(categoryName, description);

            if (success)
            {
                MessageBox.Show("Category created successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close(); // sulje ja palaa BlockCategory.cs näkymään
            }
            else
            {
                MessageBox.Show("Failed to create category.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CreateCategory_Load(object sender, EventArgs e)
        {

        }
    }
}
