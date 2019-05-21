using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudScript : MonoBehaviour {
    //Variables for the clouds position
    private RectTransform Rt;
    private Vector3 startPos;
    //Variable for time
    private float timer;
    //Array for sprites of the clouds
    [SerializeField]
    private Sprite[] clouds = new Sprite[3];

    // Start to set the timer and get the transform, and the start pos
    void Start () {
        timer = Random.Range(21f, 26f);
        Rt = GetComponent<RectTransform>();
        startPos = Rt.position;
    }
	
	//Update to control our timer. when timer runs out move to start pos and change sprite
	void Update () {
        if (timer > 0) {
            timer -= Time.deltaTime;
            Rt.transform.position = Vector2.Lerp(startPos, new Vector2(Rt.position.x - 1, Rt.position.y), 1f);
        } else {
            Rt.position = startPos;
            gameObject.GetComponent<Image>().sprite = clouds[Random.Range(0, 3)];
            timer = Random.Range(21f, 26f);
            
        }
    }
}
