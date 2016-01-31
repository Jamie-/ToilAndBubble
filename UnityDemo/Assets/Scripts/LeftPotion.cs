using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class LeftPotion : MonoBehaviour {
    public Color color;
    public static double h, s = 0.6, v = 1d;

    // Use this for initialization
    void Start () {      
        h = (Cauldron.Hue + 90) % 360;
        h += (Cauldron.r.NextDouble() * 2 * Cauldron.spread)- Cauldron.spread;
        color = new HSVColor(h, s, v).RgbColor;
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = color;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
