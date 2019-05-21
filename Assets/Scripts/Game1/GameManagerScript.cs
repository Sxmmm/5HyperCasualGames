using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    //Variables for gravity and rigidbody
    public bool gravity;
    public Rigidbody rb;
    //Setting variables for the lighting of the game
    public GameObject DLight;
    private Light lt;
    //Variables to use to spawn the center line of the road
    public GameObject centerLine;
    private float timer;
    //Variables used to spawn police cars
    public GameObject policeCar;
    private float policeTimer;

	void Start () {
        //In start we set up our timers and assign our light variables
        timer = 0.5f;
        policeTimer = Random.Range(1.5f, 3);
        DLight = GameObject.FindWithTag("DLight");
        lt = DLight.GetComponent<Light>();
	}
	
	void Update () {
        //Counting down our timers
        timer -= Time.deltaTime;
        policeTimer -= Time.deltaTime;
        //Detecting for player tap
		if(Input.GetButtonDown("Fire1")) {
            //Alternating between the two gravity states
            gravity = !gravity;
        }
        //Spawning our center line to make it seem like the player is driving
        if(timer < 0) {
            Instantiate(centerLine, new Vector3(0f, 9.5f, 2.2f), Quaternion.identity);
            timer = 0.5f;
        }
        //Spawning police cars for the player to avoid
        if(policeTimer < 0) {
            int i = Random.Range(0, 2);
            if (i == 0) {
                Instantiate(policeCar, new Vector3(1.6f, 9f, 2.05f), transform.rotation * Quaternion.Euler(0f, 0f, 180f));
            } else {
                Instantiate(policeCar, new Vector3(-1.6f, 9f, 2.05f), transform.rotation * Quaternion.Euler(0f, 0f, 180f));
            }
            policeTimer = Random.Range(1.5f, 3);
        }
	}

    //In our fixed update I have to different gravity scales and light colours
    //  This is where the player car is controlled
    void FixedUpdate() {
        if(gravity) {
            //Gravity Right
            DLight.transform.rotation = Quaternion.Slerp(DLight.transform.rotation, Quaternion.Euler(15, 30, -12), Time.deltaTime * 2);
            lt.color = Color.Lerp(Color.red, Color.cyan, Time.deltaTime * 2);
            rb.AddForce((-Vector3.left * 9.8f)*2);
        } else {
            //Gravity Left
            DLight.transform.rotation = Quaternion.Slerp(DLight.transform.rotation, Quaternion.Euler(15, -30, -12), Time.deltaTime * 2);
            lt.color = Color.Lerp(Color.cyan, Color.red, Time.deltaTime * 2);
            rb.AddForce((Vector3.left * 9.8f)*2);
        }
    }
}
