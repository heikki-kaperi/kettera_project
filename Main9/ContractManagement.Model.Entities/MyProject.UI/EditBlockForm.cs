using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class EditBlockForm : Form
    {
        private ContractController controller;

        public EditBlockForm()
        {
            InitializeComponent();
            controller = new ContractController();
        }

        private void EditBlockForm_Load(object sender, EventArgs e)
        {
            LoadContracts();
        }

        private void LoadContracts()
        {
            List<Contract> contracts = controller.GetAllContracts();

            cbContracts.DataSource = contracts;
            cbContracts.DisplayMember = "Company_name";
            cbContracts.ValueMember = "Contract_NR"; // käytetään Contract_NR ID:nä
            cbContracts.SelectedIndex = -1;
        }

        private void cbContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbContracts.SelectedItem == null)
            {
                cbBlocks.DataSource = null;
                txtBlockText.Clear();
                return;
            }

            // oletetaan, että cbContracts.DataSource on List<Contract>
            Contract selectedContract = cbContracts.SelectedItem as Contract;
            if (selectedContract == null)
            {
                MessageBox.Show("Valittu sopimus ei kelpaa!", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int contractNr = selectedContract.Contract_NR; // käytetään oikeaa saraketta ID:lle
            List<ContractBlock> blocks = controller.GetContractBlocks(contractNr);

            cbBlocks.DataSource = blocks;
            cbBlocks.DisplayMember = "Contract_text"; // näytetään teksti
            cbBlocks.ValueMember = "Contract_Block_NR"; // käytetään ID:tä
            cbBlocks.SelectedIndex = -1;
            txtBlockText.Clear();
        }

        private void cbBlocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBlocks.SelectedIndex == -1)
            {
                txtBlockText.Clear();
                return;
            }

            ContractBlock block = cbBlocks.SelectedItem as ContractBlock;
            if (block != null)
            {
                txtBlockText.Text = block.Contract_text;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbBlocks.SelectedIndex == -1)
            {
                MessageBox.Show("Valitse muokattava block!", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ContractBlock block = cbBlocks.SelectedItem as ContractBlock;
            string newText = txtBlockText.Text.Trim();

            if (string.IsNullOrEmpty(newText))
            {
                MessageBox.Show("Blockin teksti ei voi olla tyhjä!", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = controller.EditBlockInContract(block.Contract_Block_NR, newText);
            if (success)
            {
                MessageBox.Show("Block päivitetty onnistuneesti!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Päivitetään ComboBox uudelleen
                cbContracts_SelectedIndexChanged(null, null);
            }
            else
            {
                MessageBox.Show("Blockin päivitys epäonnistui!", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
