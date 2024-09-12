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

            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
            
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
        }

        private void OnValidate()
        {
            _rectTransform ??= GetComponent<RectTransform>();
        }
    }
}