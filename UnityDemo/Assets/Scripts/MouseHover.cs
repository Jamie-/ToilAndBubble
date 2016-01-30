using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {
	    text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.S))
        {
            Application.LoadLevel(1);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnMouseEnter()
    {
        text.fontSize = text.fontSize + 10;
    }

    void OnMouseExit()
    {
        text.fontSize = text.fontSize - 10;
    }
}
