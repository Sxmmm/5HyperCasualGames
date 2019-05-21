using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    //Variables for navigation
    public GameObject optionsUI;
    public GameObject startUI;
    private bool isOptions;

	//Setting up the UI for the start
	void Start () {
        isOptions = false;
        optionsUI.SetActive(false);
        startUI.SetActive(true);
    }
    //Play button
    public void PlayButton() {
        SceneManager.LoadScene(1);
    }
    //Options button
    public void OptionsButton() {
        if (!isOptions) {
            optionsUI.SetActive(true);
            startUI.SetActive(false);
            isOptions = true;
        } else {
            optionsUI.SetActive(false);
            startUI.SetActive(true);
            isOptions = false;
        }
    }
    //Reset button
    public void ResetButton() {
        PlayerPrefs.DeleteKey("G5Level");
    }
}
