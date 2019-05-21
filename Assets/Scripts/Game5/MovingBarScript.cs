using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarScript : MonoBehaviour {
    //Variable to control the speed
    public float speed = 2;
    //Array for the colours of the bars
    private Color[] cols = new Color[4];
    //Assingning the colours
	void Start() {
        cols[0] = new Color(1f, 0f, 0.1647f, 1f);   //Red
        cols[1] = new Color(0f, 0.8491f, 1f, 1f);   //Blue
        cols[2] = new Color(1f, 0.9411f, 0f, 1f);   //Yellow
        cols[3] = new Color(0.8f, 0f, 1f, 1f);      //Purple
        //Picking a colour for the bar
        int i = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().color = cols[i];
        //Setting the tag for detection
        gameObject.tag = i.ToString();
    }
    //Update to move the bar down
	void Update () {
        transform.position += Vector3.down * Time.deltaTime * speed;
	}
}
