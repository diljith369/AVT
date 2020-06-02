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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            GetAllUsers();
        }

        private void GetAllUsers()
        {
            ConnectionManager oConnectionManager = new ConnectionManager();
            SqlConnection connActiveConnection = oConnectionManager.GetActiveConnection;
            DataSet dsUserDetails = DBManager.GetInstance.GetAllUsers(connActiveConnection);
            gvwAdminPage.DataSource = dsUserDetails.Tables[0];
            if (connActiveConnection.State != ConnectionState.Closed)
            {
                connActiveConnection.Close();
            }
        }
    }
}
