using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour {
    //Variable for the prefab of the asteroid
    public GameObject asteroid;
    //An array of vectors for our positions the asteroids can spawn from
    private Vector3[] SpawnPoint = new Vector3[12];
    private int i, j;
    //Timer for spawning the asteroids
    private float timer;
    //Bool to check if the game is playing
    private bool gameStarted;
    //Variables for the UI
    public GameObject startUI;
    public GameObject holdUI;
    //A timer for the pop up
    private float popupTimer = 2.0f;

    void Start () {
        //Setting up the UI
        holdUI.SetActive(true);
        startUI.SetActive(true);
        gameStarted = false;
        timer = 2;
        //Assigning all the positions we can spawn from
        SpawnPoint[0] = new Vector3(-10, 12, 0);
        SpawnPoint[1] = new Vector3(0, 12, 0);
        SpawnPoint[2] = new Vector3(10, 12, 0);
        SpawnPoint[3] = new Vector3(10, 2.75f, 0);
        SpawnPoint[4] = new Vector3(10, 0, 0);
        SpawnPoint[5] = new Vector3(10, -2.75f, 0);
        SpawnPoint[6] = new Vector3(10, -12, 0);
        SpawnPoint[7] = new Vector3(0, -12, 0);
        SpawnPoint[8] = new Vector3(-10, -12, 0);
        SpawnPoint[9] = new Vector3(-10, -2.75f, 0);
        SpawnPoint[10] = new Vector3(-10, 0, 0);
        SpawnPoint[11] = new Vector3(-10, 2.75f, 0);
    }
    //In update we check for the players touch to start the game. And if the game is started the game will start
    //  spawning asteroids
    void Update () {
        if(!gameStarted && Input.touchCount > 0) {
            gameStarted = true;
            GameObject.FindWithTag("Rocket").GetComponent<RocketScript>().multi = 1000;
            startUI.SetActive(false);
        }
        if (gameStarted) {
            HoldPopUp();
            timer -= Time.deltaTime;
            if (timer <= 0) {
                i = Random.Range(0, 12);
                do {
                    j = Random.Range(0, 12);
                } while (i == j);
                Instantiate(asteroid, SpawnPoint[i], Quaternion.identity);
                Instantiate(asteroid, SpawnPoint[j], Quaternion.identity);
                timer = Random.Range(0.5f, 5);
            }
        }
	}
    //This function is used to show the pop up for 2 seconds
    void HoldPopUp() {
        if (popupTimer > 0) {
            popupTimer -= Time.deltaTime;
            holdUI.SetActive(true);
        } else {
            holdUI.SetActive(false);
        }
    }
}
