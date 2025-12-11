using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContractManagement.Controller;


namespace MyProject.UI
{
    public partial class Form1 : Form
    {
        private UserController _userController;
        private string selectedUserType;

        public Form1()
        {
            InitializeComponent();  // Tämä kutsuu Designerin InitializeComponent
            _userController = new UserController();

            // Alussa piilotetaan login-kentät
            txtUsername.Visible = false;
            txtPassword.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
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

            // Piilotetaan valintapainikkeet
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;

            // Näytetään login-kentät
            txtUsername.Visible = true;
            txtPassword.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (selectedUserType == "Administrator")
            {
                var admin = _userController.LoginAdministrator(username, password);
                if (admin != null)
                    MessageBox.Show("Administrator login successful!");
                else
                    MessageBox.Show("Invalid administrator credentials.");
            }
            else if (selectedUserType == "Internal")
            {
                var user = _userController.LoginInternalUser(username, password);
                if (user != null)
                    MessageBox.Show("Internal user login successful!");
                else
                    MessageBox.Show("Invalid internal user credentials.");
            }
            else if (selectedUserType == "External")
            {
                var user = _userController.LoginExternalUser(username, password);
                if (user != null)
                    MessageBox.Show("External user login successful!");
                else
                    MessageBox.Show("Invalid external user credentials.");
            }
        }

        private void txtUsername_Click(object sender, EventArgs e) => txtUsername.Text = "";
        private void txtPassword_Click(object sender, EventArgs e) => txtPassword.Text = "";
    }
}

