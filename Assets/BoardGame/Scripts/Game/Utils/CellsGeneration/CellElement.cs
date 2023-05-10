using System;
using UnityEngine;

namespace BoardGame.Utils.CellsGeneration
{
    [Serializable]
    public struct CellElement
    {
        [field: SerializeField]
        public Cell Prefab { get; private set; }

        [field: SerializeField]
        public Vector2Int RandomAmount { get; private set; }

        [field: SerializeField]
        public bool IsConnector { get; private set; }
    }
}
