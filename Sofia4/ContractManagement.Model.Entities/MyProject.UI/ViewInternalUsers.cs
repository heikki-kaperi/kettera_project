using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class ViewInternalUsers : Form
    {
        private UserController _userController;

        public ViewInternalUsers(UserController userController)
        {
            InitializeComponent();
            _userController = userController;
            LoadInternalUsers();
        }

        private void LoadInternalUsers()
        {
            try
            {
                var users = _userController.GetAllInternalUsers();
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