using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {
    //Variables used for the navigation of our UI
    private bool shopOpen = false;
    public GameObject shopPanel;
    public GameObject gameoverPanel;
    public Text blue, pink, purple;
    //Variables for audio
    private AudioSource audioSource;
    public AudioClip Buy;

    //Start used to get the audiosource
    void Start() {
        audioSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
    }

    //Button used to reload the game
    public void Retry() {
        SceneManager.LoadScene("Game2");
    }
    //Button used to open and close the shop
    public void Shop() {
        if(shopOpen) {
            shopPanel.SetActive(false);
            gameoverPanel.SetActive(true);
            shopOpen = false;
        } else {
            shopPanel.SetActive(true);
            UpdateItems();
            gameoverPanel.SetActive(false);
            shopOpen = true;
        }
    }
    //Button used to use coins to purchase different colours for the player.
    // The colour brought will be saved as the player colour.
    public void Blue() {
        int coins = PlayerPrefs.GetInt("Coins");
        if(coins >= 10) {
            if (PlayerPrefs.GetInt("PlayerColour") != 0) {
                audioSource.clip = Buy;
                audioSource.Play();
                PlayerPrefs.SetInt("PlayerColour", 0);
                GameObject.FindWithTag("Player").GetComponent<PlayerScript>().coins -= 10;
                UpdateItems();
            }
        }
    }
    public void Pink() {
        int coins = PlayerPrefs.GetInt("Coins");
        if (coins >= 10) {
            if (PlayerPrefs.GetInt("PlayerColour") != 1) {
                audioSource.clip = Buy;
                audioSource.Play();
                PlayerPrefs.SetInt("PlayerColour", 1);
                GameObject.FindWithTag("Player").GetComponent<PlayerScript>().coins -= 10;
                UpdateItems();
            }
        }
    }
    public void Purple() {
        int coins = PlayerPrefs.GetInt("Coins");
        if (coins >= 10) {
            if (PlayerPrefs.GetInt("PlayerColour") != 2) {
                audioSource.clip = Buy;
                audioSource.Play();
                PlayerPrefs.SetInt("PlayerColour", 2);
                GameObject.FindWithTag("Player").GetComponent<PlayerScript>().coins -= 10;
                UpdateItems();
            }
        }
    }
    //A function used to display which colour the player has to prevent spending coins on the colour already brought
    void UpdateItems() {
        if (PlayerPrefs.HasKey("PlayerColour")) {
            switch (PlayerPrefs.GetInt("PlayerColour")) {
                case 0:
                    blue.text = "X";
                    pink.text = "10";
                    purple.text = "10";
                    break;
                case 1:
                    blue.text = "10";
                    pink.text = "X";
                    purple.text = "10";
                    break;
                case 2:
                    blue.text = "10";
                    pink.text = "10";
                    purple.text = "X";
                    break;
            }
        }
    }
}
