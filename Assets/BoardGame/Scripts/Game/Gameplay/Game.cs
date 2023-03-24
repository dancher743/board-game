using BoardGame.UI;
using UnityEngine;

namespace BoardGame
{
    public class Game : MonoBehaviour
    {
        private const int MinDiceRange = 1;
        private const int MaxDiceRange = 12;
        private const string StatusText = "Result of roll is {0}!";

        [SerializeField]
        private GameUI UI;

        [SerializeField]
        private Board board;

        [Range(MinDiceRange, MaxDiceRange)]
        [SerializeField]
        private int diceRange = MinDiceRange;

        private void Start()
        {
            UI.Initialize(OnDiceButtonClicked);
            board.Initialize(HasPlayerReachedLastCell);
        }

        private void OnDestroy()
        {
            UI.Clear();
            board.Clear();
        }

        private int RollDice()
        {
            var exclusiveValue = 1;
            return Random.Range(MinDiceRange, diceRange + exclusiveValue);
        }

        private void OnDiceButtonClicked()
        {
            var pips = RollDice();

            UI.SetStatus(string.Format(StatusText, pips));
            board.MovePlayerOnCells(pips);
        }

        private void HasPlayerReachedLastCell()
        {
            UI.ShowGameOverScreen();
        }
    }
}
