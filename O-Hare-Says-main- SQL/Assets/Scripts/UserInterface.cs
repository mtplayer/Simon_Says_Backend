using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UserInterface : MonoBehaviour
{
    /*declare two objects of the type Input Field with names PlayerName and Highscore.
     * declare two lists that will contain references to the PlayerName labels and Highscore labels of the GUI*/
    //public InputField PlayerName;
    public TMP_InputField PlayerName;
    //public InputField Highscore;
    public int Highscore;
    public List<Text> PlayerNames = new List<Text>();
    public List<Text> Highscores = new List<Text>();
    DBInterface DBInterface;
    [SerializeField] Score score;





    // Start is called before the first frame update. declare an DBInterface object, that will be set on application start.
    void Start()
    {
        DBInterface = FindObjectOfType<DBInterface>();
    }

    /*function checks first if the DBInterface object could be successfully retrieved, then it accesses the Input Fields objects 
     * it checks if the fields contain any information and if the high score can be converted from string to integer. Finally, 
     * it uses the DBInterface object to insert the high score into the database. */
    public void InsertHighscore()
    {
        //score.SetScore();
        Highscore = PlayerPrefs.GetInt("Highscore");

        if (DBInterface == null)
        {
            Debug.LogError("UserInterface: Could not insert a highscore. DBIitefrace is not present.");
            return;
        }
        if (PlayerName == null)
        {
            Debug.LogError("UserInterface: Could not insert a highscore. PlayerName or Highscore is not set.");
            return;
        }
        if (Highscore < 0)
        {
            Debug.LogError("UserInterface: Could not insert a highscore. PlayerName or Highscore is not set.");
            return;
        }
        if (string.IsNullOrEmpty(PlayerName.text) || string.IsNullOrWhiteSpace(PlayerName.text))
        {
            Debug.LogError("UserInterface: Could not insert a highscore. PlayerName is empty.");
            return;
        }
        int highscore = Highscore;
        /*if (!System.Int32.TryParse(Highscore.text, out highscore))
        {
            Debug.LogError("UserInterface: Could not insert a highscore. Highscore is not an integer.");
            return;
        }*/
        DBInterface.InsertHighscore(PlayerName.text, highscore);
        PlayerName.text = "";
        //Highscore = 0;
    }

    /*function checks if the DBInterface object is presented and if all references to the labels are properly set. 
     * Next, it asks DBInterface object to retrieve records from the database, clears the labels, and fills in the 
     * labels with new information. */
    public void RetrieveTopFiveHighscores()
    {
        if (DBInterface == null)
        {
            Debug.LogError("UserInterface: Could not retrieve the top five highscores. DBIitefrace is not present.");
            return;
        }
        if (PlayerNames.Count < 5 || Highscores.Count < 5)
        {
            Debug.LogError("UserInterface: Could not retrieve the top five highscores. Not all PlayerName labels or Highscore labels are present.");
            return;
        }
        clearScoreboard();
        List<System.Tuple<string, int>> highscores = DBInterface.RetrieveTopFiveHighscores();
        for (int i = 0; i < highscores.Count; i++)
        {
            PlayerNames[i].text = highscores[i].Item1;
            Highscores[i].text = highscores[i].Item2.ToString();
        }
    }

    // function that clears the content of labels
    private void clearScoreboard()
    {
        foreach (Text playername in PlayerNames)
        {
            playername.text = "";
        }
        foreach (Text highscore in Highscores)
        {
            highscore.text = "";
        }
    }
}