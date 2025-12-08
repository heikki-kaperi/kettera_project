using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class Internal : Form
    {
        private InternalUser _currentUser;

        public Internal(InternalUser user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private void Internal_Load(object sender, EventArgs e)
        {
        }

        private void blockCategoryBtn_Click(object sender, EventArgs e)
        {
            BlockCategory form = new BlockCategory(_currentUser);
            form.ShowDialog();
        }

        private void contractManagementBtn_Click(object sender, EventArgs e)
        {
            ContractManagement form = new ContractManagement();
            form.ShowDialog();
        }

        private void myContractsBtn_Click(object sender, EventArgs e)
        {
            MyContractsReviewer form = new MyContractsReviewer(_currentUser); // Lisätty _currentUser parametri
            form.ShowDialog();
        }

        private void openContractDetailsBtn_Click(object sender, EventArgs e)
        {
            ContractController controller = new ContractController();
            ContractDetailsInternal form = new ContractDetailsInternal(controller);
            form.ShowDialog();
        }

        private void commentContractBtn_Click(object sender, EventArgs e)
        {
            CommentContractInternal form = new CommentContractInternal();
            form.ShowDialog();
        }

        private void approveContractBtn_Click(object sender, EventArgs e)
        {
            ApproveContract form = new ApproveContract(_currentUser);
            form.ShowDialog();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                logIn newLoginForm = new logIn();
                newLoginForm.Show();
                this.Close();
            }
        }
    }
}
