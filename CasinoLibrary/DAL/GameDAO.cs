using CasinoLibrary.GameFiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

// Last modified 2019-04-16
namespace CasinoLibrary.DAL
{
    /// <summary>
    /// This class will handle all interactions with the Games table in the database.
    /// </summary>
    public class GameDAO
    {
        // String value for connecting to the database containing game information.
        string connString;

        /// <summary>
        /// Constructor for the class, sets the connString field to parameter value.        
        /// </summary>
        /// <param name="connString"></param>
        public GameDAO(string connString)
        {
            this.connString = connString;
        }

        /// <summary>
        /// Selects all games in the Game table within the database.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public List<Game> SelectAllGames()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<Game> games = null;

            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();

                // Starts a new connection to execute a command.
                cmd = new SqlCommand("SELECT * FROM Games");
                cmd.Connection = conn;

                // Execute command to read all queries.
                reader = cmd.ExecuteReader();

                // Creates a new List of Games;
                games = new List<Game>();

                // Iterate through queries.
                while (reader.Read())
                {
                    string userName = Convert.ToString(reader["user_name"]);
                    Result result = (Result)reader["result"];
                    int winnings = Convert.ToInt32(reader["winnings"]);

                    games.Add(new Game(userName, result, winnings));
                }

                // Return the results.
                return games;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return games;
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
        /// Selects a Game based on its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Game SelectByID(int id)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            Game game = null;

            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();

                // Starts a new connection to execute a command.
                cmd = new SqlCommand("SELECT * FROM Games WHERE game_id = @GameID");
                cmd.Parameters.AddWithValue("@GameID", id);
                cmd.Connection = conn;

                // Executes command to read query
                reader = cmd.ExecuteReader();
                // Creates a new Game
                game = null;

                //Iterates through found match and returns that as a Game object.
                while (reader.Read())
                {
                    string userName = (string)reader["user_name"];
                    Result result = (Result)reader["result"];
                    int winnings = Convert.ToInt32(reader["winnings"]);

                    game = new Game(userName, result, winnings);
                }

                // Return the results.
                return game;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return game;
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
        /// Adds a new entry to the Game table in the database.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public int AddNewGame(Game game)
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
                cmd = new SqlCommand("INSERT INTO Games(user_name, result, winnings) " +
                    "VALUES (@user_name, @result, @winnings)");
                cmd.Parameters.AddWithValue("@user_name", game.UserName);
                cmd.Parameters.AddWithValue("@result", game.Result.ToString());
                cmd.Parameters.AddWithValue("@winnings", game.Winnings);
                cmd.Connection = conn;

                // Executes a command to add a query.
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
    }
}
