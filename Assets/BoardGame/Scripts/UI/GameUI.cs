using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoardGame.UI
{
    public class GameUI : MonoBehaviour
    {
        public UnityAction DiceButtonClicked;

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

        private void Awake()
        {
            SetStatusActive(false);
            diceButton.onClick.AddListener(() => DiceButtonClicked?.Invoke());
        }

        private void OnDestroy()
        {
            Clear();
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

        public void SetStatusActive(bool value)
        {
            statusText.gameObject.SetActive(value);
        }

        public void Clear()
        {
            statusText.text = string.Empty;
            diceButton.onClick.RemoveAllListeners();
        }

        public IEnumerator DisappearStatusTextRoutine()
        {
            yield return new WaitForSeconds(DisappearStatusTextTime);

            SetStatusActive(false);
        }
    }
}