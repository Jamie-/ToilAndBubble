using UnityEngine;
using System.Collections;

public class LeftPotion : MonoBehaviour {
    public Color color;
    public byte leftRed;
    public byte leftGreen;
    public byte leftBlue;

    // Use this for initialization
    void Start () {
        leftRed = (byte)Random.Range(0, 255);
        leftGreen = (byte)Random.Range(0, 255);
        leftBlue = (byte)Random.Range(0, 255);
        color = new Color32(leftRed, leftGreen, leftBlue, 1);
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = color;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
