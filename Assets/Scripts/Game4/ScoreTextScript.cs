using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextScript : MonoBehaviour {
    //Variables from score and highscore
    private int score;
    private TextMeshProUGUI text;
    public TextMeshProUGUI HStext;

    void Start() {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        G4GameManagerScript.OnCubeSpawned += G4GameManagerScript_OnCubeSpawned;
    }
    //Removing
    void OnDestroy() {
        G4GameManagerScript.OnCubeSpawned -= G4GameManagerScript_OnCubeSpawned;
    }
    //Adding score
    private void G4GameManagerScript_OnCubeSpawned() {
        score++;
        text.text = score.ToString();
    }
    //Checking Highscore
    void Update() {
        if (!PlayerPrefs.HasKey("G4HS")) {
            PlayerPrefs.SetInt("G4HS", 0);
        } else {
            if (score > PlayerPrefs.GetInt("G4HS")) {
                PlayerPrefs.SetInt("G4HS", score);
            }
        }
        HStext.text = PlayerPrefs.GetInt("G4HS").ToString();
    }
}
