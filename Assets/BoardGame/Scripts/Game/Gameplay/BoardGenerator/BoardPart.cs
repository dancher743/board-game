using System;
using UnityEngine;

namespace BoardGame.Configs.BoardGenerator
{
    [Serializable]
    public struct BoardPart
    {
        [field: SerializeField]
        public Cell CellPrefab { get; private set; }

        [field: SerializeField]
        public int CellsCount { get; private set; }

        [field: SerializeField]
        public bool IsBridge { get; private set; }
    }
}
