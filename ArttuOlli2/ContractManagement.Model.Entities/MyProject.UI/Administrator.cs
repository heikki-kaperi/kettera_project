using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class Administrator : Form
    {
        private UserController _userController;

        public Administrator()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateInternal createForm = new CreateInternal(_userController);
            createForm.ShowDialog();
        }

        private void createExternalBtn_Click(object sender, EventArgs e)
        {
            CreateExternal createForm = new CreateExternal(_userController);
            createForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewInternalUsers viewForm = new ViewInternalUsers(_userController);
            viewForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewExternalUsers viewForm = new ViewExternalUsers(_userController);
            viewForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DeleteInternalUsers deleteForm = new DeleteInternalUsers(_userController);
            deleteForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DeleteExternalUsers deleteForm = new DeleteExternalUsers(_userController);
            deleteForm.ShowDialog();
        }
    }
}