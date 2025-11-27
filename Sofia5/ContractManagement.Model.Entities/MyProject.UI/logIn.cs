using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class logIn : Form
    {
        private UserController _userController;
        private string selectedUserType;

        public logIn()
        {
            InitializeComponent();
            _userController = new UserController();
            // Piilotetaan login-kentät aluksi
            txtUsername.Visible = false;
            txtPassword.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            // Login-nappi pois käytöstä
            button4.Enabled = false;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            ShowLoginFields("Administrator");
        }

        private void btnInternal_Click(object sender, EventArgs e)
        {
            ShowLoginFields("Internal");
        }

        private void btnExternal_Click(object sender, EventArgs e)
        {
            ShowLoginFields("External");
        }

        private void ShowLoginFields(string userType)
        {
            selectedUserType = userType;
            // Näytetään kentät
            txtUsername.Visible = true;
            txtPassword.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            // Login-nappi aktiiviseksi
            button4.Enabled = true;
            // Piilotetaan roolinapit
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (selectedUserType == "Administrator")
            {
                var admin = _userController.LoginAdministrator(username, password);
                if (admin != null)
                {
                    MessageBox.Show("Administrator login successful!");
                    // Avaa Administrator-ikkuna
                    Administrator adminForm = new Administrator();
                    adminForm.Show();
                    // Piilota login-ikkuna
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid administrator credentials.");
                }
            }
            else if (selectedUserType == "Internal")
            {
                var user = _userController.LoginInternalUser(username, password);
                if (user != null)
                {
                    MessageBox.Show("Internal user login successful!");
                    // Avaa Internal.cs -lomake
                    Internal internalForm = new Internal();
                    internalForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid internal user credentials.");
                }
            }
            else if (selectedUserType == "External")
            {
                var user = _userController.LoginExternalUser(username, password);
                if (user != null)
                {
                    MessageBox.Show("External user login successful!");
                    // LISÄTTY: Avaa External-lomake
                    External externalForm = new External(user);
                    externalForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid external user credentials.");
                }
            }
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
    }
}