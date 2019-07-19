using CasinoLibrary.DAL;
using CasinoLibrary.Entities;
using CasinoLibrary.GameFiles;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

// Last modified 2019-04-16
namespace CasinoWebApp
{
    public partial class Register : System.Web.UI.Page
    {
        PlayerDAO playerDAO;
        RecordDAO recordDAO;

        // Initializing configuration settings.
        string connString = WebConfigurationManager.ConnectionStrings["CasinoDBConnString"].ConnectionString;
        // string connString = @"Data Source=SHAREK\SQLEXPRESS;Initial Catalog=CasinoDB;Integrated Security=True";

        string base64Password;
        string base64Salt;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set input field types.
            txtPass.Attributes["type"] = "password";
        }

        /// <summary>
        /// On validation, new user is registered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Starting a connection with the CasinoDB for User and Record tables.
            playerDAO = new PlayerDAO(connString);
            recordDAO = new RecordDAO(connString);
            // Condition checks for duplicate account registrations.
            if (playerDAO.UserNameExists(txtUsername.Text))
            {
                txtUsername.Text = "Username already exists.";
                if (playerDAO.UserEmailExists(txtEmail.Text))
                {
                    txtEmail.Text = "Email already exists.";
                }
            }
            else
            {
                // Creates salted hash password for new registered player.
                GeneratePasswordSaltedHash(txtPass.Text);
                // Sets the new registered players starting balance.
                int balance = Player.STARTING_BALANCE;
                // Creates new instances of Player and Record for DAO classes.
                Player player = new Player(txtUsername.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, base64Password, base64Salt, balance);
                Record record = new Record(player.UserName, 0, 0, 0, 0);
                // Adds a new registered Player to User table and create their new record.
                playerDAO.AddPlayer(player);
                recordDAO.AddNewRecord(record);
                // Redirects to MainMenu page when registration is complete.
                Response.Redirect("/MainMenu.aspx");
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
            txtFirstName.Text = "First Name";
            txtLastName.Text = "Last Name";
            txtEmail.Text = "Email";
        }

        /// <summary>
        /// Computes a salted hash algorithm for the users password.
        /// </summary>
        /// <param name="newPassword"></param>
        private void GeneratePasswordSaltedHash(string newPassword)
        {
            RNGCryptoServiceProvider rNG = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[16];
            rNG.GetBytes(saltBytes);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(txtPass.Text);
            byte[] combined = new byte[saltBytes.Length + passwordBytes.Length];
            saltBytes.CopyTo(combined, 0);
            passwordBytes.CopyTo(combined, saltBytes.Length);

            SHA512 sha512 = SHA512.Create();
            byte[] finalHashedBytes = sha512.ComputeHash(combined);

            base64Password = Convert.ToBase64String(finalHashedBytes);
            base64Salt = Convert.ToBase64String(saltBytes);
        }
    }
}

