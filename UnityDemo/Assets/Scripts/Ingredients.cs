using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ingredients : MonoBehaviour {
    private const float cooldownVal = 5;

    private GameObject[] ingredients;
    private GameObject[] coolTimers;
    private bool[] coolActive;
    private float[] coolTime;
    private byte[] ingredReds;
    private byte[] ingredGreens;
    private byte[] ingredBlues;

    public GameObject cauld;
    private bool p1Mod;
    private bool p2Mod;

    // Use this for initialization
    void Start () {
        int i = 0;
        cauld = GameObject.Find("Cauldron");
        ingredients = new GameObject[10];
        coolTimers = new GameObject[10];
        coolTime = new float[10];
        coolActive = new bool[10];

        // Set all coolActive values to false
        for (i = 0; i < 10; i++)
        {
            coolActive[i] = false;
        }

        // Set all coolTime values to 0
        for (i = 0; i < 10; i++)
        {
            coolTime[i] = 0;
        }

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

        // Build arrray of coolTimers for fast access
        objCount = 0;
        for (i = 1; i <= 10; i++)
        {
            coolTimers[objCount] = GameObject.Find("C" + i);
            objCount++;
        }

        // Generate random starting colors
        ingredReds = new byte[10];
        ingredGreens = new byte[10];
        ingredBlues = new byte[10];
        for (i = 0; i < 10; i++)
        {
            ingredReds[i] = (byte) Random.Range(0, 255);
            ingredGreens[i] = (byte)Random.Range(0, 255);
            ingredBlues[i] = (byte) Random.Range(0, 255);
        }

        // Set starting colors
        i = 0;
        foreach (Transform child in transform)
        {
            Color color = new Color32(ingredReds[i], ingredGreens[i], ingredBlues[i], 1);
            Renderer rend = child.GetComponent<Renderer>();
            rend.material.color = color;
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !coolActive[0])
        {
            changeColor(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !coolActive[1])
        {
            changeColor(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !coolActive[2])
        {
            changeColor(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !coolActive[3])
        {
            changeColor(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && !coolActive[4])
        {
            changeColor(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && !coolActive[5])
        {
            changeColor(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && !coolActive[6])
        {
            changeColor(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && !coolActive[7])
        {
            changeColor(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && !coolActive[8])
        {
            changeColor(9);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && !coolActive[9])
        {
            changeColor(10);
        }

        for (int i = 0; i < 10; i++)
        {
            if (coolActive[i])
            {
                if (coolTime[i] <= 0)
                {
                    coolTime[i] = 0;
                    coolActive[i] = false;
                }
                coolTimers[i].GetComponent<Text>().text = coolTime[i].ToString("0.0") + "s";
                coolTime[i] -= Time.deltaTime;
            }
        }
    }

    void changeColor(int ingNo)
    {
        cauld.GetComponent<Renderer>().material.color = new Color32(ingredReds[ingNo - 1], ingredGreens[ingNo - 1], ingredBlues[ingNo - 1], 1);
        coolTime[ingNo - 1] = cooldownVal;
        coolActive[ingNo - 1] = true;
        ingredReds[ingNo - 1] = (byte)Random.Range(0, 255);
        ingredGreens[ingNo - 1] = (byte)Random.Range(0, 255);
        ingredBlues[ingNo - 1] = (byte)Random.Range(0, 255);
        Color color = new Color32(ingredReds[ingNo - 1], ingredGreens[ingNo - 1], ingredBlues[ingNo - 1], 1);
        Renderer rend = ingredients[ingNo - 1].GetComponent<Renderer>();
        rend.material.color = color;
    }
}
