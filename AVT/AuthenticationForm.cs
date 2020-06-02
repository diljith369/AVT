using AVT.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AVT
{
    public partial class FormAuthentication : Form
    {
        byte[] IV;
        byte[] ciphertext;
        string vector, keyval, strciphertext;

        public FormAuthentication()
        {
            InitializeComponent();
            // IV = GenerateBitsOfRandomEntropy(16);
            keyval = "3wY(UKMb*qFyH0wC";
            IV = Convert.FromBase64String("o1Dqr9F3nnfGxMspO1c53A==");
            vector = Convert.ToBase64String(IV);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == string.Empty || txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please fill missing fields !", "AVT login validation");
                return;
            }

            GetUserDetails();

        }

        private void GetUserDetails()
        {
            string strUserName, strPassword, strEmail;

            ConnectionManager oConnectionManager = new ConnectionManager();
            SqlConnection connActiveConnection = oConnectionManager.GetActiveConnection;

            RegistryKey rkeyAVT = Registry.CurrentUser.OpenSubKey("AVT");
            if (rkeyAVT != null)
            {

                strUserName = rkeyAVT.GetValue("username").ToString();


                strPassword = rkeyAVT.GetValue("password").ToString();
                strEmail = rkeyAVT.GetValue("email").ToString();

                rkeyAVT.Close();
                
                if (strUserName == txtUserName.Text.Trim() && txtPassword.Text == Encoding.ASCII.GetString(Convert.FromBase64String(strPassword)))
                {
                    if (strUserName == "admin")
                    {
                        this.Hide();

                        AdminForm oAdminForm = new AdminForm();
                        oAdminForm.ShowDialog();

                    }
                    else
                    {
                        this.Hide();

                        TimeTableForm oTimeTableForm = new TimeTableForm();
                        DataSet dsTimeTable = DBManager.GetInstance.GetTimeTable(strEmail, connActiveConnection);
                        oTimeTableForm.gvwTimeTable.DataSource = dsTimeTable.Tables[0];
                        oTimeTableForm.ShowDialog();

                    }
                } else
                {
                   
                    DataSet dsUserDetails = DBManager.GetInstance.GetUserDetails(txtUserName.Text, txtPassword.Text, connActiveConnection);
                    if (dsUserDetails.Tables[0].Rows != null && dsUserDetails.Tables[0].Rows.Count > 0)
                    {
                        bool bisAdmin;
                        strUserName = dsUserDetails.Tables[0].Rows[0]["username"].ToString();
                        strPassword = dsUserDetails.Tables[0].Rows[0]["password"].ToString();
                        strEmail = dsUserDetails.Tables[0].Rows[0]["email"].ToString();
                        bisAdmin = Convert.ToBoolean(dsUserDetails.Tables[0].Rows[0]["isadmin"]);

                        RegistryKey rkeyAVTUSer = Registry.CurrentUser.CreateSubKey("AVT");
                        rkeyAVTUSer.SetValue("username", strUserName);
                        rkeyAVTUSer.SetValue("password", Convert.ToBase64String(Encoding.ASCII.GetBytes(strPassword)));
                        rkeyAVTUSer.SetValue("email", strEmail);
                        rkeyAVTUSer.SetValue("LoggedInTime", DateTime.Now.ToString());
                        rkeyAVTUSer.Close();
                        if (bisAdmin)
                        {
                            this.Hide();
                            AdminForm oAdminForm = new AdminForm();
                            oAdminForm.ShowDialog();
                        } else
                        {
                            this.Hide();

                            TimeTableForm oTimeTableForm = new TimeTableForm();
                            DataSet dsTimeTable = DBManager.GetInstance.GetTimeTable(strEmail, connActiveConnection);
                            oTimeTableForm.gvwTimeTable.DataSource = dsTimeTable.Tables[0];
                            oTimeTableForm.ShowDialog();
                        }

                    }

                    else
                    {

                        MessageBox.Show("Invalid Attempt !");
                        txtUserName.Text = txtPassword.Text = "";

                    }
                    if (connActiveConnection.State != ConnectionState.Closed)
                    {
                        connActiveConnection.Close();
                    }
                }

            }
            else
            {
               
                DataSet dsUserDetails = DBManager.GetInstance.GetUserDetails(txtUserName.Text, txtPassword.Text, connActiveConnection);
                if (dsUserDetails.Tables[0].Rows != null && dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    bool bisAdmin;
                    strUserName = dsUserDetails.Tables[0].Rows[0]["username"].ToString();
                    strPassword = dsUserDetails.Tables[0].Rows[0]["password"].ToString();
                    strEmail = dsUserDetails.Tables[0].Rows[0]["email"].ToString();
                    bisAdmin = Convert.ToBoolean(dsUserDetails.Tables[0].Rows[0]["isadmin"]);

                    RegistryKey rkeyAVTUSer = Registry.CurrentUser.CreateSubKey("AVT");
                    rkeyAVTUSer.SetValue("username", strUserName);
                    rkeyAVTUSer.SetValue("password", Convert.ToBase64String(Encoding.ASCII.GetBytes(strPassword)));
                    rkeyAVTUSer.SetValue("email", strEmail);
                    rkeyAVTUSer.SetValue("LoggedInTime", DateTime.Now.ToString());
                    rkeyAVTUSer.Close();

                    if (bisAdmin)
                    {
                        this.Hide();
                        AdminForm oAdminForm = new AdminForm();
                        oAdminForm.ShowDialog();
                    } else
                    {
                        TimeTableForm oTimeTableForm = new TimeTableForm();
                        DataSet dsTimeTable = DBManager.GetInstance.GetTimeTable(strEmail, connActiveConnection);
                        oTimeTableForm.gvwTimeTable.DataSource = dsTimeTable.Tables[0];
                        oTimeTableForm.ShowDialog();
                    }

                }

                else
                {

                    MessageBox.Show("Invalid Attempt !");
                    txtUserName.Text = txtPassword.Text = "";

                }
                if (connActiveConnection.State != ConnectionState.Closed)
                {
                    connActiveConnection.Close();
                }

            }
        }

        private void TestCode()
        {
            //Encrypt
            HashAlgorithm hash = MD5.Create();

            //string strKeyVal = GetUniqueKeyVal(16);

            byte[] key = hash.ComputeHash(Encoding.Unicode.GetBytes(keyval));
            ciphertext = Encrypt(txtUserName.Text, key, IV);
            strciphertext = Convert.ToBase64String(ciphertext);
            txtUserName.Text = strciphertext;


            //byte[] IV = Encoding.Unicode.GetBytes("ab10efghijkl20op");

            HashAlgorithm hash2 = MD5.Create();

            byte[] key2 = hash.ComputeHash(Encoding.Unicode.GetBytes(keyval));
            //byte[] cipher = //Encoding.Unicode.GetBytes(txtUserName.Text);

            txtPassword.Text = Decrypt(Convert.FromBase64String("7/MUS2Mw85+VCYF/yI0uQg=="), key2);
        }
        private void lnklblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private string Decrypt(byte[] cipherText, byte[] Key)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;

                byte[] vectorb = Convert.FromBase64String(vector);
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

        public string GetUniqueKeyVal(int size)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        private static byte[] GenerateBitsOfRandomEntropy(int num)
        {
            var randomBytes = new byte[num]; // 32 Bytes will give us 256 bits.

            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }

            return randomBytes;
        }
        private byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                // aes.Mode = CipherMode.CBC;
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }

    }
}
