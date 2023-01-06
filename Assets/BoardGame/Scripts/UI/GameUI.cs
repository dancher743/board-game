using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoardGame.UI
{
    public class GameUI : MonoBehaviour
    {
        public UnityAction DiceButtonClicked;

        [SerializeField]
        private Button diceButton;

        [SerializeField]
        private TextMeshProUGUI statusText;

        private void Awake()
        {
            diceButton.onClick.AddListener(() => DiceButtonClicked?.Invoke());
        }

        private void OnDestroy()
        {
            Clear();
        }

        public void SetStatus(string text)
        {
            statusText.text = text;
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
    }
}