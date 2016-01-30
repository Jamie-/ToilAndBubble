using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class RightPotion : MonoBehaviour {
    public Color color;
    public byte rightRed;
    public byte rightGreen;
    public byte rightBlue;

    // Use this for initialization
    void Start () {
        //Set colour---------------
        double h, s = 0.6, v = 1d;
        System.Random r = new System.Random();
        h = r.NextDouble() * 360d;
        color = new HSVColor(h, s, v).RgbColor;
        //----------------------------------

        Renderer rend = GetComponent<Renderer>();
        rend.material.color = color;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
