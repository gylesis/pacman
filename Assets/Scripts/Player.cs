using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    int counter;

    private void Start() {
        Debug.Log("qq");
        currentPos = Vector2.one;

    }

    void Update() {
        //currentPos = GridGeneration.currentPos;
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
            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + 90;         
        }
        if (Input.GetMouseButtonUp(0)) {
            GetDirectionFromAngle(angle);

            var steps = GetStepsToWall(GetDirectionFromAngle(angle));
            Debug.Log(steps);
            currentPos += steps;
            
        }
    }

    public Vector2 GetDirectionFromAngle(float angle) {
        switch (angle) {
            case float n when (n < 135 && n > 45):
                text.text = "Up";
                return Vector2.up;
            case float n when (n > 135 && n < 225):
                text.text = "Right";
                return Vector2.right;
            case float n when (n < -45 || n > 225):
                text.text = "Down";
                return Vector2.down;
            case float n when (n < 45 && n > -45):
                text.text = "Left";
                return Vector2.left;
            default:
                text.text = "Nana";
                return Vector2.zero;
        }
    }

    private Vector2 GetStepsToWall(Vector2 direction) {
        var startPoint = currentPos + direction;
  
        counter = 0;
        for (int y = (int)startPoint.y; y < GridGeneration.gridSizeY; y++) {

            for (int x = (int)startPoint.x; x < GridGeneration.gridSizeX; x++) {
                var cellToCheck = GridGeneration.cells[GridGeneration.GetGridIndexOfCell(startPoint)];
                if (cellToCheck.isWalkable == false) return direction * counter ;
                
                startPoint += direction;
                counter++;            
            }
          
        }
        Debug.Log(counter);
        return direction * counter;
    }





    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(point1, 1);
        Gizmos.DrawWireSphere(point2, 1);
    }


}
