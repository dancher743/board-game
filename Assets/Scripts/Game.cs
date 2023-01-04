using UnityEngine;

namespace BoardGame
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private Board board;

        public void Awake()
        {
            board.Initialize();
        }
    }
}
