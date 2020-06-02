using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVT.Manager
{
    public class ConnectionManager
    {
        private  SqlConnection sqlCurrentConnection;

        public  ConnectionManager ()
        {
           
        }

        public SqlConnection GetActiveConnection
        {
            get {
                sqlCurrentConnection = new SqlConnection(DBManager.GetInstance.GetConnectionString());
                sqlCurrentConnection.Open();
                return sqlCurrentConnection; 
            }
        }

      
    }
}
