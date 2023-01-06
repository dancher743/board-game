using BoardGame.UI;
using UnityEngine;

namespace BoardGame
{
    public class Game : MonoBehaviour
    {
        private const string StatusText = "Result of roll is {0}!";

        [SerializeField]
        private GameUI UI;

        [SerializeField]
        private Board board;

        public void Start()
        {
            UI.Initialize(OnDiceButtonClicked, false);
            board.Initialize();
        }

        public void Clear()
        {
            UI.Clear();
        }

        private int RollDice()
        {
            return Random.Range(1, 7);
        }

        private void OnDiceButtonClicked()
        {
            var pips = RollDice();

            UI.SetStatus(string.Format(StatusText, pips));
            board.MovePlayerOnCell(pips);
        }
    }
}
