using UnityEngine;
using System.Collections;
 using UnityEngine.EventSystems;  
 using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text text;

    public void startGame()
    {
        Application.LoadLevel("Main");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    void OnMouseEnter()
    {
        Application.LoadLevel(1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize = text.fontSize + 10;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize = text.fontSize - 10;
    }
}
