using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLeaderBoard : MonoBehaviour
{
    public GameObject LeaderBoard;
    public GameObject PlayerNameInput;
    public GameObject EnterButton;

    public void HideleaderBoard()
    {
        LeaderBoard.gameObject.SetActive(false);
    }

    public void PlayernameInput()
    {
        PlayerNameInput.gameObject.SetActive(false);
    }

    public void Enterbutton()
    {
        EnterButton.gameObject.SetActive(false);
    }
}
