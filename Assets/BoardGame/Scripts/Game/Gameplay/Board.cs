using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class Board : MonoBehaviour
    {
        [SerializeField]
        private Transform cellsParent;

        [SerializeField]
        private Player player;

        private List<Cell> cells;

        private int cellWithPlayerIndex;

        public void Initialize()
        {
            cells = GetCells();
            SetPlayerToCell(player, 0);
        }

        public void MovePlayerOnCells(int count)
        {
            MovePlayerOnCells(player, count);
        }

        private void SetPlayerToCell(Player player, int index)
        {
            if (player == null)
            {
                return;
            }

            cellWithPlayerIndex = index;
            var cell = cells[index];

            // A View code.
            player.transform.position = cell.transform.position;
            player.transform.rotation = cell.transform.rotation;
        }

        private void MovePlayerOnCells(Player player, int count)
        {
            var nextCellIndex = cellWithPlayerIndex + count;

            if (nextCellIndex < 0 || nextCellIndex > cells.Count - 1)
            {
                return;
            }

            SetPlayerToCell(player, nextCellIndex);
        }

        private List<Cell> GetCells()
        {
            var cellsCount = cellsParent.childCount;
            var cells = new List<Cell>(cellsCount);

            for (int i = 0; i < cellsCount; i++)
            {
                var cell = cellsParent.GetChild(i).GetComponent<Cell>();

                if (cell == null)
                {
                    continue;
                }

                cells.Add(cell);
            }

            return cells;
        }

#if UNITY_EDITOR
        #region DEBUG

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MovePlayerOnCells(player, 1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                MovePlayerOnCells(player, -1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SetPlayerToCell(player, 0);
            }
        }

        #endregion

        [Button]
        private void NumerateCells()
        {
            int number = 1;

            var cells = transform.Find("Cells");

            if (cells == null)
            {
                Debug.LogError("Cells are not found.");
                return;
            }

            foreach (Transform cell in cells)
            {
                cell.name = $"Cell{number}";
                var cellText = cell.GetComponentInChildren<TextMesh>();

                if (cellText != null)
                {
                    cellText.text = number.ToString();
                }

                number++;
            }

            Debug.Log($"Cells has been successfully numerated.");
        }
#endif
    }
}
