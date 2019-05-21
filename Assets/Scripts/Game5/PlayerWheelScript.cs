using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerWheelScript : MonoBehaviour {
    //Variables for detecting swipes
    private Vector2 startTouchPos, endTouchPos;
    //Float for the rotation of the wheel
    private float playerRotation;
    private Quaternion startRot;
    private Quaternion targetRot;
    //Speed of the rotation
    [SerializeField]
    private float speed = 25f;
    //Array for the colours
    private Color[] cols = new Color[4];
    public GameObject colourBar;
    private int topColour;
    public Text swipeText;
    //Variables for the audio
    public AudioSource _adSource;
    private float pitch;

    //In start we assign audio and colours.
    void Start () {
        _adSource = GetComponent<AudioSource>();
        pitch = 0.7f;
        cols[0] = new Color(1f, 0f, 0.1647f, 1f);   //Red
        cols[1] = new Color(0f, 0.8491f, 1f, 1f);   //Blue
        cols[2] = new Color(1f, 0.9411f, 0f, 1f);   //Yellow
        cols[3] = new Color(0.8f, 0f, 1f, 1f);      //Purple
        playerRotation = 0f;
        startRot = transform.rotation;
        swipeText.CrossFadeAlpha(0f, 0.0f, false);
        swipeText.CrossFadeAlpha(1.0f, 0.2f, false);
    }
	
	//In update we call two differnt functions
	void Update () {
        DetectSwipe();
        DetectColour();
    }
    //Detecting colour and assigning the top colour so we can detect the if the colour is the same as the line
    private void DetectColour() {
        if (Math.Round(transform.localEulerAngles.z) == 0) {
            topColour = 0;
            colourBar.GetComponent<Image>().color = cols[0];
        }
        if (Math.Round(transform.localEulerAngles.z) == 90) {
            topColour = 1;
            colourBar.GetComponent<Image>().color = cols[1];
        }
        if(Math.Round(transform.localEulerAngles.z) == 180) {
            topColour = 2;
            colourBar.GetComponent<Image>().color = cols[2];
        }
        if(Math.Round(transform.localEulerAngles.z) == 270) {
            topColour = 3;
            colourBar.GetComponent<Image>().color = cols[3];
        }
    }

    private void DetectSwipe() {
        // MOBILE TOUCH SWIPE CONTROLS
        Quaternion targetRot = Quaternion.Euler(0, 0, playerRotation) * startRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * speed);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            startTouchPos = Input.GetTouch(0).position;
        }
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            endTouchPos = Input.GetTouch(0).position;

            swipeText.CrossFadeAlpha(0f, 0.3f, false);

            //Swipe Right
            if (endTouchPos.x > startTouchPos.x) {
                playerRotation -= 90;
            }
            //Swipe Left
            if(endTouchPos.x < startTouchPos.x) {
                playerRotation += 90;
            }
        }

        // PC CONTROLS
        if (Input.GetKeyDown(KeyCode.A)) {
            swipeText.CrossFadeAlpha(0f, 0.3f, false);
            playerRotation += 90;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            swipeText.CrossFadeAlpha(0f, 0.3f, false);
            playerRotation -= 90;
        }
    }
    //Detecing to see if the colour on the wheel mathces the falling line
    void OnTriggerEnter2D (Collider2D col) {
        if(col.gameObject.tag == topColour.ToString()) {
            Destroy(col.gameObject);
            pitch += 0.025f;
            if (pitch > 1.25f)
                pitch = 0.7f;
            _adSource.pitch = pitch;
            _adSource.Play();
        } else {
            //Dont have an android device that supports vibration. Hopefully this works well with the player loosing
            Handheld.Vibrate();
            SceneManager.LoadScene(0);
        }
    }
}
