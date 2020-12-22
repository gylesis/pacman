﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGeneration : MonoBehaviour {

    public static GridGeneration Instance;

    [SerializeField]
    private GameObject cellPrefab;

    public List<Vector2> _cells = new List<Vector2>();

    public static int gridSizeX = 50;
    public static int gridSizeY = 50;

    public static List<Cell> cells = new List<Cell>();

    public static Vector2 currentPos;

    public delegate void OnSwitch();
    public static OnSwitch onSwitch;

    private void Start() {
        Instance = this;
        GenerateGrid();
    }

    private void Update() {
        MoveCurrentPos();
    }

    public void GenerateGrid() {
        for (int y = 0; y < gridSizeY; y++) {
            for (int x = 0; x < gridSizeX; x++) {
                var pos = new Vector3(x, y, 0);

                var cell = Instantiate(cellPrefab, transform.position + pos, Quaternion.identity, transform).GetComponent<Cell>();
                cells.Add(cell);
                bool isWalkable = true;
                if (x == 0 || y == 0 || x == gridSizeX - 1 || y == gridSizeY - 1 ) {                  
                    isWalkable = false;
                    cell.SetSpriteColor(Color.red);
                }



                cell.OnSpawn(pos, cells.Count - 1, isWalkable);

                _cells.Add(pos);
            }
        }
    }

    public void MoveCurrentPos() {
        if (Input.anyKeyDown) {
           Player.currentPos += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Debug.Log(GetDistance(Cell.currentCell, cells[GetGridIndexOfCell(new Vector2(9, 9))]));

            if (Player.currentPos.x > GridGeneration.gridSizeX - 1) Player.currentPos.x = GridGeneration.gridSizeX - 1;
            if (Player.currentPos.y > GridGeneration.gridSizeY - 1) Player.currentPos.y = GridGeneration.gridSizeY - 1;
            if (Player.currentPos.y < 0) Player.currentPos.y = 0;
            if (Player.currentPos.x < 0) Player.currentPos.x = 0;


            Cell.currentCell = GridGeneration.cells[GridGeneration.GetGridIndexOfCell(Player.currentPos)];
            Cell.currentCell.OnSwitch();

          
        }
    }

    public static List<Cell> GetNeighbors(Cell cell) {

        int cellPosX = (int)cell.currentPos.x;
        int cellPosY = (int)cell.currentPos.y;
        var neighbors = new List<Cell>();

        for (int y = cellPosY - 1; y <= cellPosY + 1; y++) {
            for (int x = cellPosX - 1; x <= cellPosX + 1; x++) {
                if (y < 0 || y >= gridSizeY || x < 0 || x >= gridSizeX) continue;
                if (x == cellPosX && y == cellPosY) continue;

                var pos = new Vector2(x, y);
                var cellPosToFind = GetGridIndexOfCell(pos);

                neighbors.Add(cells[GetGridIndexOfCell(pos)]);
            }
        }
        return neighbors;

    }

    public static int GetGridIndexOfCell(Vector2 pos) {
        foreach (var obj in cells) {
            if (obj.currentPos == pos) {
                return obj.gridIndex;
            }
        }
        return -1;
    }


    public int GetDistance(Cell cell1, Cell cell2) {
        int distanceX = (int)Mathf.Abs(cell1.currentPos.x - cell2.currentPos.x);
        int distanceY = (int)Mathf.Abs(cell1.currentPos.y - cell2.currentPos.y);

        if (distanceX > distanceY) {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceX + 10 * (distanceY - distanceX);

    }


    public static void FindPath(Cell cell1,Cell cell2) {




    }




}
