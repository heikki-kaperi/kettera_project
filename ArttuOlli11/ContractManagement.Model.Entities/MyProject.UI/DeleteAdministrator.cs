using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace ContractManagement.View
{
    public partial class DeleteAdministrator : Form
    {
        private int currentAdminId;
        private UserController userController;
        private AdministratorController adminController;

        public DeleteAdministrator(int adminId)
        {
            this.currentAdminId = adminId;
            this.userController = new UserController();
            this.adminController = new AdministratorController();
            InitializeComponent();
            LoadAdministrators();
        }

        private void LoadAdministrators()
        {
            try
            {
                var admins = userController.GetAllAdministrators();
                cmbAdministrators.Items.Clear();

                foreach (var admin in admins)
                {
                    // Don't show current admin in the list
                    if (admin.Administrator_ID != currentAdminId)
                    {
                        cmbAdministrators.Items.Add(new AdminItem
                        {
                            Id = admin.Administrator_ID,
                            Name = $"{admin.First_name} {admin.Last_name}",
                            Username = admin.Username
                        });
                    }
                }

                if (cmbAdministrators.Items.Count == 0)
                {
                    MessageBox.Show("No other administrators available.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnVote.Enabled = false;
                }
                else
                {
                    cmbAdministrators.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading administrators: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbAdministrators_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAdministrators.SelectedItem != null)
            {
                var selectedAdmin = (AdminItem)cmbAdministrators.SelectedItem;
                lblVoteCount.Text = "Select 'Vote for Deletion' to register your vote.";
            }
        }

        private void BtnVote_Click(object sender, EventArgs e)
        {
            if (cmbAdministrators.SelectedItem == null)
            {
                MessageBox.Show("Please select an administrator to vote for deletion.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedAdmin = (AdminItem)cmbAdministrators.SelectedItem;

            var result = MessageBox.Show(
                $"Are you sure you want to vote to delete administrator:\n{selectedAdmin.Name} ({selectedAdmin.Username})?",
                "Confirm Vote",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                bool voted = adminController.VoteToDeleteAdmin(selectedAdmin.Id, currentAdminId);

                if (voted)
                {
                    MessageBox.Show("✓ Vote recorded successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Check if enough votes to delete
                    bool deleted = adminController.TryDeleteAdmin(selectedAdmin.Id);

                    if (deleted)
                    {
                        MessageBox.Show(
                            $"Administrator {selectedAdmin.Name} has been removed from the system!\n" +
                            "The required 3 votes have been reached.",
                            "Administrator Deleted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Your vote has been recorded. More votes are needed to delete this administrator.",
                            "Vote Recorded",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        LoadAdministrators(); // Refresh the list
                    }
                }
                else
                {
                    MessageBox.Show("You have already voted for this administrator's deletion.",
                        "Already Voted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing vote: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Helper class for ComboBox items
        private class AdminItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Username { get; set; }

            public override string ToString()
            {
                return $"{Name} ({Username})";
            }
        }
    }
}