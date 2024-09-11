using System;
using UnityEngine;

namespace UISystem
{
    public class IgnoreSafeAreaRectTransform : MonoBehaviour
    {
        [SerializeField] public RectTransform _rectTransform;

        private void Start()
        {
            if (_rectTransform == null)
                _rectTransform = this.GetComponent<RectTransform>();

            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                _rectTransform.rect.width + SafeAreaManager.SafeRect.x * 2);
            
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                _rectTransform.rect.height + SafeAreaManager.SafeRect.y * 2);
        }

        private void OnValidate()
        {
            _rectTransform ??= GetComponent<RectTransform>();
        }
    }
}