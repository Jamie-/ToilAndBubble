using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts;
using System;

public class Cauldron : MonoBehaviour {
    public static Color32 color;
    private int framesPassed;
    public Text frames;
    public Renderer rend;

    static double h, s = 0.8, v = 1d;
    public static double spread = 30d;

    public Text countdown;
    private float timer;

    public static double Hue { get { return h; } }

    public static System.Random r = new System.Random();

    // Use this for initialization
    void Start()
    {
        // Set instance variables
        rend = GetComponent<Renderer>();
        timer = 60;

        // Create a random starting color
        h = r.NextDouble() * 360d;
        color = new HSVColor(h, s, v).RgbColor;
        updateColor(); // set it
        countdown.enabled = false; // Hide timer
    }
    
    // Update is called once per frame
    void Update() {
        // Frame counter
        framesPassed++;
        frames.text = "Frames: " + framesPassed.ToString();

        if (Ingredients.gameStarted)
        {
            if(!countdown.enabled)
            {
                countdown.enabled = true; // Enbable it when game is running
            }
            // Game timer
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                countdown.text = timer.ToString("0") + "s";
            }
            else
            {
                Application.LoadLevel("End");
            }

            // Update cauldron color
            updateColor();
        }
        
    }

    // Set color of cauldron to it's instance component color values
    void updateColor()
    {
        rend.material.color = color;
    }

    // Allow other methods to set cauldron's color value - needs changing to modify color instead of reseting it
    public static void setColorValues(double h)
    {
        setColorValues(h, s, v);
    }
    public static void setColorValues(double hue, double sat, double val)
    {
        h = hue;
        s = sat;
        v = val;
    }

    private const double blendFactor = 0.5;

    public static void BlendColors(Color color2)
    {
        HSVColor c1 = new HSVColor(color);
        HSVColor c2 = new HSVColor(color2);

        Debug.Log("4r=" + color.r + " g=" + color.g + " b=" + color.b);
        Debug.Log("h1=" + c1.hue + " h2=" + c2.hue);

        double delta1;
        double delta2;

        if (c1.hue > c2.hue)
        {
            delta1 = (360d - c1.hue) + c2.hue;
        }
        else
        {
            delta1 = (360d - c2.hue) + c1.hue;
        }

        delta2 = c2.hue - c1.hue;

        if (Math.Abs(delta1) >= Math.Abs(delta2))
        {
            c1.hue += blendFactor * delta2;
            Debug.Log("diff=" + delta2);
        }
        else
        {
            Debug.Log("diff=" + delta1);
            if (c1.hue > c2.hue)
            {
                c1.hue += blendFactor * delta1;
                c1.hue %= 360;
            }
            else
            {
                c1.hue -= blendFactor * delta1;
                if (c1.hue < 0)
                {
                    c1.hue = 360 + c1.hue;
                }
            }
        }

        Debug.Log("5new hue=" + c1.hue);
        h = c1.hue;
        color = c1.RgbColor;
        Debug.Log("r=" + color.r + " g=" + color.g + " b=" + color.b);
    }
}
