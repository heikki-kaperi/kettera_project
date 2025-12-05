using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class External : Form
    {
        private ExternalUser _currentUser;
        private ContractController _contractController;

        public External(ExternalUser user)
        {
            InitializeComponent();
            _currentUser = user;
            _contractController = new ContractController();

            lblTitle.Text = $"EXTERNAL USER MENU - {_currentUser.Username}";
        }

        private void btnViewContracts_Click(object sender, EventArgs e)
        {
            ViewMyContracts viewForm = new ViewMyContracts(_currentUser, _contractController);
            viewForm.ShowDialog();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            // Käytetään yksinkertaista input-dialogia
            string contractNr = ShowInputDialog("Enter Contract Number:", "View Contract Details");

            if (!string.IsNullOrEmpty(contractNr) && int.TryParse(contractNr, out int contractId))
            {
                ViewContractDetails detailsForm = new ViewContractDetails(contractId, _contractController);
                detailsForm.ShowDialog();
            }
            else if (!string.IsNullOrEmpty(contractNr))
            {
                MessageBox.Show("Invalid contract number.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnComment_Click(object sender, EventArgs e)
        {
            string contractNr = ShowInputDialog("Enter Contract Number:", "Comment on Contract");

            if (!string.IsNullOrEmpty(contractNr) && int.TryParse(contractNr, out int contractId))
            {
                CommentOnContract commentForm = new CommentOnContract(
                    contractId,
                    _currentUser.Ext_User_ID,
                    "External");
                commentForm.ShowDialog();
            }
            else if (!string.IsNullOrEmpty(contractNr))
            {
                MessageBox.Show("Invalid contract number.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide(); // Piilota Administrator-ikkuna

                // Avaa kirjautumislomake uudelleen (vaihda Login nimeen omaksesi)
                logIn loginForm = new logIn();
                loginForm.ShowDialog();

                this.Close(); // Sulje Administrator-lomake lopullisesti
            }
        }

        // Oma InputBox-metodi (ei tarvitse Microsoft.VisualBasic referenssiä)
        private string ShowInputDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 350 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 350 };
            Button confirmation = new Button() { Text = "OK", Left = 220, Width = 70, Top = 80, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Cancel", Left = 300, Width = 70, Top = 80, DialogResult = DialogResult.Cancel };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}