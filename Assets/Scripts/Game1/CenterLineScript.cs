using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterLineScript : MonoBehaviour {

    //Simple script used to make object move down the screen to give the effect of the car driving down the road

    [SerializeField]
    private int speed = 10;

	void Start () {
        Destroy(gameObject, 3f);
	}
	
	void Update () {
        transform.position += Vector3.down * Time.deltaTime * speed;
	}
}
