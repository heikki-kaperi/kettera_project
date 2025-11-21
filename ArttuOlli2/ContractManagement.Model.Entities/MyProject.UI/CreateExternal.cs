using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class CreateExternal : Form
    {
        private UserController _userController; // Controller tietokantaa varten

        public CreateExternal(UserController userController)
        {
            InitializeComponent();
            _userController = userController;
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            // Haetaan tiedot tekstikentistä
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string company = txtCompany.Text.Trim(); // Jos sinulla on yrityskenttä

            // Tarkistetaan, että kaikki kentät on täytetty
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(company))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Yritetään luoda käyttäjä tietokantaan
                bool success = _userController.CreateExternalUser(firstName, lastName, company, email, username, password);

                if (success)
                {
                    MessageBox.Show("External user created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Suljetaan lomake ja palataan Administrator-näkymään
                }
                else
                {
                    MessageBox.Show("Failed to create external user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
