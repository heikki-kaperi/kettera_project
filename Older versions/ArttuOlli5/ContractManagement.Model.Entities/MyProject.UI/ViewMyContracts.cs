using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class ViewMyContracts : Form
    {
        private ExternalUser _currentUser;
        private ContractController _contractController;

        public ViewMyContracts(ExternalUser user, ContractController contractController)
        {
            InitializeComponent();
            _currentUser = user;
            _contractController = contractController;
            LoadContracts();
        }

        private void LoadContracts()
        {
            try
            {
                var contracts = _contractController.GetContractsByExternalUser(_currentUser.Ext_User_ID);

                if (contracts.Count == 0)
                {
                    MessageBox.Show("No contracts found.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dataGridViewContracts.DataSource = contracts;
                dataGridViewContracts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading contracts: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}