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
            button4.Visible= false;
            button4.Enabled = false;
            //Login otsikot piiloon
            labelAdmin.Visible = false;
            labelInternal.Visible = false; 
            labelExternal.Visible = false;
            //Takaisin nappi piiloon
            buttonBack.Visible = false;
            //Show Password -checkbox piiloon
            chkShowPassword.Visible = false;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            ShowLoginFields("Administrator");
            labelAdmin.Visible = true;
        }

        private void btnInternal_Click(object sender, EventArgs e)
        {
            ShowLoginFields("Internal");
            labelInternal.Visible = true;
        }

        private void btnExternal_Click(object sender, EventArgs e)
        {
            ShowLoginFields("External");
            labelExternal.Visible = true;
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
            button4.Visible = true;
            button4.Enabled = true;
            // Piilotetaan roolinapit
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            // Näytetään takaisin-nappi
            buttonBack.Visible = true;
            //Piilotetaan Exit-nappi
            buttonExit.Visible = false;
            //Näytetään Show Password -checkbox
            chkShowPassword.Visible = true;
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
                    Internal internalForm = new Internal(user); // ← Tämä rivi tärkeä!
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void labelExternal_Click(object sender, EventArgs e)
        {

        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            // Piilota login-kentät
            txtUsername.Visible = false;
            txtPassword.Visible = false;
            label1.Visible = false;
            label2.Visible = false;

            // Tyhjennetään kentät
            txtUsername.Text = "";
            txtPassword.Text = "";

            // Piilotetaan login-nappi
            button4.Visible = false;
            button4.Enabled = false;

            // Piilota käyttäjätyyppiotsikot
            labelAdmin.Visible = false;
            labelInternal.Visible = false;
            labelExternal.Visible = false;

            // Näytä alkuperäiset roolinapit
            button1.Visible = true; // Admin
            button2.Visible = true; // Internal
            button3.Visible = true; // External

            // Näytä Exit-nappi
            buttonExit.Visible = true;

            // Piilota takaisin-nappi
            buttonBack.Visible = false;

            // Piilota Show Password -checkbox
            chkShowPassword.Visible = false;

            // Nollaa valittu käyttäjätyyppi
            selectedUserType = null;
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Jos CheckBox on valittuna, näytä salasana
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }


    }
}