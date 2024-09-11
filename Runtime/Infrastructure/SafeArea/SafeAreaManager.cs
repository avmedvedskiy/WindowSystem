using System;
using UnityEngine;

namespace UISystem
{
    public class SafeAreaManager : MonoBehaviour
    {
        public static Rect SafeRect { get; set; }

        [SerializeField] private RectTransform _safeAreaRoot;
        [SerializeField] private bool _x = true;
        [SerializeField] private bool _y = true;


        public void Start()
        {
            CalculateSafeArea();
        }

        private void CalculateSafeArea()
        {
            var screenRect = new Rect(0, 0, Screen.width, Screen.height);
            var needRect = Screen.safeArea;

#if UNITY_IOS
            //on ios strange x2 size on safe area
            safeArea.width = Screen.width - safeArea.x;
            safeArea.x /= 2;
#endif
            SafeRect = needRect;

            if (needRect.width != screenRect.width || needRect.height != screenRect.height)
            {
                ApplySafeArea(needRect);
            }
        }

        private Rect _lastSafeArea;

        private void Update()
        {
            if (_lastSafeArea != Screen.safeArea)
            {
                ApplySafeArea(Screen.safeArea);
                _lastSafeArea = Screen.safeArea;
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
        }
    }
}