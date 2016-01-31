﻿using UnityEngine;
using System.Collections;
 using UnityEngine.EventSystems;  
 using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text text; //Button text
    
    public Text inputFieldText1;
    public Text inputFieldText2;

    public static string playerOneName;
    public static string playerTwoName;

    public void startGame()
    {
        Application.LoadLevel("NamePlayers");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize = text.fontSize + 10;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
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
