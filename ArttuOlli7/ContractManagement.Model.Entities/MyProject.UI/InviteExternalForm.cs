using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class InviteExternalForm : Form
    {
        private ContractController controller;

        public InviteExternalForm()
        {
            InitializeComponent();
            controller = new ContractController();
        }

        private void InviteExternalForm_Load(object sender, EventArgs e)
        {
            List<Contract> contracts = controller.GetAllContracts();
            cbContracts.DataSource = contracts;
            cbContracts.DisplayMember = "Company_name";
            cbContracts.ValueMember = "Contract_NR";
            cbContracts.SelectedIndex = -1;
        }

        private void btnInvite_Click(object sender, EventArgs e)
        {
            if (cbContracts.SelectedItem == null)
            {
                MessageBox.Show("Valitse sopimus ensin!", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtExternalUserId.Text.Trim(), out int extUserId))
            {
                MessageBox.Show("Anna kelvollinen ulkoisen käyttäjän ID!", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Contract selectedContract = cbContracts.SelectedItem as Contract;
            bool success = controller.InviteExternalUser(selectedContract.Contract_NR, extUserId);

            if (success)
                MessageBox.Show("Ulkoinen käyttäjä kutsuttu onnistuneesti!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Kutsuminen epäonnistui.", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
