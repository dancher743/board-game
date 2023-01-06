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

        public void Awake()
        {
            UI.SetStatusActive(false);
            UI.DiceButtonClicked += OnDiceButtonClicked;
            board.Initialize();
        }

        public void Clear()
        {
            UI.DiceButtonClicked -= OnDiceButtonClicked;
        }

        public int RollDice()
        {
            return Random.Range(1, 7);
        }

        private void OnDiceButtonClicked()
        {
            var pips = RollDice();

            UI.SetStatusActive(true);
            UI.SetStatus(string.Format(StatusText, pips));
            board.MovePlayerOnCell(pips);
        }
    }
}
