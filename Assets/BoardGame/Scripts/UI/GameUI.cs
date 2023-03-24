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

        private Coroutine disappearStatusTextCoroutine;

        public void Initialize(UnityAction diceButtonClicked)
        {
            diceButton.onClick.AddListener(diceButtonClicked);
            SetStatusActive(false);
        }

        public void Clear()
        {
            StopAllCoroutines();
            statusText.text = string.Empty;
            diceButton.onClick.RemoveAllListeners();
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

        private void SetStatusActive(bool value)
        {
            if (statusText.gameObject.activeSelf == value)
            {
                return;
            }

            statusText.gameObject.SetActive(value);
        }

        private IEnumerator DisappearStatusTextRoutine()
        {
            yield return new WaitForSeconds(DisappearStatusTextTime);

            SetStatusActive(false);
        }
    }
}