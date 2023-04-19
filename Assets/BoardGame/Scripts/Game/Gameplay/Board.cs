using BoardGame.Utils;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class Board : MonoBehaviour
    {
        private event Action HasPlayerReachedLastCell;

        [SerializeField]
        private Transform cellsParent;

        [SerializeField]
        private Player player;

        [SerializeField]
        private CellsGenerator cellsGenerator;

        private List<Cell> cells;

        private int cellWithPlayerIndex;

        public void Initialize(Action hasPlayerReachedLastCell)
        {
            GenerateCells();
            SetPlayerToCell(player, 0);
            HasPlayerReachedLastCell += hasPlayerReachedLastCell;
        }

        public void GenerateCells()
        {
            cellsGenerator.Generate();
            NumerateCells();
            cells = GetCells();
        }

        public void MovePlayerOnCells(int count)
        {
            MovePlayerOnCells(player, count);
        }

        public void ResetPlayerPosition()
        {
            SetPlayerToCell(player, 0);
        }

        private void SetPlayerToCell(Player player, int index)
        {
            if (player == null || cellWithPlayerIndex == index)
            {
                return;
            }

            var lastCellIndex = cells.Count - 1;

            if (index < 0 || index > lastCellIndex)
            {
                return;
            }

            cellWithPlayerIndex = index;

            // A View code.
            var cell = cells[index];
            player.transform.position = cell.transform.position;
            player.transform.rotation = cell.transform.rotation;

            if (index == lastCellIndex)
            {
                HasPlayerReachedLastCell?.Invoke();
            }
        }

        private void MovePlayerOnCells(Player player, int count)
        {
            SetPlayerToCell(player, cellWithPlayerIndex + count);
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

            if (Input.GetKeyDown(KeyCode.F))
            {
                SetPlayerToCell(player, cells.Count - 1);
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
