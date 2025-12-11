using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class ViewContractDetails : Form
    {
        private ContractController _contractController;
        private int _contractId;

        public ViewContractDetails(int contractId, ContractController contractController)
        {
            InitializeComponent();
            _contractId = contractId;
            _contractController = contractController;
            LoadContractDetails();
        }

        private void LoadContractDetails()
        {
            try
            {
                var contract = _contractController.GetContractById(_contractId);

                if (contract == null)
                {
                    MessageBox.Show("Contract not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Näytä sopimuksen perustiedot
                lblContractInfo.Text = $"Contract: {contract.Company_name}\n" +
                                      $"Created: {contract.Created_date:yyyy-MM-dd}\n" +
                                      $"Approved: {(contract.Approved ? "Yes" : "No")}";

                // Lataa sopimuksen lohkot
                var blocks = _contractController.GetBlocksByContract(_contractId);
                dataGridViewBlocks.DataSource = blocks;
                dataGridViewBlocks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading contract details: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}