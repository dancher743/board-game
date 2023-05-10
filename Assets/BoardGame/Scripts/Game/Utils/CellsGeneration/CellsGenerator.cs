using Core;
using NaughtyAttributes;
using System.Collections.Generic;
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
        public List<Cell> Generate()
        {
            var cells = new List<Cell>(elementsAmount);

            if (elementsAmount == 0)
            {
                return cells;
            }

            transform.DestroyChildrens();

            var cellsLeftAmount = elementsAmount;
            var isPreviousElementConnector = false;
            var nextCellPosition = Vector3.zero;

            while (cellsLeftAmount > 0)
            {
                var cellElements = elements.Where(element => element.IsConnector == !isPreviousElementConnector);
                var cellElement = cellElements.ElementAt(Random.Range(0, cellElements.Count()));
                var cellAmount = Random.Range(cellElement.RandomAmount.x, cellElement.RandomAmount.y);
                isPreviousElementConnector = cellElement.IsConnector;

                if (cellsLeftAmount < cellAmount)
                {
                    cellAmount = cellsLeftAmount;
                    cellsLeftAmount = 0;
                }
                else
                {
                    cellsLeftAmount -= cellAmount;
                }

                for (int i = 0; i < cellAmount; i++)
                {
                    var cell = Instantiate(cellElement.Prefab, transform);
                    cell.Initialize(nextCellPosition, transform.childCount);
                    cells.Add(cell);
                    nextCellPosition += cell.transform.forward;
                }
            }

            return cells;
        }
    }
}
