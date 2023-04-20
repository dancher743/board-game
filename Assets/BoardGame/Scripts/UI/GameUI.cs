using BoardGame.UI.Screens;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoardGame.UI
{
    public class GameUI : MonoBehaviour
    {
        [Header("Dice button")]
        [SerializeField]
        private Button diceButton;

        [Header("Status text")]
        [SerializeField]
        private TextMeshProUGUI statusText;

        [Tooltip("Time in seconds")]
        [SerializeField]
        private float DisappearStatusTextTime = 1f;

        [Header("Screens")]
        [SerializeField]
        private GameOverScreen gameOverScreenPrefab;

        private GameOverScreen gameOverScreen;
        private Coroutine disappearStatusTextCoroutine;

        public void Initialize(UnityAction diceButtonClicked)
        {
            diceButton.onClick.AddListener(diceButtonClicked);
            SetStatusActive(false);
        }

        public void Reset()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            StopAllCoroutines();
            SetStatusActive(false);
            RemoveGameOverScreen();
        }

        public void SetStatus(string text)
        {
            if (disappearStatusTextCoroutine != null)
            {
                StopCoroutine(disappearStatusTextCoroutine);
            }

            statusText.text = text;
            SetStatusActive(true);

            disappearStatusTextCoroutine = StartCoroutine(DisappearStatusTextRoutine());
        }

        public void ShowGameOverScreen(Action restartButtonClicked)
        {
            gameOverScreen = Instantiate(gameOverScreenPrefab, transform);
            gameOverScreen.RestartButtonClicked += restartButtonClicked;
        }

        private void RemoveGameOverScreen()
        {
            if (gameOverScreen == null)
            {
                return;
            }

            Destroy(gameOverScreen.gameObject);
            gameOverScreen = null;
        }

        private void SetStatusActive(bool value)
        {
            if (statusText.gameObject.activeSelf == value)
            {
                return;
            }

            statusText.gameObject.SetActive(value);
        }

        public void SetDiceButtonInteractable(bool value)
        {
            diceButton.interactable = value;
        }

        private IEnumerator DisappearStatusTextRoutine()
        {
            yield return new WaitForSeconds(DisappearStatusTextTime);

            SetStatusActive(false);
        }
    }
}