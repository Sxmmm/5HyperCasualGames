using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RabbitTapScript : MonoBehaviour {

    int counter = 0;
    public Text taps;

	void Update () {
        foreach(Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                counter++;
            }
        }
        taps.text = counter.ToString();
    }
}
