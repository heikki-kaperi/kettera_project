using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class ViewOriginalBlocks : Form
    {
        private BlockController _blockController;

        public ViewOriginalBlocks()
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}