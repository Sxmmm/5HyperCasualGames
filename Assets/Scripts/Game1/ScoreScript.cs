using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour {
    //Setting up variables to hold our score and to detect if we can score
    [SerializeField]
    private int score;
    private bool canScore;
    //Variables to do with UI
    public TextMeshProUGUI scoreText, scoreText1;
    public TextMeshProUGUI highscoreText;
    public GameObject newhsText;
    public GameObject failedUI;
    //Variables to do with audio
    private AudioSource _audioSource;
    public AudioSource _extaudioSource;
    public AudioClip caughtSound;

	void Start () {
        //Getting the audio source
        _audioSource = GetComponent<AudioSource>();
        //Setting the correct UI states for our loaded game
        newhsText.SetActive(false);
        failedUI.SetActive(false);
        canScore = true;
        //Checking to see if we have a highscore saved
		if(PlayerPrefs.HasKey("G1Highscore")) {
            highscoreText.text = PlayerPrefs.GetInt("G1Highscore").ToString();
        } else {
            PlayerPrefs.SetInt("G1Highscore", 0);
        }
	}
	
	void Update () {
        //Displaying the score
        scoreText.text = score.ToString();
        //Saving the new highscore
		if(score > PlayerPrefs.GetInt("G1Highscore")) {
            highscoreText.text = score.ToString();
            PlayerPrefs.SetInt("G1Highscore", score);
            newhsText.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider col) {
        //Trigger detection for the police car to gain points
        if(col.gameObject.tag == "PoliceCar") {
            if (canScore) {
                score++;
            }
        }
        //Trigger detection for our player to determine if the game is still in play
        if (col.gameObject.tag == "Player") {
            _extaudioSource.Stop();
            canScore = false;
            scoreText1.text = score.ToString();
            failedUI.SetActive(true);
            _audioSource.clip = caughtSound;
            _audioSource.Play();
        }
    }
}
