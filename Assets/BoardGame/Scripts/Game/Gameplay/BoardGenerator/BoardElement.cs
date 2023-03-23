using System;
using UnityEngine;

namespace BoardGame.Configs
{
    [Serializable]
    public struct BoardElement
    {
        [field: SerializeField]
        public Cell Prefab { get; private set; }

        [field: SerializeField]
        public Vector2Int RandomCount { get; private set; }

        [field: SerializeField]
        public bool IsConnector { get; private set; }
    }
}
