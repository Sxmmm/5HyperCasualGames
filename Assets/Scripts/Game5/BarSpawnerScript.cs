using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarSpawnerScript : MonoBehaviour {
    //Variables to hold the bar prefab and the panel for the next level UI
    public GameObject movingBar;
    public GameObject nextLevelPanel;
    //Text to display the level
    public TextMeshProUGUI levelText;
    //Ints for the level and speed 
    [SerializeField]
    private int levelNumber, spawns;

    private float timer;
    //Start to check if the player has a level saved
	void Start () {
        if (!PlayerPrefs.HasKey("G5Level")) {
            PlayerPrefs.SetInt("G5Level", 1);
        }
        levelNumber = PlayerPrefs.GetInt("G5Level");
        spawns = levelNumber + 10;
        timer = Random.Range(1.5f, 3.5f);
    }
	
	//Update for displaying the level the player is on and spawning the correct amount of bars
    //  The formula for the speed is (2 + 0.25 * the level number)
	void Update () {
        levelText.text = "Level " + levelNumber;

        if (spawns > 0) {
            nextLevelPanel.SetActive(false);
            if (timer > 0) {
                timer -= Time.deltaTime;
            }
            if (timer <= 0) {
                var go = Instantiate(movingBar, transform.position, Quaternion.identity);
                go.GetComponent<MovingBarScript>().speed = 2 + levelNumber * 0.25f;
                timer = Random.Range(1.5f, 3.5f);
                spawns--;
                if(spawns == 0) {
                    timer = 5f;
                }
            }
        } else {
            timer -= Time.deltaTime;
            if (timer < 0) {
                nextLevelPanel.SetActive(true);
                if (Input.GetButtonDown("Fire1")) {
                    levelNumber++;
                    PlayerPrefs.SetInt("G5Level", levelNumber);
                    levelNumber = PlayerPrefs.GetInt("G5Level");
                    spawns = levelNumber + 10;
                }
            }
        }
    }
}
