using AVT.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVT
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == string.Empty ||
                txtPassword.Text.Trim() == string.Empty ||
                txtConfirmPassword.Text.Trim() == string.Empty ||
                txtConfirmPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please fill all fields !", "AVT Add User");
                return;
            }
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                MessageBox.Show("Please ensure both passwords are same !", "AVT Add User");
                return;
            }
            ConnectionManager oConnectionManager = new ConnectionManager();
            SqlConnection connActiveConnection = oConnectionManager.GetActiveConnection;
            DBManager.GetInstance.AddUser(txtUserName.Text, txtConfirmPassword.Text, txtEmail.Text, connActiveConnection);
            if (connActiveConnection.State != ConnectionState.Closed)
            {
                connActiveConnection.Close();
            }
        }
    }
}
