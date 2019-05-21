using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

    //UI Script for ingame
    //Rerty Button
    public void Retry() {
        SceneManager.LoadScene(1);
    }
    //Menu Button
    public void Menu() {
        SceneManager.LoadScene(0);
    }
}
