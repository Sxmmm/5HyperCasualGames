using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {
    //Variables used to hold the prefab of the cube and a enum for the direction its coming from
    [SerializeField]
    private MovingCubeScript cubePrefab;
    [SerializeField]
    private MoveDirection moveDirection;
    //Function to spawn the cube. Checking what the enum moveDirection is set to so our scripts know what
    //  axis we need to transform our cubes on.
    public void SpawnCube() {
        var cube = Instantiate(cubePrefab);

        if (MovingCubeScript.LastCube != null && MovingCubeScript.LastCube.gameObject != GameObject.Find("Start")) {
            float x = moveDirection == MoveDirection.x ? transform.position.x : MovingCubeScript.LastCube.transform.position.x;
            float z = moveDirection == MoveDirection.z ? transform.position.z : MovingCubeScript.LastCube.transform.position.z;

            cube.transform.position = new Vector3(x, MovingCubeScript.LastCube.transform.position.y + cubePrefab.transform.localScale.y, z);
        }
        else {
            cube.transform.position = transform.position;
        }

        cube.MoveDirection = moveDirection;
    }
    //Gizmos used so I can see where the cubes are spawned from
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}
