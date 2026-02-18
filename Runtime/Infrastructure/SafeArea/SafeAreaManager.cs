using System;
using UnityEngine;

namespace UISystem
{
    public class SafeAreaManager : MonoBehaviour
    {
        public static Rect SafeRect { get; set; }
        public static RectTransform ScreenRect { get; set; }

        [SerializeField] private RectTransform _canvas;
        [SerializeField] private RectTransform _safeAreaRoot;
        [SerializeField] private bool _x = true;
        [SerializeField] private bool _y = true;


        public void Start()
        {
            ApplySafeArea(CalculateSafeArea());
        }

        private Rect CalculateSafeArea()
        {
            var needRect = Screen.safeArea;

#if UNITY_IOS
            //on ios strange x2 size on safe area
            needRect.x /= 2;
            needRect.width = Screen.safeArea.width + needRect.x * 2f ;
            
            needRect.y /= 2;
            needRect.height = Screen.safeArea.height + needRect.y * 2f;
#endif
            return needRect;
        }

        private Rect _lastSafeArea;

        private void Update()
        {
            if (_lastSafeArea != Screen.safeArea)
            {
                ApplySafeArea(CalculateSafeArea());
            }
        }

        private void ApplySafeArea(Rect area)
        {
            if (_x == false && _y == false)
                return;

            var anchorMin = area.position;
            var anchorMax = area.position + area.size;

            anchorMin.x /= Screen.width;
            anchorMax.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.y /= Screen.height;

            _safeAreaRoot.anchorMin = anchorMin;
            _safeAreaRoot.anchorMax = anchorMax;
            SafeRect = area;
            ScreenRect = _canvas;
            _lastSafeArea = Screen.safeArea;
        }
    }
}