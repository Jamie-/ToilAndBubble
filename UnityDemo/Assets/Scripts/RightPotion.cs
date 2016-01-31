using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class RightPotion : MonoBehaviour {
    public Color32 color;
    public static double h, s = 0.8, v = 1d;

    // Use this for initialization
    void Start () {  
        h = (Cauldron.Hue - 90);
        if (h < 0) h += 360;
        double d = Cauldron.r.NextDouble();
        h += (d * 2 * Cauldron.spread) - Cauldron.spread;
        color = new HSVColor(h, s, v).RgbColor;
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = color;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
