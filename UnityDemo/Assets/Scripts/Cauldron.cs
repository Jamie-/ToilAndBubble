using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts;

public class Cauldron : MonoBehaviour {
    public Color color;
    private int framesPassed;
    public Text frames;
    public Renderer rend;

    public byte cauldRed;
    public byte cauldGreen;
    public byte cauldBlue;

    public Text countdown;
    private float timer;

    // Use this for initialization
    void Start()
    {
        //Set colour---------------
        double h, s = 0.6, v = 1d;
        System.Random r = new System.Random();
        h = r.NextDouble() * 360d;
        color = new HSVColor(h, s,v).RgbColor;
        //----------------------------------

        Renderer rend = GetComponent<Renderer>();
        rend.material.color = color;

        timer = 60;
    }
    //test
    // Update is called once per frame
    void Update() {
        framesPassed++;
        frames.text = "Frames: " + framesPassed.ToString();
        if (timer >= 0) { 
            timer -= Time.deltaTime;
            countdown.text = timer.ToString("0") + "s";
        } else
        {
            countdown.text = "GO";
        }
    }
    
}
