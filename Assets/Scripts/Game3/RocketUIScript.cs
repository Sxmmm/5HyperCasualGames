using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RocketUIScript : MonoBehaviour {
    //Variable to hold the highscore
    public Text HighScore;
    //Updating the UI Text
    void Update() {
        HighScore.text = PlayerPrefs.GetInt("RocketHS").ToString("n0");
    }
    //Button for Retry. reloads the scene
    public void Retry() {
        SceneManager.LoadScene("Game3");
    }
    //Button for reset. it ressets the player prefs.
    public void Reset() {
        GameObject.FindWithTag("Rocket").GetComponent<RocketScript>().score = 0;
        PlayerPrefs.DeleteKey("RocketHS");
        PlayerPrefs.SetInt("RocketHS", 0);
        HighScore.text = PlayerPrefs.GetInt("RocketHS").ToString();
    }

}
