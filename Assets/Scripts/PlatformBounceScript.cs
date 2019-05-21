using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBounceScript : MonoBehaviour {
    //Vectors for the start position and up or down
    private Vector3 startPos;
    private Vector3 UpDown;
    //Bool used so we dont get extra score for landing on the same platform twice
    public bool scoreCollected;
    //Variables for speed of the up and down
    [SerializeField]
    float speed = 1.5f;
    public GameObject TimeText;
    
    //Assigning the variable and setting the cameras position
	void Start () {
        scoreCollected = true;
        startPos = transform.position;
        UpDown = Vector3.up;
        transform.position = new Vector3 (startPos.x, Random.Range(-5, -2), 0);
    }

    //Update to move the platforms and make the camera follow the player
	void FixedUpdate () {
        transform.position += UpDown * Time.deltaTime * speed;
        if(transform.position.y >= -2.0f) {
            UpDown = -Vector3.up;
        } else if(transform.position.y <= -5.0f) {
            UpDown = Vector3.up;
        }
    }

    //Function used to destroy the platform and spawn a timer to show the player how long they have
    public void TimeTillDeath() {
        Destroy(gameObject, 5.0f);
        Instantiate(TimeText, new Vector3 (gameObject.transform.position.x+0.4f, -2.9f, gameObject.transform.position.z), Quaternion.identity);
    }
}
