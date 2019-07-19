using CasinoLibrary.DAL;
using CasinoLibrary.Entities;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;

// Last modified 2019-04-16
namespace CasinoWebApp
{
    public partial class Login : System.Web.UI.Page
    {
        PlayerDAO playerDAO;

        // Initializing configuration settings.
        string connString = WebConfigurationManager.ConnectionStrings["CasinoDBConnString"].ConnectionString;
        // string connString = @"Data Source=SHAREK\SQLEXPRESS;Initial Catalog=CasinoDB;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set input field types.
            txtPass.Attributes["type"] = "password";
        }

        /// <summary>
        /// On validation, new user is authenticated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Starting a connection with the CasinoDB for User and Record tables.
            playerDAO = new PlayerDAO(connString);
            // Check if user is even registered.
            if (playerDAO.UserNameExists(txtUsername.Text))
            {
                // Retrieve user through username from database.
                Player player = playerDAO.SelectByUsername(txtUsername.Text);
                // Retrieve B64Password and B64Salt from user.
                string salt = player.B64Salt;
                string secPwd = player.B64Password;
                // Convert values to bytes.
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(txtPass.Text);
                // Combine to single byte value.
                byte[] combined = new byte[saltBytes.Length + passwordBytes.Length];
                saltBytes.CopyTo(combined, 0);
                passwordBytes.CopyTo(combined, saltBytes.Length);
                // Compute hash for combined byte value.
                SHA512 sha512 = SHA512.Create();
                byte[] finalHashedBytes = sha512.ComputeHash(combined);
                // Convert hashed byte to string for comparison.
                string base64Password = Convert.ToBase64String(finalHashedBytes);
                // Check if forms hashed password matches database hashed password.
                if (base64Password.Equals(secPwd))
                {
                    Session["Player"] = player;
                    FormsAuthentication.RedirectFromLoginPage($"{player.LastName}, {player.FirstName}", false);
                }
                else
                {
                    txtUsername.BorderColor = Color.Red;
                    txtUsername.Text = "Username";
                    txtPass.BorderColor = Color.Red;
                    txtPass.Text = "Password";
                }
            }
            else
            {
                txtUsername.BorderColor = Color.Orange;
                txtUsername.Text = "Username";
                txtPass.BorderColor = Color.FromArgb(236, 240, 241);
                txtPass.Text = "Password";
            }
        }

        /// <summary>
        /// Sets all input fields to default empty values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "Username";
            txtPass.Text = "Password";
        }
    }
}