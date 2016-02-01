using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class LeftPotion : MonoBehaviour {
    public Color32 color;
    public static double h, s = 0.8, v = 1d;

    // Use this for initialization
    void Start () {   
		UpdateColor();
    }

	private Color32 GenerateColor(){
		h = (Cauldron.Hue + 90) % 360;
		h += (Cauldron.r.NextDouble () * 2 * Cauldron.spread) - Cauldron.spread;
		return new HSVColor (h, s, v).RgbColor;
	}

	public void UpdateColor(){
		color = GenerateColor ();
		Renderer rend = GetComponent<Renderer>();
		rend.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		if (false) {
			if(Input.GetKeyDown(KeyCode.Q)){
				PrintHue();
			}
			if(Input.GetKeyDown(KeyCode.W)){
				UpdateColor();
			}
		}
	}

	public void PrintHue(){
		Debug.Log(h);
	}
}
