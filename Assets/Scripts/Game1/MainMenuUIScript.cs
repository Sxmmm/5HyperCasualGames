using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuUIScript : MonoBehaviour {

    //TextMesh to hold the highscore text so it can be seen on the start screen
    public TextMeshProUGUI Highscore;
    //Variables for the navigation of the start screen
    private bool isOptions;
    public GameObject startUI, optionsUI;

	void Start () {
        //Setting our UI to the correct states for the start up
        startUI.SetActive(true);
        optionsUI.SetActive(false);
        isOptions = false;
        //Checking to see if the game has a highscore already saved
        if (PlayerPrefs.HasKey("G1Highscore")) {
            Highscore.text = PlayerPrefs.GetInt("G1Highscore").ToString();
        }
	}
    //Button for the options menu. Can be used to open and close the options screen
    public void Options() {
        if(isOptions) {
            optionsUI.SetActive(false);
            startUI.SetActive(true);
            isOptions = false;
        } else {
            optionsUI.SetActive(true);
            startUI.SetActive(false);
            isOptions = true;
        }
    }
    //Button the start the main game
    public void Play() {
        SceneManager.LoadScene(1);
    }
    //Button to reset the highscore
    public void Reset() {
        PlayerPrefs.SetInt("G1Highscore", 0);
        Highscore.text = PlayerPrefs.GetInt("G1Highscore").ToString();
    }
}
