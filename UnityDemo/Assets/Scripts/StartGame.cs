using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    public void startGame()
    {
        Application.LoadLevel(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    void OnMouseEnter()
    {
        Application.LoadLevel(1);
    }
	
}
