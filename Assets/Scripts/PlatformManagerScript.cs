using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManagerScript : MonoBehaviour {
    //float position used for instantiating the platforms across the x axis
    private float xPos;
    //Gameobject used to spawn platforms and coins
    public GameObject platform;
    public GameObject coin;
    //Instart we set the initial position for the x and spawn a set start of platforms. There is also a chance to spawn coins.
	void Start () {
        xPos = 1.25f;
        for (int i = 0; i < 16; i++) {
            Instantiate(platform, new Vector3(xPos, 0, 0), Quaternion.identity);
            if(Random.Range(1, 10) == 5) {
                Instantiate(coin, new Vector3(xPos, 2.5f, 0), Quaternion.identity);
            }
            xPos += 1.25f;
        }
    }
    //Everytime this is called it will spawn one more platform and increase the x position incase it is called again
    //  It will also have the chance to a coin on the new platform
    public void SpawnPlatform() {
        Instantiate(platform, new Vector3(xPos, 0, 0), Quaternion.identity);
        if (Random.Range(1, 10) == 5) {
            Instantiate(coin, new Vector3(xPos, 2.5f, 0), Quaternion.identity);
        }
        xPos += 1.25f;
    }
}
