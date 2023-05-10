using UnityEngine;

namespace BoardGame
{
    public class Cell : MonoBehaviour
    {
        [SerializeField]
        private TextMesh numberText;

        public void Initialize(Vector3 position, int number)
        {
            transform.position = position;
            numberText.text = number.ToString();
            name = $"Cell{number}";
        }
    }
}
