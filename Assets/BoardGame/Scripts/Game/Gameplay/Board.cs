using BoardGame.Utils;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class Board : MonoBehaviour
    {
        public event Action PlayerLastCellReached;
        public event Action PlayerMovementStarted;
        public event Action PlayerMovementEnded;

        [SerializeField]
        private Transform cellsParent;

        [SerializeField]
        private CellsGenerator cellsGenerator;

        [SerializeField]
        private Player player;

        [SerializeField]
        private float PlayerMovementInterval;

        private List<Cell> cells;
        private int cellWithPlayerIndex;
        private Coroutine movePlayerCoroutine;

        public void Initialize()
        {
            cells = GetCells();
            SetPlayerToCell(0);
        }

        public void RegenerateCells()
        {
            cellsGenerator.Generate();
            NumerateCells();
            cells = GetCells();
        }

        public void MovePlayerOnCells(int count)
        {
            if (movePlayerCoroutine != null)
            {
                return;
            }

            movePlayerCoroutine = StartCoroutine(MovePlayerRoutine(count));
            PlayerMovementStarted?.Invoke();

            IEnumerator MovePlayerRoutine(int cellCount)
            {
                var deltaIndex = 1 * (int)Mathf.Sign(cellCount);
                var count = Mathf.Abs(cellCount);

                for (int i = 0; i < count; i++)
                {
                    SetPlayerToCell(cellWithPlayerIndex + deltaIndex);
                    yield return new WaitForSeconds(PlayerMovementInterval);
                }

                movePlayerCoroutine = null;
                PlayerMovementEnded?.Invoke();
            }
        }

        public void SetPlayerToFirstCell()
        {
            SetPlayerToCell(0);
        }

        private void SetPlayerToCell(int index)
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
                PlayerLastCellReached?.Invoke();
            }
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
                SetPlayerToCell(cellWithPlayerIndex + 1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SetPlayerToCell(cellWithPlayerIndex - 1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SetPlayerToFirstCell();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                SetPlayerToCell(cells.Count - 1);
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
