using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        cauldRed = (byte) Random.Range(0, 255);
        cauldGreen = (byte) Random.Range(0, 255);
        cauldBlue = (byte) Random.Range(0, 255);
        color = new Color32(cauldRed, cauldGreen, cauldBlue, 1);
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = color;

        timer = 60;
    }

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
