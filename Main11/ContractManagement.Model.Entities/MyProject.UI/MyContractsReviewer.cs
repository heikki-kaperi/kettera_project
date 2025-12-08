using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class MyContractsReviewer : Form
    {
        private ContractController controller = new ContractController();
        private InternalUser _currentUser; // Lisää tämä

        // Muokattu konstruktori ottamaan InternalUser parametrina
        public MyContractsReviewer(InternalUser currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            LoadContracts();
        }

        private void LoadContracts()
        {
            // Hae vain sopimukset joihin käyttäjä on kutsuttu revieweriksi
            List<Contract> contracts = controller.GetContractsToReviewByInternalUser(_currentUser.Int_User_ID);

            if (contracts.Count == 0)
            {
                MessageBox.Show("No contracts assigned for review.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            cbContracts.DataSource = contracts;
            cbContracts.DisplayMember = "Company_name"; // Näytetään nimi
            cbContracts.ValueMember = "Contract_NR";    // Käytetään ID:tä
            cbContracts.SelectedIndex = -1;
        }

        private void cbContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbContracts.SelectedItem == null)
            {
                lstBlocks.DataSource = null; // Tyhjennetään laatikko
                return;
            }

            Contract selectedContract = cbContracts.SelectedItem as Contract;
            if (selectedContract == null)
                return;

            int contractNr = selectedContract.Contract_NR;
            List<ContractBlock> blocks = controller.GetContractBlocks(contractNr);

            lstBlocks.DataSource = blocks;
            lstBlocks.DisplayMember = "Contract_text";    // Näytetään teksti
            lstBlocks.ValueMember = "Contract_Block_NR"; // Käytetään ID:tä
        }
    }
}