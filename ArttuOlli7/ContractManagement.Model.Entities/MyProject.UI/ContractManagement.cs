using System;
using System.Windows.Forms;

namespace MyProject.UI
{
    public partial class ContractManagement : Form
    {
        public ContractManagement()
        {
            InitializeComponent();
        }

        private void ContractManagement_Load(object sender, EventArgs e)
        {
        }

        private void createContractBtn_Click(object sender, EventArgs e)
        {
            CreateContractForm form = new CreateContractForm();
            form.ShowDialog();
        }

        private void viewMyContractsBtn_Click(object sender, EventArgs e)
        {
            ViewMyContractsForm form = new ViewMyContractsForm();
            form.ShowDialog();
        }

        private void addBlockBtn_Click(object sender, EventArgs e)
        {
            AddBlockForm form = new AddBlockForm();
            form.ShowDialog();
        }

        private void removeBlockBtn_Click(object sender, EventArgs e)
        {
            RemoveBlockForm form = new RemoveBlockForm();
            form.ShowDialog();
        }

        private void editBlockBtn_Click(object sender, EventArgs e)
        {
            EditBlockForm form = new EditBlockForm();
            form.ShowDialog();
        }

        private void inviteInternalBtn_Click(object sender, EventArgs e)
        {
            InviteInternalForm form = new InviteInternalForm();
            form.ShowDialog();
        }

        private void inviteExternalBtn_Click(object sender, EventArgs e)
        {
            InviteExternalForm form = new InviteExternalForm();
            form.ShowDialog();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
