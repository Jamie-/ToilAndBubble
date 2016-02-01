using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class RightPotion : MonoBehaviour {
    public Color32 color;
    public static double h, s = 0.8, v = 1d;

    // Use this for initialization
    void Start () {  
		UpdateColor();
    }

	private Color32 GenerateColor(){
		h = (Cauldron.Hue - 90);
		if (h < 0) h += 360;
		double d = Cauldron.r.NextDouble();
		h += (d * 2 * Cauldron.spread) - Cauldron.spread;
		return new HSVColor(h, s, v).RgbColor;
	}

	public void UpdateColor(){
		color = GenerateColor ();
		Renderer rend = GetComponent<Renderer>();
		rend.material.color = color;
	}
		
	// Update is called once per frame
	void Update () {
		if (false) {
			if(Input.GetKeyDown(KeyCode.P)){
				PrintHue();
			}
			if(Input.GetKeyDown(KeyCode.O)){
				UpdateColor();
			}
		}
	}

	public void PrintHue(){
		Debug.Log (h);
	}
}
