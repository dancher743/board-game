using Core.Utils;
using UnityEditor;
using UnityEngine;

namespace Core.Editors.Utils
{
    [CustomEditor(typeof(IsometricOrienteer))]
    public class IsometricOrienteerEditor : Editor
    {
        private IsometricOrienteer isometricOrienteer;

        private void Awake()
        {
            isometricOrienteer = target as IsometricOrienteer;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (isometricOrienteer == null)
            {
                return;
            }

            if (GUILayout.Button(nameof(isometricOrienteer.Orient)))
            {
                isometricOrienteer.Orient();
            }
        }
    }
}
