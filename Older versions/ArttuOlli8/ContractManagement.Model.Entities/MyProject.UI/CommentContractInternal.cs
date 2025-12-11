using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace MyProject.UI
{
    public partial class CommentContractInternal : Form
    {
        private ContractController _contractController;
        private CommentController _commentController;

        public CommentContractInternal()
        {
            InitializeComponent();
            _contractController = new ContractController();
            _commentController = new CommentController();
            LoadContracts();
        }

        // Lataa kaikki sopimukset dropdowniin
        private void LoadContracts()
        {
            var contracts = _contractController.GetAllContracts();
            cbContracts.DataSource = contracts;
            cbContracts.DisplayMember = "Company_name";
            cbContracts.ValueMember = "Contract_NR";
            cbContracts.SelectedIndex = -1;
        }

        // Kun valitaan sopimus
        private void cbContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbContracts.SelectedIndex < 0)
                return;

            // haetaan valittu Contract-olio suoraan
            Contract selectedContract = cbContracts.SelectedItem as Contract;
            if (selectedContract == null)
                return;

            int contractId = selectedContract.Contract_NR;

            LoadComments(contractId);
        }

        // Lataa kommentit valitulle sopimukselle
        private void LoadComments(int contractId)
        {
            var comments = _commentController.GetCommentsForContract(contractId);
            lstComments.DataSource = null;
            lstComments.DataSource = comments;
            lstComments.DisplayMember = "Comment_text"; // property Comment luokassa
        }

        // Lisää uusi kommentti
        private void btnAddComment_Click(object sender, EventArgs e)
        {
            if (cbContracts.SelectedValue == null)
            {
                MessageBox.Show("Select a contract first!");
                return;
            }

            string text = txtNewComment.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Comment cannot be empty!");
                return;
            }

            int contractId = Convert.ToInt32(cbContracts.SelectedValue);

            // UserId ja userType voidaan kovakoodata esim. Internal
            bool success = _commentController.AddComment(contractId, null, 1, "Internal", text);

            if (success)
            {
                LoadComments(contractId);
                txtNewComment.Clear();
            }
            else
            {
                MessageBox.Show("Failed to add comment.");
            }
        }

        // Sulje lomake
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
