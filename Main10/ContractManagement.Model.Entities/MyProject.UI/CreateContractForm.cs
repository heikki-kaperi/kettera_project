using System;
using System.Windows.Forms;
using ContractManagement.Controller; // Varmista, että tämä namespace on oikein

namespace MyProject.UI
{
    public partial class CreateContractForm : Form
    {
        private ContractController contractController;

        public CreateContractForm()
        {
            InitializeComponent();
            contractController = new ContractController();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string contractName = txtContractName.Text.Trim();

            if (string.IsNullOrEmpty(contractName))
            {
                MessageBox.Show("Enter the contract name!", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Oletetaan, että The_Creator = 1, koska kirjautunut käyttäjä ei ole käytössä
            int newContractId = contractController.CreateContract(contractName, 1);

            if (newContractId > 0)
            {
                MessageBox.Show("Contract created successfully!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtContractName.Clear();
            }
            else
            {
                MessageBox.Show("Contract creation failed!", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateContractForm_Load(object sender, EventArgs e)
        {

        }
    }
}
