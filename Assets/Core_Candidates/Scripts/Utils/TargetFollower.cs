using UnityEngine;

namespace Core.Utils
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        private float smoothTime;

        private Vector3 currentVelocity = Vector3.zero;

        public void LateUpdate()
        {
            if (target == null)
            {
                Debug.LogError($"Couldn't follow the {nameof(target)} of {nameof(TargetFollower)} on {gameObject.name} game object because it's null.");
                return;
            }

            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, smoothTime);
        }
    }
}
