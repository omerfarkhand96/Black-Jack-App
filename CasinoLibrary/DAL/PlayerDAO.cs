using CasinoLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

// Last modified 2019-04-16
namespace CasinoLibrary.DAL
{
    /// <summary>
    /// This class will handle all interactions with the Users table in the database.
    /// </summary>
    public class PlayerDAO
    {
        // String value for connecting to the database containing player information.
        string connString;

        /// <summary>
        /// Constructor for the class, sets the connString field to parameter value.
        /// </summary>
        /// <param name="connString"></param>
        public PlayerDAO(string connString)
        {
            this.connString = connString;
        }

        /// <summary>
        /// Selects all players in the User table within the database.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public List<Player> SelectAllPlayers(Player player)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<Player> players = null;

            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();
                // Creates a new List of Players;
                players = new List<Player>();
                // Starts a new connection to execute a command.
                cmd = new SqlCommand("SELECT * FROM Users");
                cmd.Connection = conn;
                // Executes command to read queries.
                reader = cmd.ExecuteReader();
                // Iterates through all found matches and adds to the List of Players.
                while (reader.Read())
                {
                    string userName = Convert.ToString(reader["user_name"]);
                    string firstName = Convert.ToString(reader["first_name"]);
                    string lastName = Convert.ToString(reader["last_name"]);
                    string email = Convert.ToString(reader["email"]);
                    string b64Password = Convert.ToString(reader["b64Password"]);
                    string b64Salt = Convert.ToString(reader["b64Salt"]);
                    int balance = Convert.ToInt32(reader["Balance"]);

                    players.Add(new Player(userName, firstName, lastName, email, b64Password, b64Salt, balance));
                }

                // Return the results.
                return players;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return players;
            }
            finally
            {
                // Closes the open connection.
                if (reader.IsClosed == false)
                {
                    reader.Close();
                }
                cmd.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Selects a Player based on their UserName
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Player SelectByUsername(string username)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            Player player = null;

            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();

                // Starts a new connection to execute a command.
                cmd = new SqlCommand("SELECT * FROM Users WHERE user_name = @UserName");
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Connection = conn;

                // Creates a new Player.
                player = null;

                // Executes command to read query.
                reader = cmd.ExecuteReader();

                // Iterates through found match and returns that as a Player object.
                while (reader.Read())
                {
                    string userName = (string)reader["user_name"];
                    string firstName = (string)reader["first_name"];
                    string lastName = (string)reader["last_name"];
                    string email = Convert.ToString(reader["email"]);
                    string b64Password = Convert.ToString(reader["secPwd"]);
                    string b64Salt = Convert.ToString(reader["salt"]);
                    int balance = Convert.ToInt32(reader["balance"]);

                    player = new Player(userName, firstName, lastName, email, b64Password, b64Salt, balance);
                }

                // Return the results.
                return player;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return player;
            }
            finally
            {
                // Closes the open connection.
                if (reader.IsClosed == false)
                {
                    reader.Close();
                }
                cmd.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Deletes a Player based on their UserName.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int DeletePlayer(Player player)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            int count = 0;

            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();

                // Starts a new connection to execute a command.
                cmd = new SqlCommand("DELETE FROM Users WHERE user_name = @UserName");
                cmd.Parameters.AddWithValue("@UserName", player.UserName);
                cmd.Connection = conn;

                // Executes a command to remove query.
                count = cmd.ExecuteNonQuery();

                // Return the results.
                return count;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return count;
            }
            finally
            {
                // Closes the open connection.
                cmd.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Updates a Players information based on values provided.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int UpdatePlayer(Player player)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            int count = 0;

            try
            {
                // Opens a connection.
                conn = new SqlConnection(connString);
                conn.Open();

                // Starts a connection to execute a command.
                cmd = new SqlCommand("UPDATE Users " +
                    "SET first_name = @FirstName,  last_name = @LastName, email = @Email, secPwd = @Base64Password, salt = @Base64Saltbalance = @Balance " +
                    "WHERE user_name = @UserName");
                cmd.Parameters.AddWithValue("@FirstName", player.FirstName);
                cmd.Parameters.AddWithValue("@LastName", player.LastName);
                cmd.Parameters.AddWithValue("@Email", player.Email);
                cmd.Parameters.AddWithValue("@Base64Password", player.B64Password);
                cmd.Parameters.AddWithValue("@Base64Salt", player.B64Salt);
                cmd.Parameters.AddWithValue("@Balance", player.Balance);
                cmd.Parameters.AddWithValue("@UserName", player.UserName);
                cmd.Connection = conn;

                // Executes command to update query.
                count = cmd.ExecuteNonQuery();

                // Return the results.
                return count;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return count;
            }
            finally
            {
                // Closes the open connection.
                cmd.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Updates a Players Balance based on values provided.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int UpdateBalance(Player player)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            int count = 0;

            try
            {
                // Opens a connection.
                conn = new SqlConnection(connString);
                conn.Open();

                // Starts a connection to execute a command.
                cmd = new SqlCommand("UPDATE Users " +
                    "SET balance = @Balance " +
                    "WHERE user_name = @UserName");

                cmd.Parameters.AddWithValue("@Balance", player.Balance);
                cmd.Parameters.AddWithValue("@UserName", player.UserName);
                cmd.Connection = conn;

                // Executes command to update query.
                count = cmd.ExecuteNonQuery();

                // Return the results.
                return count;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return count;
            }
            finally
            {
                // Closes the open connection.
                cmd.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Adds a new entry to the User table in the database.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int AddPlayer(Player player)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            int count = 0;

            try
            {
                // Opens a connection
                conn = new SqlConnection(connString);
                conn.Open();

                // Starts a connection to execute a command.
                cmd = new SqlCommand("INSERT INTO Users VALUES(@UserName, @FirstName, @LastName, @Email, @SecPwd, @Salt, @Balance)");
                cmd.Parameters.AddWithValue("@FirstName", player.FirstName);
                cmd.Parameters.AddWithValue("@LastName", player.LastName);
                cmd.Parameters.AddWithValue("@Email", player.Email);
                cmd.Parameters.AddWithValue("@SecPwd", player.B64Password);
                cmd.Parameters.AddWithValue("@Salt", player.B64Salt);
                cmd.Parameters.AddWithValue("@Balance", player.Balance);
                cmd.Parameters.AddWithValue("@UserName", player.UserName);
                cmd.Connection = conn;

                // Executes command to update query.
                count = cmd.ExecuteNonQuery();

                // Return the results.
                return count;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return count;
            }
            finally
            {
                // Closes the open connection.
                cmd.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Function checks to avoid clone entries inputed by users.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Boolean UserNameExists(string username)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();
                // Start a connection.
                cmd = new SqlCommand("SELECT * FROM Users WHERE user_name = @UName");
                cmd.Parameters.AddWithValue("@UName", username);
                cmd.Connection = conn;
                // Execute a command.
                reader = cmd.ExecuteReader();
                // Iterate through queries.
                while (reader.Read())
                {
                    return true;
                }
                return false;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return false;
            }
            finally
            {
                // Close the open connection.
                if (reader.IsClosed == false)
                {
                    reader.Close();
                }
                cmd.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Function checks to avoid clone entries inputed by users.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Boolean UserEmailExists(string email)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();
                // Start a connection.
                cmd = new SqlCommand("SELECT * FROM Users WHERE email = @UEmail");
                cmd.Parameters.AddWithValue("@UEmail", email);
                cmd.Connection = conn;
                // Execute a command.
                reader = cmd.ExecuteReader();
                // Iterate through queries.
                while (reader.Read())
                {
                    return true;
                }
                return false;
            }
            catch(SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return false;
            }
            finally
            {
                // Close the open connection.
                if(reader.IsClosed == false)
                {
                    reader.Close();
                }
                cmd.Dispose();
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}