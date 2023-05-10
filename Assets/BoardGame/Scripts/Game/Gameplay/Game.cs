using BoardGame.UI;
using System;
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
            UI.Initialize();
            UI.DiceButtonClicked += OnDiceButtonClicked;

            board.Initialize();
            board.PlayerMovementStarted += OnPlayerMovementStarted;
            board.PlayerMovementEnded += OnPlayerMovementEnded;
            board.PlayerLastCellReached += OnPlayerLastCellReached;
        }

        private void Finish()
        {
            UI.ShowGameOverScreen(Restart);
        }

        private void Restart()
        {
            UI.Reset();
            board.Regenerate();
        }

        public void OnDestroy()
        {
            UI.DiceButtonClicked -= OnDiceButtonClicked;
            board.PlayerMovementStarted -= OnPlayerMovementStarted;
            board.PlayerMovementStarted -= OnPlayerMovementStarted;
            board.PlayerMovementEnded -= OnPlayerMovementEnded;
        }

        private int RollDice()
        {
            var exclusiveValue = 1;
            return UnityEngine.Random.Range(MinDiceRange, diceRange + exclusiveValue);
        }

        private void OnDiceButtonClicked()
        {
            var roll = RollDice();
            UI.SetStatus(string.Format(StatusText, roll));
            board.MovePlayerOnCells(roll);
        }

        private void OnPlayerMovementStarted()
        {
            UI.SetDiceButtonInteractable(false);
        }

        private void OnPlayerMovementEnded()
        {
            UI.SetDiceButtonInteractable(true);
        }

        private void OnPlayerLastCellReached()
        {
            Finish();
        }
    }
}
