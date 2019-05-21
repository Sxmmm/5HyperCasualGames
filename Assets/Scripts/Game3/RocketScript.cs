using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketScript : MonoBehaviour {

    //Get the position of the players touch
    private Vector3 touch;
    public Camera cam;
    //Variables for the health
    private int health;
    public GameObject h1, h2, h3;
    //Variables for the score and the highscore
    public float score, multi;
    public Text scoreText;
    public Text scoreText2;
    //Variables for the UI
    public GameObject gameUI;
    public GameObject endUI;
    //Variables for the audio
    private AudioSource audioSrc;
    public AudioClip hit, death;

    //In start we set up our audio. Set the multiplier to 0 so we cant obtain score yet and set the health
    void Start() {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = hit;
        multi = 0;
        Time.timeScale = 1;
        endUI.SetActive(false);
        health = 3;
        h1.SetActive(true); h2.SetActive(true); h3.SetActive(true);
    }
    //In update we update the score and health. and get all the information about where the player is touching on screen
	void Update () {
        gameUI.SetActive(true);
        score += multi * Time.deltaTime;
        scoreText.text = score.ToString("n0");
        UpdateHealth();
        touch = Input.GetTouch(0).position;
        transform.position = Vector2.MoveTowards(transform.position, cam.ScreenToWorldPoint(touch), Time.deltaTime * 5.0f);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, cam.ScreenToWorldPoint(touch) - transform.position);
    }
    //Trigger to detect if we collide with the asteroids. Takes off our health and shakes the screen
    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Asteroid") {
            Destroy(col.gameObject);
            health--;
            cam.GetComponent<CameraManagerScript>().shakeDuration = 1;
            if (health == 0) {
                audioSrc.clip = death;
            }
            audioSrc.Play();
        }
    }
    //Function to update the health when called. If health is 0 it stops the game and displays the retry screen
    void UpdateHealth() {
        if (health == 2) {
            h3.SetActive(false);
        } else if (health == 1) {
            h2.SetActive(false);
        } else if (health == 0) {
            scoreText2.text = score.ToString("n0");
            endUI.SetActive(true);
            h1.SetActive(false);
            scoreText.text = "";
            cam.GetComponent<CameraManagerScript>().shakeDuration = 0;
            Time.timeScale = 0;
            UpdateHS();
        }
    }
    //Function to update the highscore if the score is greater than the score
    void UpdateHS() {
        if(!PlayerPrefs.HasKey("RocketHS")) {
            PlayerPrefs.SetInt("RocketHS", Mathf.RoundToInt(score));
        } else {
            int HS = PlayerPrefs.GetInt("RocketHS");
            if (score > HS) {
                PlayerPrefs.SetInt("RocketHS", Mathf.RoundToInt(score));
            }
        }
    }
}
