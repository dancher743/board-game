using System;
using UnityEngine;

namespace BoardGame.UI.Screens
{
    public class GameOverScreen : MonoBehaviour
    {
        public Action RestartButtonClicked;

        private void OnDestroy()
        {
            RestartButtonClicked = null;
        }

        public void OnRestartButtonClicked()
        {
            RestartButtonClicked?.Invoke();
        }
    }
}
