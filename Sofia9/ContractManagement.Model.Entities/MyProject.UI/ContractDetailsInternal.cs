using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class ContractDetailsInternal : Form
    {
        private ContractController _controller;
        private bool _isLoading = true; // estää SelectedIndexChanged heti alussa

        public ContractDetailsInternal(ContractController controller)
        {
            InitializeComponent();
            _controller = controller;

            LoadContracts();
        }

        private void LoadContracts()
        {
            _isLoading = true;

            List<Contract> contracts = _controller.GetAllContracts();

            cbContracts.DataSource = contracts;
            cbContracts.DisplayMember = "Company_name";
            cbContracts.ValueMember = "Contract_NR";
            cbContracts.SelectedIndex = -1;

            _isLoading = false;
        }

        private void cbContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            if (cbContracts.SelectedIndex < 0 || cbContracts.SelectedValue == null)
                return;

            if (!int.TryParse(cbContracts.SelectedValue.ToString(), out int contractId))
                return; // ei virheilmoitusta

            LoadContract(contractId);
        }

        private void LoadContract(int contractId)
        {
            Contract contract = _controller.GetContractById(contractId);
            if (contract == null) return;

            txtContractName.Text = contract.Company_name;
            txtCreatorId.Text = contract.The_Creator.ToString();
            txtCreatedDate.Text = contract.Created_date.ToString("yyyy-MM-dd HH:mm");
            txtContractStatus.Text = contract.Approved ? "Approved" : "Not Approved";

            var blocks = _controller.GetContractBlocks(contractId);
            lstBlocks.DataSource = null;
            lstBlocks.DataSource = blocks;
            lstBlocks.DisplayMember = "Contract_text";
            lstBlocks.ValueMember = "Contract_Block_NR";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
