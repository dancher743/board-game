using BoardGame.Utils.CellsGeneration;
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

        [Header("Cells")]
        [SerializeField]
        private Transform cellsParent;

        [SerializeField]
        private CellsGenerator cellsGenerator;

        [Header("Player")]
        [SerializeField]
        private Player player;

        [SerializeField]
        private float PlayerMovementInterval;

        private List<Cell> cells;
        private int currentCellWithPlayerIndex;
        private Coroutine movePlayerCoroutine;

        public void Initialize()
        {
            cells = cellsParent.childCount > 0 ? GetCells() : cellsGenerator.Generate();
            SetPlayerToFirstCell();
        }

        public void Regenerate()
        {
            cells = cellsGenerator.Generate();
            SetPlayerToFirstCell();
        }

        public void MovePlayerOnCells(int count)
        {
            if (movePlayerCoroutine != null)
            {
                return;
            }

            movePlayerCoroutine = StartCoroutine(MovePlayerRoutine(count));
            PlayerMovementStarted?.Invoke();
        }

        public void SetPlayerToFirstCell()
        {
            SetPlayerToCell(0);
        }

        private void SetPlayerToCell(int index)
        {
            if (player == null || currentCellWithPlayerIndex == index)
            {
                return;
            }

            var lastCellIndex = cells.Count - 1;

            if (index < 0 || index > lastCellIndex)
            {
                return;
            }

            currentCellWithPlayerIndex = index;

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

        private IEnumerator MovePlayerRoutine(int cellCount)
        {
            var delta = (int)Mathf.Sign(cellCount);

            for (int i = 0; i < Mathf.Abs(cellCount); i++)
            {
                SetPlayerToCell(currentCellWithPlayerIndex + delta);
                yield return new WaitForSeconds(PlayerMovementInterval);
            }

            movePlayerCoroutine = null;
            PlayerMovementEnded?.Invoke();
        }

#if UNITY_EDITOR
        #region DEBUG

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetPlayerToCell(currentCellWithPlayerIndex + 1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SetPlayerToCell(currentCellWithPlayerIndex - 1);
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
#endif
    }
}
