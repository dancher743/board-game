using Core.Utils;
using UnityEditor;
using UnityEngine;

namespace Core.Editors.Utils
{
    [CustomEditor(typeof(IsometricOrienter))]
    public class IsometricOrienterEditor : Editor
    {
        private IsometricOrienter isometricOrienter;

        private void Awake()
        {
            isometricOrienter = target as IsometricOrienter;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (isometricOrienter == null)
            {
                return;
            }

            if (GUILayout.Button(nameof(isometricOrienter.Orient)))
            {
                isometricOrienter.Orient();
            }
        }
    }
}
