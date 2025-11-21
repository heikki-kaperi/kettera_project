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
    }
}
