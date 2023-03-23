using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
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

        private Dictionary<Player, Cell> playerToCell;

        public void Initialize()
        {
            playerToCell = new Dictionary<Player, Cell>();

            cells = GetCells();

            SetPlayerToCell(player, cells.FirstOrDefault());
        }

        public void MovePlayerOnCells(int count)
        {
            MovePlayerOnCells(player, count);
        }

        private void MovePlayerOnCells(Player player, int count)
        {
            if (playerToCell == null)
            {
                return;
            }

            var cell = playerToCell.GetValueOrDefault(player);

            if (cell == null)
            {
                return;
            }

            var nextCellIndex = cells.IndexOf(cell) + count;
            var allCellsIndex = cells.Count - 1;

            if (nextCellIndex < 0 || nextCellIndex > allCellsIndex)
            {
                return;
            }

            var nextCell = cells[nextCellIndex];

            playerToCell[player] = nextCell;

            // A View code.
            player.transform.position = nextCell.transform.position;
            player.transform.rotation = nextCell.transform.rotation;
        }

        private void SetPlayerToCell(Player player, Cell cell)
        {
            if (cell == null || playerToCell == null)
            {
                return;
            }

            if (playerToCell.ContainsKey(player))
            {
                playerToCell[player] = cell;
            }
            else
            {
                playerToCell.Add(player, cell);
            }

            // A View code.
            player.transform.position = cell.transform.position;
            player.transform.rotation = cell.transform.rotation;
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
                SetPlayerToCell(player, cells[0]);
            }
        }

        #endregion


#if UNITY_EDITOR
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

            Debug.Log($"Cells has been successfully renamed.");
        }
#endif
    }
}
