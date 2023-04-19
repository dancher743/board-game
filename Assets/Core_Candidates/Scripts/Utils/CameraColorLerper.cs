using System.Collections;
using System.Linq;
using UnityEngine;

namespace Core.Utils
{
    public class CameraColorLerper : MonoBehaviour
    {
        [SerializeField]
        private Camera targetCamera;

        [SerializeField]
        private Color[] colors;

        [SerializeField]
        private float lerpDelta;

        private Color colorA;
        private Color colorB;
        private float lerpProgress;

        private void Awake()
        {
            if (colors.Length == 0)
            {
                return;
            }

            colorA = GetColorRandomly();
            colorB = GetColorRandomly(colorA);
        }

        private IEnumerator Start()
        {
            if (colors.Length == 0)
            {
                yield break;
            }

            while (true)
            {
                lerpProgress += lerpDelta;

                if (lerpProgress >= 1f)
                {
                    lerpProgress = 0f;
                    colorA = colorB;
                    colorB = GetColorRandomly(colorA);
                }

                targetCamera.backgroundColor = Color.Lerp(colorA, colorB, lerpProgress);

                yield return null;
            }
        }

        private Color GetColorRandomly(Color excludeColor = default)
        {
            var colors = this.colors.Where(color => color != excludeColor);
            return colors.ElementAtOrDefault(Random.Range(0, colors.Count()));
        }
    }
}
