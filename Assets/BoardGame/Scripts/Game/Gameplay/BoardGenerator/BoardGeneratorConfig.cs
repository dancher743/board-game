using UnityEngine;
using static BoardGame.Consts;

namespace BoardGame.Configs.BoardGenerator
{
    [CreateAssetMenu(menuName = AssetMenu.Root + nameof(BoardGeneratorConfig), fileName = nameof(BoardGeneratorConfig))]
    public class BoardGeneratorConfig : ScriptableObject
    {
        [field: SerializeField]
        public BoardPart[] BoardParts { get; private set; }
    }
}
