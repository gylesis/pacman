using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities {

    public static class Utils {

        public static Vector2 GetDirectionFromAngle(float angle) {
            switch (angle) {
                case float angl when (angl >= 45 && angl < 135):
                    return Vector2.up;
                case float angl when (angl >= 135 && angl < 225):
                    return Vector2.left;
                case float angl when (angl >= 225 && angl < 315):
                    return Vector2.down;
                case float angl when (angl >= 315 || angl < 45):
                    return Vector2.right;
                default:
                    return Vector2.zero;
            }
        }

        public static int GetGridIndexOfCell(Vector2 pos) {
            foreach (var obj in GridGeneration.cells) {
                if (obj.currentPos == pos) {
                    return obj.gridIndex;
                }
            }
            return -1;
        }

        public static Vector2 GetRandomDirection() {

            var randomDirectionX = Random.Range(-1, 2);
            var randomDirectionY = Random.Range(-1, 2);

            if (randomDirectionX == -1 || randomDirectionX == 1) {
                randomDirectionY = 0;
            }
            else if (randomDirectionX == 0) {

                var a = Random.value;
                var b = Random.value;
                if (a > b) randomDirectionY = 1;
                else randomDirectionY = -1;
            }

            return new Vector2(randomDirectionX, randomDirectionY);

        }

        public static Vector2 GetRandomDirectionFromAvailableDirections(Cell cell) {
            var startCell = cell;
            var neigbours = GetNeighbors(startCell);

            var randomCell = neigbours[Random.Range(0, neigbours.Count)];

            var randomDirection = GetDirectionFromPoints(startCell.currentPos, randomCell.currentPos);

            float angle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;

            if (angle < 0) {
                angle = 360 - Mathf.Abs(angle);
            }

            switch (angle) {
                case float angl when (angl >= 45 && angl < 135):
                    return Vector2.up;
                case float angl when (angl >= 135 && angl < 225):
                    return Vector2.left;
                case float angl when (angl >= 225 && angl < 315):
                    return Vector2.down;
                case float angl when (angl >= 315 || angl < 45):
                    return Vector2.right;
                default:
                    return Vector2.zero;
            }
        }

        public static Vector2 GetDirectionFromPoints(Vector2 point1, Vector2 point2) {
            var direction = (point2 - point1).normalized;
            return direction;
        }

        public static List<Cell> GetNeighbors(Cell cell) {

            int cellPosX = (int)cell.currentPos.x;
            int cellPosY = (int)cell.currentPos.y;
            var neighbors = new List<Cell>();

            for (int y = cellPosY - 1; y <= cellPosY + 1; y++) {
                for (int x = cellPosX - 1; x <= cellPosX + 1; x++) {
                    if (y < 0 || y >= GridGeneration.gridSizeY || x < 0 || x >= GridGeneration.gridSizeX) continue;
                    if (x == cellPosX && y == cellPosY) continue;
                    if (x == cellPosX + 1 && y == cellPosY - 1) continue;
                    if (x == cellPosX - 1 && y == cellPosY + 1) continue;
                    if (x == cellPosX + 1 && y == cellPosY + 1) continue;
                    if (x == cellPosX - 1 && y == cellPosY - 1) continue;

                    var pos = new Vector2(x, y);
                    var cellPosToFind = GetGridIndexOfCell(pos);

                    var cellToAdd = GridGeneration.cells[cellPosToFind];
                    if (cellToAdd.isWall) continue;
                    if (cellToAdd.isWalkable) continue;

                    neighbors.Add(cellToAdd);
                }
            }
            return neighbors;

        }

        public static Vector2 GetStepsFromToWall(Vector2 currentPos, Vector2 direction) {
            var startPoint = currentPos + direction;

            int counter = 0;
            for (int y = (int)startPoint.y; y < GridGeneration.gridSizeY; y++) {

                for (int x = (int)startPoint.x; x < GridGeneration.gridSizeX; x++) {
                    var cellToCheck = GridGeneration.cells[GetGridIndexOfCell(startPoint)];
                    if (cellToCheck.isWalkable == false) return direction * counter;

                    x -= (int)startPoint.x;
                    startPoint += direction;
                    counter++;
                }
                y -= (int)startPoint.y;
            }
            return direction * counter;
        }


    }

}


