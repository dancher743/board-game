using BoardGame.Configs;
using NaughtyAttributes;
using System.Linq;
using UnityEngine;

namespace BoardGame.Utils
{
    public class BoardGenerator : MonoBehaviour
    {
        private const string GeneratedCellsParentName = "GeneratedCells";

        [SerializeField]
        private BoardElement[] elements;

        [SerializeField]
        private int elementsCount;

        [Button]
        public void Generate()
        {
            var totalCount = elementsCount;

            if (totalCount == 0)
            {
                return;
            }

            var generatedCellsTransform = transform.Find(GeneratedCellsParentName);

            if (generatedCellsTransform != null)
            {
                DestroyImmediate(generatedCellsTransform.gameObject);
            }

            var generatedCellsParent = new GameObject(GeneratedCellsParentName);
            generatedCellsParent.transform.SetParent(transform);

            var wasPreviousElementConnector = false;
            var i2 = 0;

            while (totalCount > 0)
            {
                BoardElement boardElement;

                if (wasPreviousElementConnector)
                {
                    var boardElements = elements.Where(part => !part.IsConnector);
                    var boardCount = boardElements.Count();
                    boardElement = boardElements.ElementAt(Random.Range(0, boardCount));
                }
                else
                {
                    boardElement = elements[Random.Range(0, elements.Length)];
                }

                var count = Random.Range(boardElement.RandomCount.x, boardElement.RandomCount.y);

                for (int i = 0; i < count; i++)
                {
                    var element = Instantiate(boardElement.Prefab, generatedCellsParent.transform);
                    element.transform.position += element.transform.forward.normalized * i2;
                    i2++;
                }

                totalCount -= count;
                wasPreviousElementConnector = boardElement.IsConnector;
            }
        }
    }
}