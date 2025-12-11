using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MyProject.UI
{
    public partial class Internal : Form
    {
        public Internal()
        {
            InitializeComponent();
        }

        private void Internal_Load(object sender, EventArgs e)
        {

        }

        // 1. Block & Category Management
        private void blockCategoryBtn_Click(object sender, EventArgs e)
        {
            BlockCategory form = new BlockCategory();
            form.ShowDialog();
        }

        // 2. Contract Management
        private void contractManagementBtn_Click(object sender, EventArgs e)
        {
            ContractManagement form = new ContractManagement();
            form.ShowDialog();
        }

        // 3. My Contracts (as Reviewer)
        private void myContractsBtn_Click(object sender, EventArgs e)
        {
            MyContracts form = new MyContracts();
            form.ShowDialog();
        }

        // 4. View Contract Details
        private void viewContractDetailsBtn_Click(object sender, EventArgs e)
        {
            ContractDetails form = new ContractDetails();
            form.ShowDialog();
        }

        // 5. Comment on Contract
        private void commentContractBtn_Click(object sender, EventArgs e)
        {
            CommentContract form = new CommentContract();
            form.ShowDialog();
        }

        // 6. Approve Contract
        private void approveContractBtn_Click(object sender, EventArgs e)
        {
            ApproveContract form = new ApproveContract();
            form.ShowDialog();
        }

        // 0. Logout → takaisin LogIn.cs
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide(); // piilota Internal-näkymä
            logIn loginForm = new logIn();
            loginForm.Show();
        }
    }
}
