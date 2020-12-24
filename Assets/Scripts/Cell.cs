using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class Cell : MonoBehaviour {

    public Vector2 currentPos;

    [SerializeField]
    public Sprite shelfImg;

    [SerializeField]
    Sprite roadImg;

    [SerializeField]
    public Sprite wallImg;

    [SerializeField]
    public SpriteRenderer _currentSprite;

    [SerializeField]
    public SpriteRenderer _neiborIMG;

    [SerializeField]
    private Text currentIndexShow;

    public int gridIndex;

    public bool isWalkable = true;

    public bool isWall = false;

    public static Cell currentCell;

    public static Cell checkingCell;

    public static Cell prevCell;

    public void OnSpawn(Vector2 pos, int _gridIndex, bool isWalkable, bool isWall) {
        currentPos = pos;
        gridIndex = _gridIndex;
        GridGeneration.onSwitch += OnSwitch;
        GridGeneration.onSwitch.Invoke();
        this.isWalkable = isWalkable;
        this.isWall = isWall;

        if (isWall) gameObject.tag = "Wall";

    }

    private void Update() {
        SetFitSprites();
    }

    private void SetFitSprites() {
        if (isWall) {
            _currentSprite.sprite = wallImg;
        }
        if (!isWall && !isWalkable) {
            _currentSprite.sprite = shelfImg;
        }
        if (!isWall && isWalkable) {
            _currentSprite.sprite = roadImg;
        }

        if (prevCell == this) {
            _currentSprite.color = Color.green;
        }
        else if (prevCell != this || checkingCell != this) {
            _currentSprite.color = Color.white;
        }

        if (checkingCell == this) {
            _currentSprite.color = Color.red;
        }

    }

    public void OnSwitch() {
        currentCell = this;
    }

    public void SetSpriteColor(Sprite sprite) {
        _currentSprite.sprite = sprite;
    }

    public void Checking() {
        StartCoroutine(CheckingCell());
    }


    IEnumerator CheckingCell() {
        var neibors = Utils.GetNeighbors(this);

        foreach(var o in neibors) {
            o._neiborIMG.enabled = true;
        }

        yield return new WaitForSeconds(0.4f);

        foreach (var o in neibors) {
            o._neiborIMG.enabled = false;
        }
    }


}

