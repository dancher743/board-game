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

        public void MovePlayerOnCell(int cellCount)
        {
            MovePlayerOnCell(player, cellCount);
        }

        private void MovePlayerOnCell(Player player, int cellCount)
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

            var nextCellIndex = cells.IndexOf(cell) + cellCount;
            var cellsIndex = cells.Count - 1;

            if (nextCellIndex < 0 || nextCellIndex > cellsIndex)
            {
                return;
            }

            var nextCell = cells[nextCellIndex];

            playerToCell[player] = nextCell;

            // A View code.
            player.transform.position = nextCell.transform.position;
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
                MovePlayerOnCell(player, 1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                MovePlayerOnCell(player, -1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SetPlayerToCell(player, cells[0]);
            }
        }

        #endregion
    }
}
