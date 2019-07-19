using CasinoLibrary.GameFiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

// Last modified 2019-04-16
namespace CasinoLibrary.DAL
{
    /// <summary>
    /// This class will handle all interactions with the Records table in the database.
    /// </summary>
    public class RecordDAO
    {
        // String value for connecting to the database containing record information.
        string connString;

        /// <summary>
        /// Constructor for the class, sets the connString field to parameter value.
        /// </summary>
        /// <param name="connString"></param>
        public RecordDAO(string connString)
        {
            this.connString = connString;
        }

        /// <summary>
        /// This function retrieves a query from the Record table by the username.
        /// </summary>
        /// <returns></returns>
        public Record ReadRecordByUserName(string userName)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            Record record = null;

            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();
                // Start a connection.
                cmd = new SqlCommand("SELECT * FROM Records WHERE user_name = @UName");
                cmd.Parameters.AddWithValue("@UName", userName);
                cmd.Connection = conn;
                // Execute a command.
                reader = cmd.ExecuteReader();
                // Create a new instance of Record.
                record = null;
                // Iterate through queries.
                while (reader.Read())
                {
                    string username = Convert.ToString(reader["user_name"]);
                    int gamesPlayed = Convert.ToInt32(reader["games_played"]);
                    int wins = Convert.ToInt32(reader["wins"]);
                    int losses = Convert.ToInt32(reader["losses"]);
                    int draws = Convert.ToInt32(reader["draws"]);
                    float winLossRatio = Convert.ToSingle(reader["winLossRatio"]);
                    record = new Record(username, gamesPlayed, wins, losses, draws);
                }

                // Return the results.
                return record;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return record;
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
        /// This function retrieves all queries from the Records table.
        /// </summary>
        /// <returns></returns>
        public List<Record> ReadAllRecords()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<Record> records = null;

            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();
                // Start a connection.
                cmd = new SqlCommand("SELECT * FROM Records");
                cmd.Connection = conn;
                // Execute a command.
                reader = cmd.ExecuteReader();
                // Create a list of Records.
                records = new List<Record>();
                // Iterate through queries.
                while (reader.Read())
                {
                    string userName = Convert.ToString(reader["user_name"]);
                    int gamesPlayed = Convert.ToInt32(reader["games_played"]);
                    int wins = Convert.ToInt32(reader["wins"]);
                    int losses = Convert.ToInt32(reader["losses"]);
                    int draws = Convert.ToInt32(reader["draws"]);
                    float winLossRatio = Convert.ToSingle(reader["winLossRatio"]);
                    records.Add(new Record(userName, gamesPlayed, wins, losses, draws));
                }

                // Return the results.
                return records;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return records;
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
        /// This function retrieves all queries from the Records table and sorts them
        /// by the winLossRatio in descending order.
        /// </summary>
        /// <returns></returns>
        public List<Record> ReadAllRecordsSortedByRatio()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<Record> records = null;

            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();
                // Start a connection.
                cmd = new SqlCommand("SELECT * FROM Records ORDER BY winLossRatio DESC");
                cmd.Connection = conn;
                // Execute a command.
                reader = cmd.ExecuteReader();
                // Create a list of Records.
                records = new List<Record>();
                // Iterate through queries.
                while (reader.Read())
                {
                    string userName = Convert.ToString(reader["user_name"]);
                    int gamesPlayed = Convert.ToInt32(reader["games_played"]);
                    int wins = Convert.ToInt32(reader["wins"]);
                    int losses = Convert.ToInt32(reader["losses"]);
                    int draws = Convert.ToInt32(reader["draws"]);
                    float winLossRatio = Convert.ToSingle(reader["winLossRatio"]);
                    records.Add(new Record(userName, gamesPlayed, wins, losses, draws));
                }

                // Return the results.
                return records;
            }
            catch (SqlException sqle)
            {
                Console.Write(sqle.StackTrace);
                return records;
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
        /// This function creates a new entry in the Records table.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public int AddNewRecord(Record record)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            int count = 0;

            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();
                // Start a connection.
                cmd = new SqlCommand("INSERT INTO Records(user_name, games_played, wins, losses, draws, winLossRatio) " +
                    "VALUES(@UName, @GPlayed, @Wins, @Losses, @Draws, @WLRatio)");
                cmd.Parameters.AddWithValue("@UName", record.UserName);
                cmd.Parameters.AddWithValue("@GPlayed", record.GamesPlayed);
                cmd.Parameters.AddWithValue("@Wins", record.Wins);
                cmd.Parameters.AddWithValue("@Losses", record.Losses);
                cmd.Parameters.AddWithValue("@Draws", record.Draws);
                cmd.Parameters.AddWithValue("@WLRatio", record.WinLossRatio);
                cmd.Connection = conn;
                // Execute a command.
                count = cmd.ExecuteNonQuery();

                // Return the state of new entry.
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
        /// This function updates an existing entry in th Records table.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public int UpdateRecord(Record record)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            int count = 0;

            try
            {
                // Open a connection.
                conn = new SqlConnection(connString);
                conn.Open();
                // Start a connection.
                cmd = new SqlCommand("UPDATE Records SET games_played = @GPlayed, wins = @Wins, losses = @Losses, draws = @Draws, winLossRatio = @WLRatio WHERE user_name = @UName");
                cmd.Parameters.AddWithValue("@GPlayed", record.GamesPlayed);
                cmd.Parameters.AddWithValue("@Wins", record.Wins);
                cmd.Parameters.AddWithValue("@Losses", record.Losses);
                cmd.Parameters.AddWithValue("@Draws", record.Draws);
                cmd.Parameters.AddWithValue("@WLRatio", record.WinLossRatio);
                cmd.Parameters.AddWithValue("@UName", record.UserName);
                cmd.Connection = conn;
                // Execute a command.
                count = cmd.ExecuteNonQuery();
                
                // Return the state of new entry.
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
