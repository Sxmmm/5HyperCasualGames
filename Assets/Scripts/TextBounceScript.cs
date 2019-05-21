using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBounceScript : MonoBehaviour {
    //float variables to modify the script so we can use this in multiple scenarios.
    public float waveSpeedMultiplier = 7;
    public float waveHeight = 6;
    public float waveFrequency = 1f;
    //Variables used to alter the text
    private RectTransform rt;
    private Vector2 originalAnchor;
    private float wave;

    void Start() {
        //Assigning the rectTransform to the gameobject that this is attatched to 
        rt = GetComponent<RectTransform>();
    }
    //Creating a wave effect to create a bounce like pattern along the texts.
    void Update() {
        if (originalAnchor == Vector2.zero) {
            if (rt.anchoredPosition == Vector2.zero)
                return;

            originalAnchor = rt.anchoredPosition;
        }

        wave += Time.deltaTime * waveSpeedMultiplier;
        rt.anchoredPosition = originalAnchor + new Vector2(0, Mathf.Sin((wave + rt.anchoredPosition.x) * waveFrequency) * waveHeight);
    }
}
