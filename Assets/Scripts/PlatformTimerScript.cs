using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTimerScript : MonoBehaviour {

    //Timer to display the amount of time left before the platform disappears
    private float timer;

    //Setting the timer and tellign the gameobject to destroy after 5 seconds
	void Start () {
        timer = 5;
        Destroy(gameObject, 5);
	}
	
    //Taking time away from timer and setting the on screen UI
	void Update () {
        timer -= Time.deltaTime;
        gameObject.GetComponent<TextMesh>().text = timer.ToString("F0");
	}
}
