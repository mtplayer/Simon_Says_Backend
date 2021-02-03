using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLeaderBoard : MonoBehaviour
{
    public GameObject LeaderBoard;
    public GameObject PlayerNameInput;
    public GameObject EnterButton;

    public void ShowleaderBoard()
    {
        LeaderBoard.gameObject.SetActive(true);
    }

    public void PlayernameInput()
    {
        PlayerNameInput.gameObject.SetActive(true);
    }

    public void Enterbutton()
    {
        EnterButton.gameObject.SetActive(true);
    }
}
