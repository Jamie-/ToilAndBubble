using UnityEngine;
using System.Collections;

public class TitleAnimation : MonoBehaviour {
    
    private bool direction;
    public int Min;
    public int Max;
	
	// Update is called once per frame
	void Update () {
        if (direction) transform.Translate(new Vector2(0, 10) * Time.deltaTime);
        else transform.Translate(new Vector2(0, 10) * Time.deltaTime * -1);

        if (transform.localPosition.y > Max) direction = false;
        if (transform.localPosition.y < Min) direction = true;

    }
}
