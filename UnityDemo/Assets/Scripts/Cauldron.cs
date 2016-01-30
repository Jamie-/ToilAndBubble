using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cauldron : MonoBehaviour {
    public Color color;
    private int framesPassed;
    public Text frames;
    public Renderer rend;

    public static byte cauldRed;
    public static byte cauldGreen;
    public static byte cauldBlue;

    public Text countdown;
    private float timer;

    // Use this for initialization
    void Start()
    {
        // Set instance variables
        rend = GetComponent<Renderer>();
        timer = 60;

        // Create a random starting color
        cauldRed = (byte) Random.Range(0, 255);
        cauldGreen = (byte) Random.Range(0, 255);
        cauldBlue = (byte) Random.Range(0, 255);
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
        color = new Color32(cauldRed, cauldGreen, cauldBlue, 1);
        rend.material.color = color;
    }

    // Allow other methods to set cauldron's color value - needs changing to modify color instead of reseting it
    public static void setColorValues(byte red, byte green, byte blue) {
        cauldRed = red;
        cauldGreen = green;
        cauldBlue = blue;
    }
}
