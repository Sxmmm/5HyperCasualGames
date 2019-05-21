using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G4GameManagerScript : MonoBehaviour {
    //Event create to hold a delegate for later use
    public static event Action OnCubeSpawned = delegate { };
    //Variables for spawning cubes in alternative axis
    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;
    //Variables for the audio
    public AudioSource audioPlayer;
    public AudioClip tap, gameover;
    private float pitch;
    //Variable for UI
    [SerializeField]
    private GameObject startUI;

    private GameObject cam;
    //On awake assign variables and find spawners
    private void Awake() {
        audioPlayer.PlayOneShot(gameover);
        startUI.SetActive(true);
        spawners = FindObjectsOfType<CubeSpawner>();
        cam = GameObject.Find("Main Camera");
        audioPlayer.clip = tap;
        pitch = 0.7f;
    }
    //Update to detect for touch. This is used to stop the cube and make them fall.
	void Update() {
        if(Input.GetButtonDown("Fire1")) {
            startUI.SetActive(false);
            if (MovingCubeScript.CurrCube != null) {
                MovingCubeScript.CurrCube.Stop();
            }
            audioPlayer.pitch = pitch;
            audioPlayer.Play();
            pitch += 0.02f;
            if(pitch > 1.2f) {
                pitch = 0.7f;
            }
            //Switch between spawners
            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];

            currentSpawner.SpawnCube();
            OnCubeSpawned();
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 0.055f, cam.transform.position.z);
        }
    }
}
