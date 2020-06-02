using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AVT.Manager
{
    public class DBManager
    {
        private static DBManager oDBManager;

        public static DBManager GetInstance
        {
            get
            {
                if (oDBManager == null)
                {
                    oDBManager = new DBManager();

                }
                return oDBManager;
            }
        }

        public DataSet GetUserDetails(String strUserName, string strPassword, SqlConnection connActiveConnection)
        {

            string strQuery = "SELECT * FROM userdetails WHERE username='" + strUserName + "' AND password='" + strPassword + "'";

            DataSet dsCurrent = new DataSet();
            SqlDataAdapter daUserDetails = new SqlDataAdapter();
            SqlCommand cmdUSerDetails = new SqlCommand(strQuery, connActiveConnection);
            daUserDetails.SelectCommand = cmdUSerDetails; // Set the select command for the DataAdapter
            daUserDetails.Fill(dsCurrent); // Fill the DataSet with the DataAdapter
            
            return dsCurrent;

        }

        public DataSet GetTimeTable(string strEmail, SqlConnection connActiveConnection)
        {

            string strQuery = "SELECT * FROM timetable WHERE email='" + strEmail + "'";

            DataSet dsCurrent = new DataSet();
            SqlDataAdapter daTimeTable = new SqlDataAdapter();
            SqlCommand cmdUSerDetails = new SqlCommand(strQuery, connActiveConnection);
            daTimeTable.SelectCommand = cmdUSerDetails; // Set the select command for the DataAdapter
            daTimeTable.Fill(dsCurrent); // Fill the DataSet with the DataAdapter

            return dsCurrent;

        }

        public DataSet GetAllUsers(SqlConnection connActiveConnection)
        {

            string strQuery = "SELECT * FROM userdetails";

            DataSet dsCurrent = new DataSet();
            SqlDataAdapter daUserDetails = new SqlDataAdapter();
            SqlCommand cmdUSerDetails = new SqlCommand(strQuery, connActiveConnection);
            daUserDetails.SelectCommand = cmdUSerDetails; // Set the select command for the DataAdapter
            daUserDetails.Fill(dsCurrent); // Fill the DataSet with the DataAdapter

            return dsCurrent;

        }

        public int AddUser(String strUserName, string strPassword,string  strClientMail, SqlConnection connActiveConnection)
        {

            int iAffectedRows = 0;
            int isAdmin = 0;
            string strAddUserQuery = "INSERT INTO userdetails VALUES('" + strUserName + "','" + strPassword + "','" + strClientMail + "','" + isAdmin + "')";
            SqlCommand cmdAddUser = new SqlCommand(strAddUserQuery, connActiveConnection);

            try
            {
                iAffectedRows =  cmdAddUser.ExecuteNonQuery();

            }
            catch (Exception)
            {
            }

            return iAffectedRows;

        }

        public string GetConnectionString()
        {
            string strDBServer = ConfigurationManager.AppSettings["DBSERVER"].ToString();
            string strDBName = ConfigurationManager.AppSettings["DBNAME"].ToString();
            string strDBUser = ConfigurationManager.AppSettings["DBUSER"].ToString();


            return "Data Source = " + strDBServer + "; Initial Catalog=" + strDBName + "; User Id=" + strDBUser + "; Password=" + DecryptPassword() + ";Integrated Security=false";

        }





        private string DecryptPassword()
        {
            string strDbPassword = ConfigurationManager.AppSettings["DBPASSWORD"].ToString();
            string strKeyVal = ConfigurationManager.AppSettings["KEY"].ToString();
            string strIV = ConfigurationManager.AppSettings["INTIVECTOR"].ToString();
            string strClearTextPassword = string.Empty;
              

            byte[] baPasswordCipher = Convert.FromBase64String(strDbPassword);

            HashAlgorithm hashKey = MD5.Create();      

            using (AesManaged aes = new AesManaged())
            {
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor(hashKey.ComputeHash(Encoding.Unicode.GetBytes(strKeyVal)), Convert.FromBase64String(strIV));
                using (MemoryStream ms = new MemoryStream(baPasswordCipher))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            strClearTextPassword = reader.ReadToEnd();
                    }
                }
            }
            return strClearTextPassword;

        }

    }
}
