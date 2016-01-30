using UnityEngine;
using System.Collections;

public class TitleAnimation : MonoBehaviour {
    
    private bool direction;
	
	// Update is called once per frame
	void Update () {
        if (direction) transform.Translate(new Vector2(0, 10) * Time.deltaTime);
        else transform.Translate(new Vector2(0, 10) * Time.deltaTime * -1);

        if (transform.position.y > 420) direction = false;
        if (transform.position.y < 400) direction = true;

    }
}
