using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    public Vector2 currentPos;

    [SerializeField]
    public SpriteRenderer _currentSprite;

    [SerializeField]
    private Text currentIndexShow;

    public int gridIndex;

    public bool isWalkable = true;

    public static Cell currentCell;

    public void OnSpawn(Vector2 pos, int _gridIndex,bool isWalkable) {
        currentPos = pos;
        gridIndex = _gridIndex;
        GridGeneration.onSwitch += OnSwitch;
        GridGeneration.onSwitch.Invoke();
        this.isWalkable = isWalkable;

       // if (!isWalkable) _currentSprite.color = Color.red;
    }

    private void Update() {
        if (Player.currentPos == currentPos) {
            SetSpriteColor(Color.yellow);
          //  currentCell = this;
        }
        if (!isWalkable) {
            SetSpriteColor(Color.red);
        }
        else if (isWalkable) SetSpriteColor(Color.white);
    }


    public void OnSwitch() {
         currentCell = this;  
        /* if (Player.currentPos == currentPos) {
             SetSpriteColor(Color.yellow);
             currentCell = this;
         }
        else ResetColor();*/
    }

    public void SetSpriteColor(Color color) {
        _currentSprite.color = color;
    }

    public static void ResetColor() {
        foreach (var cel in GridGeneration.cells) {
            if (!(cel == currentCell) && cel.isWalkable ) {
                cel.SetSpriteColor(Color.white);
            }
        }
    }

}
