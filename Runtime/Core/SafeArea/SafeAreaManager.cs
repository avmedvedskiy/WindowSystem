using UnityEngine;

namespace UISystem
{
    public class SafeAreaManager : MonoBehaviour
    {
        public static Rect SafeRect { get; set; }

        [SerializeField] private RectTransform _safeAreaRoot;
        [SerializeField] private RectTransform _leftSafeAreaMask;
        [SerializeField] private RectTransform _rightSafeAreaMask;

        [SerializeField] private bool _useOutSafeRect = true;

        [SerializeField] private bool _applySafeArea = true;

        public int _debugOffset = 100;

        public void Start()
        {
            var screenRect = new Rect(0, 0, Screen.width, Screen.height);
            var needRect = Screen.safeArea;

#if UNITY_IOS
            //on ios strange x2 size on safe area
            needRect.width = Screen.width - needRect.x;
            needRect.x /= 2;

#else
            if (needRect.x == 0) //for right offset
                needRect.x = screenRect.width - needRect.width;

            needRect.width -= needRect.x; //for symmetric offset
#endif


#if UNITY_EDITOR
            needRect.x = _debugOffset;
            needRect.width = Screen.width - _debugOffset * 2;
#endif
            SafeRect = needRect;

            if (needRect.width != Screen.width)
            {
                ApplySafeArea(needRect);
            }
        }

        private void ApplySafeArea(Rect area)
        {
            if (!_applySafeArea)
                return;

            var anchorMin = area.position;
            var anchorMax = area.position + area.size;

            anchorMin.x /= Screen.width;
            anchorMax.x /= Screen.width;
            anchorMin.y = 0;
            anchorMax.y = 1;

            _safeAreaRoot.anchorMin = anchorMin;
            _safeAreaRoot.anchorMax = anchorMax;

            if (_useOutSafeRect)
            {
                _leftSafeAreaMask.anchorMin = Vector2.zero;
                _leftSafeAreaMask.anchorMax = new Vector2(anchorMin.x, 1);

                _rightSafeAreaMask.anchorMin = new Vector2(anchorMax.x, 0);
                _rightSafeAreaMask.anchorMax = Vector2.one;

                _leftSafeAreaMask.gameObject.SetActive(area.width != Screen.width);
                _rightSafeAreaMask.gameObject.SetActive(area.width != Screen.width);
            }
        }
    }
}