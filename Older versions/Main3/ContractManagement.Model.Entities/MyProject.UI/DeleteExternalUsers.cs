using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class DeleteExternalUsers : Form
    {
        private UserController _userController;

        public DeleteExternalUsers(UserController userController)
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
                MessageBox.Show("Error loading users: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one user to delete.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete {dataGridViewUsers.SelectedRows.Count} external user(s)?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int successCount = 0;
                int failCount = 0;

                foreach (DataGridViewRow row in dataGridViewUsers.SelectedRows)
                {
                    var user = row.DataBoundItem as ExternalUser;
                    if (user != null)
                    {
                        bool success = _userController.DeleteExternalUser(user.Ext_User_ID);
                        if (success)
                            successCount++;
                        else
                            failCount++;
                    }
                }

                MessageBox.Show(
                    $"Successfully deleted: {successCount}\nFailed: {failCount}",
                    "Deletion Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadExternalUsers();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}