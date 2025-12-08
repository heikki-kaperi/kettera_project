using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class ViewExternalUsers : Form
    {
        private UserController _userController;

        public ViewExternalUsers(UserController userController)
        {
            InitializeComponent();
            _userController = userController;
            LoadExternalUsers();
        }

        private void LoadExternalUsers()
        {
            try
            {
                var users = _userController.GetAllExternalUsers();
                dataGridViewUsers.DataSource = users;
                dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}