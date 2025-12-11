using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class InviteInternalForm : Form
    {
        private ContractController controller;

        public InviteInternalForm()
        {
            InitializeComponent();
            controller = new ContractController();
        }

        private void InviteInternalForm_Load(object sender, EventArgs e)
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

            if (!int.TryParse(txtUserId.Text.Trim(), out int userId))
            {
                MessageBox.Show("Anna kelvollinen käyttäjä-ID!", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool hasApprovalRights = chkApproval.Checked;

            Contract selectedContract = cbContracts.SelectedItem as Contract;
            bool success = controller.InviteInternalReviewer(selectedContract.Contract_NR, userId, hasApprovalRights);

            if (success)
                MessageBox.Show("Käyttäjä kutsuttu onnistuneesti!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Kutsuminen epäonnistui.", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
