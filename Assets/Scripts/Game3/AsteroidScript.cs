using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {
    //Variables used for the asteroids and to fire towards the player
    private Vector3 target;
    private Vector3 startPos;
    private int randAngle;
    private int randDirection;

	//In start we get the start position and the target we fire towards
	void Start () {
        startPos = transform.position;
        target = GameObject.FindWithTag("Rocket").GetComponent<Transform>().position;
        randAngle = Random.Range(50, 100);
        randDirection = Random.Range(1, 3);
        Debug.Log("Angle: " + randAngle + " | Direction: " + randDirection);
        Destroy(gameObject, 7.5f);
	}
	
	// In update we move the asteroid towards the players position upon the asteroid was spawned
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, target + -startPos, Time.deltaTime * 3);
        if (randDirection == 1) {
            transform.Rotate(0, 0, randAngle * Time.deltaTime);
        } else {
            transform.Rotate(0, 0, -randAngle * Time.deltaTime);
        }
    }

}
