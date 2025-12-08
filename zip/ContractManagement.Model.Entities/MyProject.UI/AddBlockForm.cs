using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;
using System.Collections.Generic;

namespace MyProject.UI
{
    public partial class AddBlockForm : Form
    {
        private ContractController controller;

        public AddBlockForm()
        {
            InitializeComponent();
            controller = new ContractController();
        }

        private void AddBlockForm_Load(object sender, EventArgs e)
        {
            // Täytetään sopimukset comboboxiin
            var contracts = controller.GetAllContracts();
            cbContracts.DataSource = contracts;
            cbContracts.DisplayMember = "Company_name";
            cbContracts.ValueMember = "Contract_NR"; // ID sarake

            // Täytetään alkuperäiset lohkot comboboxiin
            var originalBlocks = controller.GetAllOriginalBlocks(); // lisää tämä metodi ContractControlleriin jos puuttuu
            cbOriginalBlocks.DataSource = originalBlocks;
            cbOriginalBlocks.DisplayMember = "Contract_text";
            cbOriginalBlocks.ValueMember = "Org_Cont_ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbContracts.SelectedValue == null || cbOriginalBlocks.SelectedValue == null)
            {
                MessageBox.Show("Please select a contract and an original block!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int contractNr = (int)cbContracts.SelectedValue;
            int originalBlockId = (int)cbOriginalBlocks.SelectedValue;

            bool result = controller.AddBlockToContract(contractNr, originalBlockId);

            if (result)
                MessageBox.Show("Block added successfully!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Failed to add block.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
