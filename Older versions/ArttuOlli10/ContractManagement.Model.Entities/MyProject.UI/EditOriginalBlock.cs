using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class EditOriginalBlock : Form
    {
        private BlockController _blockController;
        private OriginalContractBlock _selectedBlock;

        public EditOriginalBlock()
        {
            InitializeComponent();
            _blockController = new BlockController();
            LoadOriginalBlocks();
        }

        private void LoadOriginalBlocks()
        {
            try
            {
                var blocks = _blockController.GetAllOriginalBlocks();

                if (blocks.Count == 0)
                {
                    MessageBox.Show("No original blocks found.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cbBlocks.DataSource = blocks;
                cbBlocks.DisplayMember = "Contract_text"; // Näyttää tekstin
                cbBlocks.ValueMember = "Org_Cont_ID";     // Käyttää ID:tä
                cbBlocks.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading blocks: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbBlocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBlocks.SelectedItem == null)
            {
                ClearFields();
                return;
            }

            _selectedBlock = cbBlocks.SelectedItem as OriginalContractBlock;

            if (_selectedBlock != null)
            {
                // Näytä valitun blockin tiedot
                txtBlockId.Text = _selectedBlock.Org_Cont_ID.ToString();
                txtCategory.Text = _selectedBlock.Category_name;
                txtBlockText.Text = _selectedBlock.Contract_text;

                // Lataa myös kategoriat dropdown-listaan
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _blockController.GetAllCategories();
                cbCategories.DataSource = categories;
                cbCategories.DisplayMember = "Category_name";
                cbCategories.ValueMember = "Category_name";

                // Aseta nykyinen kategoria valituksi
                if (_selectedBlock != null)
                {
                    cbCategories.SelectedValue = _selectedBlock.Category_name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedBlock == null)
            {
                MessageBox.Show("Please select a block to update.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBlockText.Text))
            {
                MessageBox.Show("Block text cannot be empty.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbCategories.SelectedItem == null)
            {
                MessageBox.Show("Please select a category.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Päivitä blockin tiedot
                _selectedBlock.Category_name = cbCategories.SelectedValue.ToString();
                _selectedBlock.Contract_text = txtBlockText.Text;

                // Kutsu controllerin kautta UpdateOriginalBlock
                bool success = UpdateOriginalBlockViaController(_selectedBlock);

                if (success)
                {
                    MessageBox.Show("✓ Block updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Lataa blockit uudelleen näyttääksesi muutokset
                    LoadOriginalBlocks();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("✗ Failed to update block.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating block: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool UpdateOriginalBlockViaController(OriginalContractBlock block)
        {
            // Tämä kutsuu BlockController:ia, joka kutsuu DAL:ia
            // Sinun täytyy lisätä UpdateOriginalBlock metodi BlockControlleriin
            return _blockController.UpdateOriginalBlock(block);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            txtBlockId.Clear();
            txtCategory.Clear();
            txtBlockText.Clear();
            cbCategories.DataSource = null;
            _selectedBlock = null;
        }
    }
}