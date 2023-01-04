using UnityEngine;

namespace BoardGame
{
    public class Board : MonoBehaviour
    {
        [SerializeField]
        private Player player;

        public void MovePlayerOn(int cellCount)
        {
            player.transform.position += Vector3.forward * cellCount;
        }
    }
}
