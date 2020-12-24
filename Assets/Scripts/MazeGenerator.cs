using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class MazeGenerator : MonoBehaviour {

    public static Stack<Cell> passedCells = new Stack<Cell>();

    Vector2 currentPos;
    Vector2 direction;

    int counter = 0;

    private void Start() {
        // Invoke("GenerateMaze",2f);

        StartCoroutine(GenerateMaze());
        //   StartCoroutine(gh());
        //  Invoke("gh", 0.5f);


    }



    IEnumerator gh() {
        while (true) {
            yield return new WaitForSeconds(3f);
            var cell = GridGeneration.cells[Utils.GetGridIndexOfCell(new Vector2(8, 8))];

            Utils.GetRandomDirectionFromAvailableDirections(cell);

            var direct = Utils.GetRandomDirectionFromAvailableDirections(cell);

            Debug.Log(direct);

            /*foreach (var b in direct) {
                b.Checking();
            }*/

        }
    }



    IEnumerator GenerateMaze() {
        yield return new WaitForSeconds(1f);
        var randomPosX = Random.Range(1, GridGeneration.gridSizeX - 1);
        var randomPosY = Random.Range(1, GridGeneration.gridSizeY - 1);
        var startPos = new Vector2(randomPosX, randomPosY);

        currentPos = startPos;

        direction = Utils.GetRandomDirectionFromAvailableDirections(GridGeneration.cells[Utils.GetGridIndexOfCell(currentPos)]);
        var tempPos = currentPos + direction;
        var checkingCell = GridGeneration.cells[Utils.GetGridIndexOfCell(tempPos)];

        var prevCell = GridGeneration.cells[Utils.GetGridIndexOfCell(tempPos - direction)];
        passedCells.Push(checkingCell);

        Cell.checkingCell = checkingCell;
        Cell.prevCell = prevCell;

        do {
            yield return new WaitForSeconds(0.2f);

            if (!checkingCell.isWall) {
                Debug.Log("not Wall");
                prevCell = GridGeneration.cells[Utils.GetGridIndexOfCell(tempPos)];

                currentPos += direction;
                tempPos = currentPos + direction;

                checkingCell.isWalkable = true;
                checkingCell = GridGeneration.cells[Utils.GetGridIndexOfCell(tempPos)];

                Cell.checkingCell = checkingCell;
                Cell.prevCell = prevCell;
                passedCells.Push(checkingCell);

                
            }
            else if (checkingCell.isWall) {
                Debug.Log("Wall");              
                direction = Utils.GetRandomDirectionFromAvailableDirections(prevCell);

                tempPos = currentPos + direction;

                checkingCell = GridGeneration.cells[Utils.GetGridIndexOfCell(tempPos)];
                Cell.checkingCell = checkingCell;
             //   tempPos = currentPos - direction;
            }
            
            if(passedCells.Count % 5 == 0) {
                var tempDirection = Utils.GetRandomDirectionFromAvailableDirections(prevCell);
                var tempPos2 = prevCell.currentPos + tempDirection;

                var tempCheckingCell = GridGeneration.cells[Utils.GetGridIndexOfCell(tempPos2)];

                if (GridGeneration.walls.Contains(tempCheckingCell)) continue;

                Debug.Log("Swap");
                tempPos = currentPos + direction;
                checkingCell = tempCheckingCell;
               // currentPos = tempPos;

                direction = tempDirection;

                Cell.prevCell = checkingCell;
               // tempPos = currentPos - direction;
            }


            
        }
        while (passedCells.Count > 0);

    }
}
