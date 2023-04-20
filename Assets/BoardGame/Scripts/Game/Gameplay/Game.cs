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
            UI.Initialize(OnDiceButtonClicked);
            board.Initialize(Finish);

            board.PlayerMovementStarted += OnPlayerMovementStarted;
            board.PlayerMovementEnded += OnPlayerMovementEnded;
        }

        private void Finish()
        {
            UI.ShowGameOverScreen(Restart);
        }

        private void Restart()
        {
            UI.Reset();
            board.RegenerateCells();
            board.SetPlayerToFirstCell();
        }

        public void OnDestroy()
        {
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
    }
}
