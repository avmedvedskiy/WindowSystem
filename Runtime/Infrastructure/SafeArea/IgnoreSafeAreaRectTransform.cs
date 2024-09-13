using System;
using UnityEngine;

namespace UISystem
{
    public class IgnoreSafeAreaRectTransform : MonoBehaviour
    {
        [SerializeField] public RectTransform _rectTransform;

        private void OnEnable()
        {
            _rectTransform ??= this.GetComponent<RectTransform>();
            _rectTransform.position = SafeAreaManager.ScreenRect.position;
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SafeAreaManager.ScreenRect.sizeDelta.y);
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SafeAreaManager.ScreenRect.sizeDelta.x);
        }

        private void OnValidate()
        {
            _rectTransform ??= GetComponent<RectTransform>();
        }
    }
}