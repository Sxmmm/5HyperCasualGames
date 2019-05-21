using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDontDestroy : MonoBehaviour {
    //Variable for music
    private AudioSource _audioSource;
    //Dont destroy so we can play through all scene without the object duplicating
	void Awake() {
        GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("Music");
        if(musicObjs.Length > 1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }
}
