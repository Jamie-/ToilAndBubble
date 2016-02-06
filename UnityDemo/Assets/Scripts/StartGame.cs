using UnityEngine;
using System.Collections;
 using UnityEngine.EventSystems;  
 using UnityEngine.UI;
using System.Text.RegularExpressions;

public class StartGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text text; //Button text
    
    public Text inputFieldText1;
    public Text inputFieldText2;

    public static string playerOneName;
    public static string playerTwoName;

    private float time;

    public static StartGame instance = null;
    void Awake()
    {
        if (instance != null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if (this.name == "ContinueButton")
        {
            Debug.Log("setting play");
        }
    }

    public void startGame()
    {
        time = this.GetComponent<AudioSource>().time;
        Debug.Log(time);
        Application.LoadLevel("NamePlayers");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize = text.fontSize + 10;
       // GetComponent<AudioSource>().Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //GetComponent<AudioSource>().Stop();
        text.fontSize = text.fontSize - 10;
    }

    // For Assigning Player Names
    public void startMain()
    {
        playerOneName = inputFieldText1.text;
        playerTwoName = inputFieldText2.text;
        startQuickGame();
    }

    public void startQuickGame()
    {
        Application.LoadLevel("Main");
    }

}
