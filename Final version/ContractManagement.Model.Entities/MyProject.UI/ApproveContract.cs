using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class ApproveContract : Form
    {
        private InternalUser _currentUser;
        private ApprovalController _approvalController = new ApprovalController();
        private ContractController _contractController = new ContractController();

        public ApproveContract(InternalUser user)
        {
            InitializeComponent();
            _currentUser = user;
            LoadContracts();
        }

        private void LoadContracts()
        {
            List<Contract> contracts = _contractController.GetContractsToReviewByInternalUser(_currentUser.Int_User_ID);
            cbContracts.DataSource = contracts;
            cbContracts.DisplayMember = "Company_name";
            cbContracts.ValueMember = "Contract_NR";
            cbContracts.SelectedIndex = -1;
        }

        private void cbContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbContracts.SelectedIndex < 0 || cbContracts.SelectedItem == null)
                return;

            // Castataan SelectedItem Contract-tyyppiin turvallisesti
            if (cbContracts.SelectedItem is Contract selectedContract)
            {
                txtContractName.Text = selectedContract.Company_name;

                // Jos haluat myös ID:n:
                int contractId = selectedContract.Contract_NR;

                // Tässä voit ladata muut tiedot tai kommentit
                // LoadComments(contractId);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (cbContracts.SelectedIndex < 0)
            {
                MessageBox.Show("Select a contract first!");
                return;
            }

            int contractId = Convert.ToInt32(cbContracts.SelectedValue);

            bool result = _approvalController.ApproveContract(contractId, _currentUser.Int_User_ID);
            if (result)
            {
                MessageBox.Show("Contract approved!");
            }
            else
            {
                MessageBox.Show("You are not authorized or contract is already approved.");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
