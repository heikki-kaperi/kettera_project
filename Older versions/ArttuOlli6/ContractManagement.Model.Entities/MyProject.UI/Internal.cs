using System;
using System.Windows.Forms;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class Internal : Form
    {
        private InternalUser _currentUser; // ← LISÄTTY

        public Internal(InternalUser user) // ← MUUTETTU: lisätty user parametri
        {
            InitializeComponent();
            _currentUser = user; // ← LISÄTTY
        }

        private void Internal_Load(object sender, EventArgs e)
        {
        }

        // 1. Block & Category Management
        private void blockCategoryBtn_Click(object sender, EventArgs e)
        {
            BlockCategory form = new BlockCategory(_currentUser);
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
            MyContracts form = new MyContracts(_currentUser); // ← KORJATTU
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
            var result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Etsi ja palauta login-lomake alkutilaan
                foreach (Form form in Application.OpenForms)
                {
                    if (form is logIn loginForm)
                    {
                        // Palauta alkutilaan
                        loginForm.Controls["button1"].Visible = true; // Admin nappi
                        loginForm.Controls["button2"].Visible = true; // Internal nappi
                        loginForm.Controls["button3"].Visible = true; // External nappi
                        loginForm.Controls["txtUsername"].Visible = false;
                        loginForm.Controls["txtPassword"].Visible = false;
                        loginForm.Controls["label1"].Visible = false;
                        loginForm.Controls["label2"].Visible = false;
                        loginForm.Controls["button4"].Enabled = false;

                        // Tyhjennä kentät
                        ((TextBox)loginForm.Controls["txtUsername"]).Text = "";
                        ((TextBox)loginForm.Controls["txtPassword"]).Text = "";

                        loginForm.Show();
                        this.Close();
                        return;
                    }
                }

                // Jos login-lomaketta ei löydy, luo uusi
                logIn newLoginForm = new logIn();
                newLoginForm.Show();
                this.Close();
            }
        }
    }
}