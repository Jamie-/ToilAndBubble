using UnityEngine;
using System.Collections;

public class RightPotion : MonoBehaviour {
    public Color color;
    public byte rightRed;
    public byte rightGreen;
    public byte rightBlue;

    // Use this for initialization
    void Start () {
        rightRed = (byte)Random.Range(0, 255);
        rightGreen = (byte)Random.Range(0, 255);
        rightBlue = (byte)Random.Range(0, 255);
        color = new Color32(rightRed, rightGreen, rightBlue, 1);
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = color;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
