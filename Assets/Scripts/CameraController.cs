using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private Camera camera;

    float height;

    [SerializeField]
    public float offsetX;

    [SerializeField]
    public float offsetY;

    private void Start() {
      //  transform.position = new Vector3(GridGeneration.gridSizeX / 2, GridGeneration.gridSizeY / 2, transform.position.z);
    }


    private void Update() {

        /* var playerPos = new Vector3(Mathf.Clamp(transform.position.x, GridGeneration.gridSizeX / 5.88f, GridGeneration.gridSizeX / 1.23f),
             Mathf.Clamp(transform.position.y, GridGeneration.gridSizeY / 11.1f, GridGeneration.gridSizeY / 1.1315f),
             transform.position.z);*/

        var playerPos = new Vector3(Player.currentPos.x, Player.currentPos.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, playerPos, 0.3f);
    }


}
