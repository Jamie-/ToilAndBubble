using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts;
using System;

public class Cauldron : MonoBehaviour {
    public static Color color;
    private int framesPassed;
    public Text frames;
    public Renderer rend;

    double h, s = 0.6, v = 1d;

    public Text countdown;
    private float timer;

    // Use this for initialization
    void Start()
    {
        // Set instance variables
        rend = GetComponent<Renderer>();
        timer = 60;

        // Create a random starting color
        
        System.Random r = new System.Random();
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
                countdown.text = "GO"; // Game over
            }

            // Update cauldron color
            updateColor();
        }
        
    }

    // Set color of cauldron to it's instance component color values
    void updateColor()
    {
        color = new HSVColor(h, s, v).RgbColor;
        rend.material.color = color;
    }

    // Allow other methods to set cauldron's color value - needs changing to modify color instead of reseting it
    public void setColorValues(double h)
    {
        setColorValues(h, this.s, this.v);
    }
    public void setColorValues(double h, double s, double v)
    {
        this.h = h;
        this.s = s;
        this.v = v;
    }

    private const double blendFactor = 0.5;

    public static void BlendColors(Color color2)
    {
        HSVColor c1 = new HSVColor(color);
        HSVColor c2 = new HSVColor(color2);

        double delta1;
        double delta2;

        if (c1.hue > c2.hue)
        {
            delta1 = (255 - c1.hue) + c2.hue;
        }
        else
        {
            delta1 = (255 - c2.hue) + c1.hue;
        }

        delta2 = c2.hue - c1.hue;

        if (Math.Abs(delta1) >= Math.Abs(delta2))
        {
            c1.hue += blendFactor * delta2;

        }
        else
        {
            if (c1.hue > c2.hue)
            {
                c1.hue += blendFactor * delta1;
                c1.hue %= 255;

            }
            else
            {
                c1.hue += blendFactor * -1 * delta1;
                if (c1.hue < 0)
                {
                    c1.hue = 255 + c1.hue;
                }
            }
        }

        color = c1.RgbColor;
    }
}
