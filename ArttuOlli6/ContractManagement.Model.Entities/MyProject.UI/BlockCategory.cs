using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class BlockCategory : Form
    {
        private BlockController _blockController;
        private InternalUser _currentUser; // ← LISÄTTY

        public BlockCategory(InternalUser user) // ← MUUTETTU: lisätty user parametri
        {
            InitializeComponent();
            _blockController = new BlockController();
            _currentUser = user; // ← LISÄTTY
        }

        // 1. Create Category
        private void createCategoryBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateCategory form = new CreateCategory(_blockController);
            form.ShowDialog();
            this.Show();
        }

        // 2. View All Categories
        private void viewCategoriesBtn_Click(object sender, EventArgs e)
        {
            ViewAllCategories form = new ViewAllCategories();
            form.ShowDialog();
        }

        // 3. Create Original Block
        private void createOriginalBlockBtn_Click(object sender, EventArgs e)
        {
            CreateOriginalBlock form = new CreateOriginalBlock(_currentUser.Int_User_ID);
            form.ShowDialog();
        }

        // 4. View All Original Blocks
        private void viewOriginalBlocksBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewOriginalBlocks form = new ViewOriginalBlocks();
            form.ShowDialog();
            this.Show();
        }

        // 5. View Blocks by Category
        private void viewBlocksByCategoryBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewBlocksByCategory form = new ViewBlocksByCategory();
            form.ShowDialog();
            this.Show();
        }

        // 6. Copy Block
        private void copyBlockBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            CopyBlock form = new CopyBlock();
            form.ShowDialog();
            this.Show();
        }

        // 0. Back
        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BlockCategory_Load(object sender, EventArgs e)
        {
        }
    }
}