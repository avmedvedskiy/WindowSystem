using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UISystem
{
    public class IgnoreSafeAreaRectTransform : MonoBehaviour
    {
        public RectTransform rectTransform;

        private void Start()
        {
            if (rectTransform == null)
                rectTransform = this.GetComponent<RectTransform>();

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.rect.width + CanvasSafeAreaManager.SafeRect.x * 2);

        }
    }
}
