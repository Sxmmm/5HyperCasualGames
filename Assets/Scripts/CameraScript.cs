using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    //Variable to hold the camera
    Camera cm;
    //Array to hold the two colours of the background
    Color[] colours = new Color[2];
    public Color newColour;
    [SerializeField]
    GameObject player;
    //In start we get the player, set the camera and assigning the colours
    void Start() {
        player = GameObject.FindWithTag("Player");
        cm = GetComponent<Camera>();
        colours[0] = Color.grey; 
        colours[1] = Color.white;
        newColour = colours[0];
    }
    //In update we set the camera to follow the player and change the colour of the background
    void Update() {
        gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        cm.backgroundColor = Color.Lerp(cm.backgroundColor, newColour, 0.1f);

        if (cm.backgroundColor == colours[1]) {
            newColour = colours[0];
        } else if (cm.backgroundColor == colours[0]) {
            newColour = colours[1];
        }
    }
}
