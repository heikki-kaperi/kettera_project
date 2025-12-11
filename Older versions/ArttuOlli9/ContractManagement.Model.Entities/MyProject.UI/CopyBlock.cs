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
                // Haetaan ORIGINAL blokit
                var blocks = _blockController.GetAllOriginalBlocks();

                dataGridViewBlocks.DataSource = null; // Tyhjennä ensin
                dataGridViewBlocks.DataSource = blocks; // Lataa uusi data
                dataGridViewBlocks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
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
                MessageBox.Show("Invalid Block ID.", "Validation Error",
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
                    LoadAllBlocks();
                    txtBlockId.Clear();
                }
                else
                {
                    MessageBox.Show(
                        $"Failed to copy block ID {blockId}.\n\n" +
                        $"Check Output window (View → Output) for MySQL error details.\n\n" +
                        $"User ID: {_userId}",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception:\n\n{ex.Message}\n\n{ex.StackTrace}",
                    "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}