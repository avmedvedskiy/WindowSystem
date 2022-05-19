using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class CanvasScaleComponent : MonoBehaviour
    {
        private const float DEFAULT_SCREEN_RATIO = 16f / 9f;
        private const float FACTOR = 4f / 3f;
        [SerializeField] protected CanvasScaler canvasScaler;

        private void Awake()
        {
            SetScreenScale();
        }
        
        [ContextMenu("SetScreenScale")]
        public void SetScreenScale()
        {
            int width = Screen.width;
            int height = Screen.height;
            float scale = (float)Mathf.Max(width, height) / Mathf.Min(width, height);
            if (scale > DEFAULT_SCREEN_RATIO)
            {
                canvasScaler.matchWidthOrHeight = (scale - DEFAULT_SCREEN_RATIO) * FACTOR;
            }
        }
    }
}