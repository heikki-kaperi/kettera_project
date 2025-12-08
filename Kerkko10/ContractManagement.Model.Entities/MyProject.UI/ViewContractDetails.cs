using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class ViewContractDetails : Form
    {
        private ContractController _contractController;
        private int _contractId;
        private ExternalUser _currentUser;
        private bool _accessDenied = false; // Lisää tämä

        public ViewContractDetails(int contractId, ContractController contractController, ExternalUser currentUser = null)
        {
            InitializeComponent();
            _contractId = contractId;
            _contractController = contractController;
            _currentUser = currentUser;
            LoadContractDetails();
        }

        private void LoadContractDetails()
        {
            try
            {
                // Jos ulkoinen käyttäjä, tarkista kutsu
                if (_currentUser != null)
                {
                    if (!_contractController.IsExternalUserInvitedToContract(_contractId, _currentUser.Ext_User_ID))
                    {
                        MessageBox.Show("Access denied. You are not invited to this contract.",
                            "Access Denied",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        _accessDenied = true; // Merkitse vain
                        return; // Älä sulje vielä
                    }
                }

                var contract = _contractController.GetContractById(_contractId);
                if (contract == null)
                {
                    MessageBox.Show("Contract not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _accessDenied = true;
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
                _accessDenied = true;
            }
        }

        // Lisää tämä tapahtumankäsittelijä
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Sulje lomake heti näyttämisen jälkeen jos pääsy evätty
            if (_accessDenied)
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}