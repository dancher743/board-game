using NaughtyAttributes;
using UnityEngine;

namespace Core.Utils
{
    public class IsometricOrienteer : MonoBehaviour
    {
        private readonly Vector3 isometricVector = new Vector3(30f, 40f, 0f);

        [Button]
        public void Orient()
        {
            transform.rotation = Quaternion.Euler(isometricVector);

            Debug.Log($"{nameof(IsometricOrienteer)}: game object \"{name}\" has been oriented to isometric view.");
        }
    }
}
