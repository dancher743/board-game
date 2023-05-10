using Core;
using NaughtyAttributes;
using System.Linq;
using UnityEngine;

namespace BoardGame.Utils.CellsGeneration
{
    public class CellsGenerator : MonoBehaviour
    {
        [SerializeField]
        private CellElement[] elements;

        [SerializeField]
        private int elementsAmount;

        [Button]
        public void Generate()
        {
            if (elementsAmount == 0)
            {
                return;
            }

            transform.DestroyChildrens();

            var cellsAmount = elementsAmount;
            var isPreviousElementConnector = false;
            var nextCellPosition = Vector3.zero;

            while (cellsAmount > 0)
            {
                var boardElements = elements.Where(element => element.IsConnector == !isPreviousElementConnector);
                var boardElement = boardElements.ElementAt(Random.Range(0, boardElements.Count()));
                var boardElementAmount = Random.Range(boardElement.RandomAmount.x, boardElement.RandomAmount.y);
                isPreviousElementConnector = boardElement.IsConnector;
                cellsAmount -= boardElementAmount;

                if (cellsAmount < 0)
                {
                    boardElementAmount -= Mathf.Abs(cellsAmount);
                    cellsAmount = 0;
                }

                for (int i = 0; i < boardElementAmount; i++)
                {
                    var cell = Instantiate(boardElement.Prefab, transform);
                    cell.transform.position = nextCellPosition;
                    nextCellPosition = cell.transform.position + cell.transform.forward;
                }
            }
        }
    }
}
