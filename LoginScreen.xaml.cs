using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Windows.Controls;
using MessageBox = System.Windows.Forms.MessageBox;

namespace test1
{
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        // Verify hashed password
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {

            byte[] buffer4;
            
            if (hashedPassword == null)
            {
                return false;
            }
            
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            
            byte[] src = Convert.FromBase64String(hashedPassword);
            
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static bool ByteArraysEqual(byte[] buffer3, byte[] buffer4)
        {
            bool areEqual = buffer3.SequenceEqual(buffer4);

            return areEqual;
        }

        private void Button_Click_1(object sender, EventArgs e)
        {

            MySqlConnection conn = new MySqlConnection
            {
                ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
            };
          
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT brukernavn, passord FROM admin_user WHERE brukernavn = '" + txtUsername.Text + "' AND passord ='" + txtPassword.Password + "'";
            conn.Open();
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();
            String dbUsername = string.Empty;
            String dbPassword = string.Empty;
          
            while (reader.Read())
            {
                dbUsername += reader.GetString("brukernavn");
                dbPassword += reader.GetString("passord");
               
            }

            if (dbUsername == "" || dbPassword == "") 
            {
                Debug.WriteLine("Wrong username and Password");
                string message = "Wrong username or password";
                string title = "Error";
                MessageBox.Show(message, title);
                this.Show(); 
                LoginScreen mm = new LoginScreen();
                mm.Show();
                this.Close();
            }
            
            else 
            {
                Debug.WriteLine("Success");
                this.Show();
                MainWindow mm = new MainWindow();
                mm.Show();
                this.Close();
            }
        }
    }
}