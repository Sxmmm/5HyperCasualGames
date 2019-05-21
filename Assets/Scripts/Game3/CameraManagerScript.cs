using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerScript : MonoBehaviour {

    //Variables to control the camera and how it shakes
    public Transform camTransform;
    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    //Getting the position to return to.
    Vector3 originalPos;

    //In start we are assigning the start pos and the cameras transform
    void Start() {
        camTransform = GetComponent<Transform>();
        originalPos = camTransform.position;
    }
    //In update if we have called this script with a time it will make the camera shake by randomly moving its position
    //   within a set range of a shpere from the original position
    void Update() {
        if (shakeDuration > 0) {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime;
        } else {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}
