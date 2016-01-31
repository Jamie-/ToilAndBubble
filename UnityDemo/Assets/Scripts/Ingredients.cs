using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts;
using System;
using System.Collections.Generic;

public class Ingredients : MonoBehaviour {
    private const float cooldownVal = 5; // Cool down time constant (seconds)

    public Material normalMaterial;
    public Material inActive;
    public Text leftBinText;
    public Text rightBinText;
    private bool leftShiftState;
    private bool rightShiftState;
    public static bool gameStarted;

    public Text StartCountdown;
    private GameObject[] hide;
    private float startCountdownTime;

    private HashSet<int> availableHuesLeft;
    private HashSet<int> availableHuesRight;
    private GameObject[] ingredients;
    private GameObject[] coolTimers;
    private bool[] coolActive;
    private bool[] newColor;
    private float[] coolTime;

    private double[] ingredHues;
    private double saturation = 0.8;
    private double value = 1d;

    private System.Random r;
    private bool initialPass = true;

    // Use this for initialization
    void Start () {
        availableHuesLeft = new HashSet<int>();
        availableHuesRight = new HashSet<int>();
        for (int j = 0; j < 360; j++)
        {
            availableHuesLeft.Add(j);
            availableHuesRight.Add(j);
        }
        r = new System.Random();
        // Set up all arrays
        ingredients = new GameObject[10];
        coolTimers = new GameObject[10];
        coolTime = new float[10];
        coolActive = new bool[10];
        newColor = new bool[10];
        ingredHues = new double[10];

        // Hide binning text
        leftBinText.enabled = false;
        rightBinText.enabled = false;
        gameStarted = false;
        startCountdownTime = 3.4f;

        int i = 0; // Main counter for setup

        // Build array of ingredients for fast access
        int objCount = 0;
        for (i = 1; i <= 5; i++)
        {
            ingredients[objCount] = GameObject.Find("L" + i);
            objCount++;
        }
        for (i = 1; i <= 5; i++)
        {
            ingredients[objCount] = GameObject.Find("R" + i);
            objCount++;
        }

        // Main init loop
        for (i = 0; i < 10; i++)
        {
            coolActive[i] = false; // Set all coolActive values to false
            newColor[i] = true; // Make all colors require updating
            coolTime[i] = 0; // Set all coolTime values to 0
            coolTimers[i] = GameObject.Find("C" + (i+1)); // Build arrray of coolTimers for fast access
            generateNewColor(i); // Generate random ingredient starting colors
        }

        initialPass = false;

        // Hide pre game components
        hide = GameObject.FindGameObjectsWithTag("HideBeforeGame");
        foreach (GameObject obj in hide)
        {
            obj.GetComponent<Renderer>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (gameStarted)
        {
            // Check ingredient keys
            if (Input.GetKeyDown(KeyCode.Alpha1) && !coolActive[0])
            {
                ingredientUsed(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !coolActive[1])
            {
                ingredientUsed(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && !coolActive[2])
            {
                ingredientUsed(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && !coolActive[3])
            {
                ingredientUsed(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && !coolActive[4])
            {
                ingredientUsed(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && !coolActive[5])
            {
                ingredientUsed(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) && !coolActive[6])
            {
                ingredientUsed(7);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8) && !coolActive[7])
            {
                ingredientUsed(8);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9) && !coolActive[8])
            {
                ingredientUsed(9);
            }
            if (Input.GetKeyDown(KeyCode.Alpha0) && !coolActive[9])
            {
                ingredientUsed(10);
            }

            // Check bin keys
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                leftShiftState = true;
                leftBinText.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.RightAlt))
            {
                rightShiftState = true;
                rightBinText.enabled = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                leftShiftState = false;
                leftBinText.enabled = false;
            }
            if (Input.GetKeyUp(KeyCode.RightAlt))
            {
                rightShiftState = false;
                rightBinText.enabled = false;
            }

            updateIngredientTimers(); // update all timers once per frame

            // Set any required new colors
            for (int i = 0; i < 10; i++)
            {
                if (newColor[i])
                {
                    generateNewColor(i); // generate and set new color
                    newColor[i] = false; // clear new color flag
                }
            }
        } else
        {
            // Run inital countdown timer
            startCountdownTime -= Time.deltaTime;
            if (startCountdownTime.ToString("0") == "0") StartCountdown.text = "Go!";
            else StartCountdown.text = startCountdownTime.ToString("0");

            if (startCountdownTime < 0)
            {
                foreach (GameObject obj in hide)
                {
                    obj.GetComponent<Renderer>().enabled = true;
                }
                StartCountdown.enabled = false;
                gameStarted = true;
            }
        }
        
        
    }

    // Generate random colour values into colour arrays and set color
    void generateNewColor(int index)
    {
        double range = 360d / ingredients.Length;
        double myHue = 0d;
        if (!initialPass)
        {
            myHue = ingredHues[index];
            updateAvailableHues(index, (int)range, (int)myHue, true);
        }
        int hue;
        if(index < 5)
        {
            int[] a = new int[availableHuesLeft.Count];
            availableHuesLeft.CopyTo(a);
            hue = a[r.Next(a.Length)];           
        }
        else
        {
            int[] a = new int[availableHuesRight.Count];
            availableHuesRight.CopyTo(a);
            hue = a[r.Next(a.Length)];
        }
        updateAvailableHues(index, (int)range, (int)hue, false);

        myHue = (double)hue + r.NextDouble() * Math.Pow(-1d, r.Next(2));

        ingredHues[index] = myHue;
        Color color = new HSVColor(ingredHues[index], saturation, value).RgbColor;
        Renderer rend = ingredients[index].GetComponent<Renderer>();
        rend.material.color = color;
    }

    void updateAvailableHues(int index, int range, int hue, bool add)
    {
        bool splitRange = false;

        int lowerBound1 = hue - range;
        int lowerBound2 = 0;
        int upperBound1 = hue + range;
        int upperBound2 = 360;
        if (lowerBound1 < 0)
        {
            splitRange = true;
            lowerBound2 = 360 + lowerBound1;
            lowerBound1 = 0;
        }
        if (upperBound1 > 360)
        {
            splitRange = true;
            upperBound2 = upperBound1 % 360;
            upperBound1 = 360;
        }

        for (int j = lowerBound1; j < upperBound1; j++)
        {
            if (index < 5)
            {
                if (add) availableHuesLeft.Add(j); else availableHuesLeft.Remove(j);
            }
            else
            {
                if (add) availableHuesRight.Add(j); else availableHuesRight.Remove(j);
            }
        }
        if (splitRange)
        {
            for (int j = lowerBound2; j < upperBound2; j++)
            {
                if (index < 5)
                {
                    if (add) availableHuesLeft.Add(j); else availableHuesLeft.Remove(j);
                }
                else
                {
                    if (add) availableHuesRight.Add(j); else availableHuesRight.Remove(j);
                }
            }
        }
    }

    // Called on use ingredient key press
    void ingredientUsed(int ingNo)
    {
        if (ingNo <= 5)
        {
            // Left Player
            if (leftShiftState)
            {
                // Bin Ingredient
                setIngredientTimer(ingNo - 1); // set timer for that ingredient
            } else
            {
                // Use Ingredient
                Cauldron.BlendColors(ingredients[ingNo - 1].GetComponent<Renderer>().material.color);
                setIngredientTimer(ingNo - 1); // set timer for that ingredient
            }
        } else
        {
            // Right Player
            if (rightShiftState)
            {
                // Bin Ingredient
                setIngredientTimer(ingNo - 1); // set timer for that ingredient
            }
            else
            {
                // Use Ingredient
                Cauldron.BlendColors(ingredients[ingNo - 1].GetComponent<Renderer>().material.color);
                setIngredientTimer(ingNo - 1); // set timer for that ingredient
            }
        }
    }

    // Starts timer for ingredient
    private void setIngredientTimer(int index)
    {
        coolTime[index] = cooldownVal;
        coolActive[index] = true; // set timer active flag
        ingredients[index].GetComponent<Renderer>().sharedMaterial = inActive; // set waiting image
    }

    // Checks all timers and updates
    private void updateIngredientTimers()
    {
        for (int index = 0; index < 10; index++)
        {
            if (coolActive[index])
            {
                coolTime[index] -= Time.deltaTime;
                if (coolTime[index] <= 0)
                {
                    coolTime[index] = 0;
                    coolActive[index] = false; // clear flag for timer being active
                    ingredients[index].GetComponent<Renderer>().sharedMaterial = normalMaterial; // reset waiting image
                    newColor[index] = true; // set flag for recolor
                }
                coolTimers[index].GetComponent<Text>().text = coolTime[index].ToString("0.0") + "s"; // update cooldown text
            }
        }
        
    }

    
}
