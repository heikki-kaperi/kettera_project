using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;
using System.Collections.Generic;

namespace MyProject.UI
{
    public partial class RemoveBlockForm : Form
    {
        private ContractController controller;

        public RemoveBlockForm()
        {
            InitializeComponent();
            controller = new ContractController();
        }

        private void RemoveBlockForm_Load(object sender, EventArgs e)
        {
            // Täytetään sopimukset comboboxiin
            var contracts = controller.GetAllContracts();
            cbContracts.DataSource = contracts;
            cbContracts.DisplayMember = "Company_name";
            cbContracts.ValueMember = "Contract_NR";

            cbBlocks.DataSource = null;
        }

        private void cbContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbContracts.SelectedValue == null)
                return;

            int contractNr;

            try
            {
                contractNr = Convert.ToInt32(cbContracts.SelectedValue);
            }
            catch
            {
                return; // Jos SelectedValue ei ole kelvollinen, lopetetaan
            }

            var blocks = controller.GetBlocksByContract(contractNr);

            cbBlocks.DataSource = blocks;
            cbBlocks.DisplayMember = "Contract_text";
            cbBlocks.ValueMember = "Contract_Block_NR";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (cbContracts.SelectedValue == null || cbBlocks.SelectedValue == null)
            {
                MessageBox.Show("Please select a contract and a block!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int contractNr = (int)cbContracts.SelectedValue;
            int blockId = (int)cbBlocks.SelectedValue;

            bool result = controller.RemoveBlockFromContract(contractNr, blockId);

            if (result)
                MessageBox.Show("Block removed successfully!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Failed to remove block.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Päivitä lohkot listassa
            cbContracts_SelectedIndexChanged(null, null);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
