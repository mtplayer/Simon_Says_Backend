using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;


public class DBInterface : MonoBehaviour
{
    /*declare the SqlConnectionStringBuilder object that we will use to create 
     * a proper connection string for communication with the database.*/
   
    private SqlConnectionStringBuilder stringBuilder;

    /*declare public fields to be called in the Unity editor to be passed to the 
     * SqlConnectionStringBuilder object at the application start.*/

    public string ConnectionString;
    
    
    // Start is called before the first frame update
    void Start()
    {
        stringBuilder = new SqlConnectionStringBuilder();
        stringBuilder.ConnectionString = ConnectionString;

        
    }

    /*method to collect player's name and score, opens a new connection to the database, 
     * creates and executes a command that inserts a new record into the database, and finally closes the connection.*/
    public void InsertHighscore(string playerName, int highscore)
    {

        using (SqlConnection con = new SqlConnection(stringBuilder.ConnectionString))

        {
            try
            {
                con.Open();
                SqlCommand command = con.CreateCommand();
                command.CommandText = "INSERT INTO scores (playername, highscore) VALUES (@playerName, @highscore)";
                command.Parameters.AddWithValue("@playerName", playerName);
                command.Parameters.AddWithValue("@highscore", highscore);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                Debug.LogError("DBInterface: Could not insert the highscore! " + System.Environment.NewLine + ex.Message);
            }
        }

    }

    /*method to open a new connection to the database, creates and executes a command that selects all records in the database, 
     *sorts them in descending order, and then returns top five elements.Each returned record is then processed and put in the list. 
     *Finally, the connection will be closed and the list returned to the caller.*/
    public List<System.Tuple<string, int>> RetrieveTopFiveHighscores()
    {
        List<System.Tuple<string, int>> topFive = new List<System.Tuple<string, int>>();
      
        using (SqlConnection con = new SqlConnection(stringBuilder.ConnectionString))

        {
            try
            {
                con.Open();
                SqlCommand command = con.CreateCommand();
                command.CommandText = "SELECT TOP 5 * FROM scores ORDER BY highscore DESC";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var ordinal = reader.GetOrdinal("playername");
                    string playername = reader.GetString(ordinal);
                    ordinal = reader.GetOrdinal("highscore");
                    int highscore = reader.GetInt32(ordinal);
                    System.Tuple<string, int> entry = new System.Tuple<string, int>(playername, highscore);
                    topFive.Add(entry);
                }
                con.Close();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("DBInterface: Could not retrieve the top five highscores! " + System.Environment.NewLine + ex.Message);
            }
        }

        return topFive;
    }
}
