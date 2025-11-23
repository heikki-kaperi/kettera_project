using System;
using System.Windows.Forms;
using ContractManagement.Controller;

namespace MyProject.UI
{
    public partial class Administrator : Form
    {
        private UserController _userController; // Kenttä controllerille

        public Administrator()
        {
            InitializeComponent();
            _userController = new UserController(); // Alustetaan controller
        }

        private void createExternalBtn_Click(object sender, EventArgs e)
        {
            // Avataan CreateExternal-lomake
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
        private void Administrator_Load(object sender, EventArgs e)
        {

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
