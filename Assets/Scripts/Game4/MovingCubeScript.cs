using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCubeScript : MonoBehaviour {
    /// This script is used to control the moving cubes. This script will stop the cube when the player taps
    ///     it will check the cube against the previous cube, checking the edges and what side its on. It will
    ///     then split the cube by scaling the original cube and transforming its position and then instigates a
    ///     a new cube in the cutoff position and makes it fall down.
    /// 

    public static MovingCubeScript CurrCube { get; private set; }
    public static MovingCubeScript LastCube { get; private set; }
    public MoveDirection MoveDirection { get; internal set; }

    [SerializeField]
    private float Speed = 1f;

    void OnEnable() {
        if(LastCube == null) {
            LastCube = GameObject.Find("Start").GetComponent<MovingCubeScript>();
        }
        CurrCube = this;
        GetComponent<Renderer>().material.color = GetRandomColour();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private Color GetRandomColour() {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    internal void Stop() {
        if (gameObject.name == "Start") {
            return;
        }
        Speed = 0f;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;
        if (Mathf.Abs(hangover) >= max) {
            LastCube = null;
            CurrCube = null;
            SceneManager.LoadScene(0);
        }

        float direction = hangover > 0 ? 1f : -1f;

        if (MoveDirection == MoveDirection.z) {
            SplitCubeZ(hangover, direction);
        } else {
            SplitCubeX(hangover, direction);
        }

        LastCube = this;
    }

    private float GetHangover() {
        if (MoveDirection == MoveDirection.z) {
            return transform.position.z - LastCube.transform.position.z;
        } else {
            return transform.position.x - LastCube.transform.position.x;
        }
    }

    private void SplitCubeX(float hangover, float direction) {
        float newXSize = LastCube.transform.localScale.x - Math.Abs(hangover);
        float fallBlockSize = transform.localScale.x - newXSize;

        float newXPos = LastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2 * direction);
        float fallingBlockXPos = cubeEdge + fallBlockSize / 2 * direction;

        SpawnDropCube(fallingBlockXPos, fallBlockSize);
    }

    private void SplitCubeZ(float hangover, float direction) {
        float newZSize = LastCube.transform.localScale.z - Math.Abs(hangover);
        float fallBlockSize = transform.localScale.z - newZSize;

        float newZPos = LastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);

        float cubeEdge = transform.position.z + (newZSize / 2 * direction);
        float fallingBlockZPos = cubeEdge + fallBlockSize / 2 * direction;

        SpawnDropCube(fallingBlockZPos, fallBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPos, float fallBlockSize) {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if(MoveDirection == MoveDirection.z) {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPos);
        } else {
            cube.transform.localScale = new Vector3(fallBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPos, transform.position.y, transform.position.z);
        }
        
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        Destroy(cube.gameObject, 1f);
    }

    void Update() {
        if (MoveDirection == MoveDirection.z) {
            transform.position += transform.forward * Time.deltaTime * Speed;
        }
        else {
            transform.position += transform.right * Time.deltaTime * Speed;
        }
    }

}
