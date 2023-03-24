using UnityEngine;

namespace Core
{
    public static class Extensions
    {
        #region Unity

        public static void DestroyChildrens(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                if (Application.isEditor)
                {
                    Object.DestroyImmediate(transform.GetChild(i).gameObject);
                }
                else
                {
                    Object.Destroy(transform.GetChild(i).gameObject);
                }
            }
        }

        #endregion
    }
}
