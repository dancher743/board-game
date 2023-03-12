using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace BoardGame.Editors
{
    [CustomEditor(typeof(Board))]
    public class BoardEditor : Editor
    {
        private Board board;

        private void Awake()
        {
            board = target as Board;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(5f);

            if (GUILayout.Button("Numerate cells"))
            {
                NumerateCells();
            }
        }

        private void NumerateCells()
        {
            int number = 1;

            var cells = board.transform.Find("Cells");

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

            EditorUtility.SetDirty(board);

            Debug.Log($"Cells has been successfully renamed.");
        }
    }
}
