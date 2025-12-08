using System;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities; // Varmista, että Contract-luokka löytyy tästä

namespace MyProject.UI
{
    public partial class ViewMyContractsForm : Form
    {
        private ContractController contractController;

        public ViewMyContractsForm()
        {
            InitializeComponent();
            contractController = new ContractController();
        }

        private void ViewMyContractsForm_Load(object sender, EventArgs e)
        {
            LoadContracts();
        }

        private void LoadContracts()
        {
            listViewContracts.Items.Clear();

            var contracts = contractController.GetAllContracts(); // Haetaan kaikki sopimukset
            foreach (var contract in contracts)
            {
                ListViewItem item = new ListViewItem(contract.Contract_NR.ToString());
                item.SubItems.Add(contract.Company_name);
                item.SubItems.Add(contract.The_Creator.ToString());
                item.SubItems.Add(contract.Created_date.ToString("yyyy-MM-dd"));
                listViewContracts.Items.Add(item);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
