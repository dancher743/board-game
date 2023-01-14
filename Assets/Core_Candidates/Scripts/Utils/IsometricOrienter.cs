using UnityEngine;

namespace Core.Utils
{
    public class IsometricOrienter : MonoBehaviour
    {
        private readonly Vector3 isometricVector = new Vector3(30f, 40f, 0f);

        private void Reset()
        {
            Orient();
        }

        private void Awake()
        {
            Orient();
        }

        private void Orient()
        {
            transform.rotation = Quaternion.Euler(isometricVector);
            
            Debug.Log($"{nameof(IsometricOrienter)}: game object \"{name}\" has been oriented to isometric view.");
        }
    }
}
