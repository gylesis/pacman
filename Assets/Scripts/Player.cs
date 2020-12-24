using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;


public class Player : MonoBehaviour {

    public static Vector2 currentPos;

    Vector2 direction;
    Vector2 point1;
    Vector2 point2;

    float angle;

    [SerializeField]
    Text text;

    [SerializeField]
    LineRenderer line;

    public bool inMove = false;

    private void Start() {
        currentPos = new Vector2(5,5);
    }

    void Update() {      
        transform.position = Vector2.Lerp(transform.position, currentPos, 0.2f);
        Move();
    }


    private void Move() {
        if (Input.GetMouseButtonDown(0)) {
            point1 = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)) {
            point2 = Input.mousePosition;
            direction = point2 - point1;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle < 0) {
                angle = 360 - Mathf.Abs(angle);
            }
        }
        if (Input.GetMouseButtonUp(0)) {        
            var steps = Utils.GetStepsFromToWall(currentPos,Utils.GetDirectionFromAngle(angle));
           // Debug.Log(steps);
            currentPos += steps;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.tag == "Wall") {
            Debug.Log("Bum");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
    }


}
