using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class CopyBlock : Form
    {
        private BlockController _blockController;
        private int _userId;

        public CopyBlock(int userId)
        {
            InitializeComponent();
            _blockController = new BlockController();
            _userId = userId;
            LoadAllBlocks();
        }

        private void LoadAllBlocks()
        {
            try
            {
                var blocks = _blockController.GetAllOriginalBlocks();

                if (blocks.Count == 0)
                {
                    MessageBox.Show("No blocks found.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dataGridViewBlocks.DataSource = blocks;
                dataGridViewBlocks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading blocks: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewBlocks_Click(object sender, EventArgs e)
        {
            ViewOriginalBlocks viewForm = new ViewOriginalBlocks();
            viewForm.ShowDialog();
        }

        private void dataGridViewBlocks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kun käyttäjä klikkaa riviä, täytä ID automaattisesti
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewBlocks.Rows[e.RowIndex];
                if (row.Cells["Org_Cont_ID"].Value != null)
                {
                    txtBlockId.Text = row.Cells["Org_Cont_ID"].Value.ToString();
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBlockId.Text))
            {
                MessageBox.Show("Please enter a Block ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtBlockId.Text, out int blockId))
            {
                MessageBox.Show("Invalid Block ID. Please enter a valid number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _blockController.CopyOriginalBlock(blockId, _userId);

                if (success)
                {
                    MessageBox.Show("Block copied successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllBlocks(); // Päivitä lista näyttämään kopio
                    txtBlockId.Clear();
                }
                else
                {
                    MessageBox.Show("Failed to copy block. Block ID may not exist.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}