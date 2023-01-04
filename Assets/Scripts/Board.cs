using UnityEngine;

namespace BoardGame
{
    public class Board : MonoBehaviour
    {
        private Player player;

        public void Initialize(Player player)
        {
            this.player = player;
        }

        public void MovePlayerOn(int cellCount)
        {
            player.transform.position += Vector3.forward * cellCount;
        }
    }
}
