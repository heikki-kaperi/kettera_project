using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.View;

namespace MyProject.UI
{
    public partial class Administrator : Form
    {
        private int userId; // Admin ID tallennetaan tähän
        private UserController _userController;

        // Poista tämä tyhjä constructor kokonaan tai käytä vain yhtä
        public Administrator(int adminId)
        {
            InitializeComponent();
            this.userId = adminId; // Tallenna admin ID
            _userController = new UserController();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateInternal createForm = new CreateInternal(_userController);
            createForm.ShowDialog();
        }

        private void createExternalBtn_Click(object sender, EventArgs e)
        {
            CreateExternal createForm = new CreateExternal(_userController);
            createForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewInternalUsers viewForm = new ViewInternalUsers(_userController);
            viewForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewExternalUsers viewForm = new ViewExternalUsers(_userController);
            viewForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DeleteInternalUsers deleteForm = new DeleteInternalUsers(_userController);
            deleteForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DeleteExternalUsers deleteForm = new DeleteExternalUsers(_userController);
            deleteForm.ShowDialog();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                logIn loginForm = new logIn();
                loginForm.ShowDialog();
                this.Close();
            }
        }

        private void btnDeleteAdmin_Click(object sender, EventArgs e)
        {
            DeleteAdministrator deleteForm = new DeleteAdministrator(this.userId); // Käytä userId, ei adminId
            deleteForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}