using UnityEngine;

namespace BoardGame
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private Board board;

        [SerializeField]
        private Player player;

        private void Awake()
        {
            board.Initialize(player);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                board.MovePlayerOn(1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                board.MovePlayerOn(-1);
            }
        }
    }
}