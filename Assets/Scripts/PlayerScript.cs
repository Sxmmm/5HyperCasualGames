using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    //Bool to control if the player can jump
    private bool grounded;
    //Rigidbody to control the player
    private Rigidbody rb;
    [SerializeField]
    private int maxSpeed = 250;
    public int score = 0;
    public int coins = 0;
    private int highscore = 0;
    //Text displayed to the player
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text highscoreText;
    [SerializeField]
    Text coinsText;
    //Gameobject to control the UI
    public GameObject gameoverUI;
    public GameObject startscreenUI;
    private GameObject PlatformManager;
    //Materials to change the colour of the player
    public Material Cyan;
    public Material Pink;
    public Material Purple;
    //Audio variables to control all sounds in game
    private AudioSource audioSource;
    private AudioSource extaudioSource;
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip death;

    //In start we get the audiosources and the rigidbody. Set the UI. Check player prefs to see if the player has coins and a highscore
    void Start () {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
        extaudioSource = GameObject.FindWithTag("ExtSounds").GetComponent<AudioSource>();
        grounded = true;
        rb = gameObject.GetComponent<Rigidbody>();
        startscreenUI.SetActive(true);
        PlatformManager = GameObject.FindWithTag("PlatformSpawner");
        if (PlayerPrefs.HasKey("Highscore")) {
            highscore = PlayerPrefs.GetInt("Highscore");
        } else {
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        if (PlayerPrefs.HasKey("Coins")) {
            coins = PlayerPrefs.GetInt("Coins");
        } else {
            PlayerPrefs.SetInt("Coins", coins);
        }
    }
	//Detecting touch to hide the UI and play the game
	void Update () {
	    //Updating the onscreen text UI
        scoreText.text = score.ToString();
        highscoreText.text = highscore.ToString();
        coinsText.text = coins.ToString();
        //Clamping the players velocity to prevent the player from launching away
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed * Time.fixedDeltaTime);
        if ((Input.touchCount == 1 || Input.GetKey(KeyCode.Mouse0)) && grounded) {
            startscreenUI.SetActive(false);
            grounded = false;
            rb.AddForce(new Vector3(1.5f, 5, 0), ForceMode.Impulse);
            return;
        }
        //Calling the Player colour function
        UpdatePlayerColour();
        //Saving the players coins
        PlayerPrefs.SetInt("Coins", coins);
    }
    //Collision detection
    void OnCollisionEnter(Collision col) {
        //Checking if the player is on the ground
        if(col.gameObject.tag == "Ground") {
            PlatformBounceScript platform = col.gameObject.GetComponent<PlatformBounceScript>();
            if(!grounded && platform.scoreCollected) {
                //Playing the jump sound with differnt pitches
                audioSource.clip = jump;
                audioSource.pitch = (Random.Range(0.6f, 0.9f));
                audioSource.Play();
                //Setting the colour of the platform
                col.gameObject.GetComponent<Renderer>().material.color = Color.green;
                platform.TimeTillDeath();
                platform.scoreCollected = false;
                //Spawning a new platform so we dont run out.
                PlatformManager.GetComponent<PlatformManagerScript>().SpawnPlatform();
                score++;
            }
            grounded = true;
        }
        //Collision with death barrier
        if(col.gameObject.tag == "Death") {
            //Checking highscore
            if (score > PlayerPrefs.GetInt("Highscore")) {
                PlayerPrefs.SetInt("Highscore", score);
            }
            //Playing death sound
            audioSource.clip = death;
            audioSource.Play();
            grounded = false;
            gameoverUI.SetActive(true);
            scoreText.color = Color.white;
        }
    }
    //Checking to see witch platform we failed to reach by changing its colour to red
    void OnCollisionStay(Collision col) {
        if (col.gameObject.tag == "Ground") {
            if (transform.position.y < -3.0f) {
                col.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
    //Trigger for collecting coins
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Coin") {
            extaudioSource.clip = coin;
            extaudioSource.Play();
            coins++;
            PlayerPrefs.SetInt("Coins", coins);
            Destroy(col.gameObject);
        }
    }
    //The function for updating the colour of the player and saving the colour
    void UpdatePlayerColour() {
        if (PlayerPrefs.HasKey("PlayerColour")) {
            switch (PlayerPrefs.GetInt("PlayerColour")) {
                case 0:
                    GetComponent<MeshRenderer>().material = Cyan;
                    break;
                case 1:
                    GetComponent<MeshRenderer>().material = Pink;
                    break;
                case 2:
                    GetComponent<MeshRenderer>().material = Purple;
                    break;
            }
        }
        else {
            PlayerPrefs.SetInt("PlayerColour", 0);
        }
    }
}
