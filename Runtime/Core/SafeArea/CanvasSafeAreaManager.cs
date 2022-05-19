using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UISystem
{
    public class CanvasSafeAreaManager : MonoBehaviour
    {
        public static Rect SafeRect
        {
            get; set;
        }


        [SerializeField]
        protected RectTransform safeAreaRoot;
        [SerializeField]
        protected RectTransform leftSafeAreaMask;
        [SerializeField]
        protected RectTransform rightSafeAreaMask;

        [SerializeField]
        protected bool useOutSafeRect = true;

        [SerializeField]
        protected bool applySafeArea = true;

        public int debugOffset = 100;

        public void Start()
        {
            var screenRect = new Rect(0, 0, Screen.width, Screen.height);
            var needRect = Screen.safeArea;

#if UNITY_IOS
        //on ios strange x2 size on safe area
        needRect.width = Screen.width - needRect.x;
        needRect.x /= 2;

#else
            if (needRect.x == 0)//for right offset
                needRect.x = screenRect.width - needRect.width;

            needRect.width -= needRect.x; //for symmetric offset
#endif


#if UNITY_EDITOR
            needRect.x = debugOffset;
            needRect.width = Screen.width - debugOffset * 2;
#endif
            SafeRect = needRect;

            if (needRect.width != Screen.width)
            {
                ApplySafeArea(needRect);
            }

        }

        private void ApplySafeArea(Rect area)
        {
            if (!applySafeArea)
                return;

            var anchorMin = area.position;
            var anchorMax = area.position + area.size;

            anchorMin.x /= Screen.width;
            anchorMax.x /= Screen.width;
            anchorMin.y = 0;
            anchorMax.y = 1;

            safeAreaRoot.anchorMin = anchorMin;
            safeAreaRoot.anchorMax = anchorMax;

            if (useOutSafeRect)
            {
                leftSafeAreaMask.anchorMin = Vector2.zero;
                leftSafeAreaMask.anchorMax = new Vector2(anchorMin.x, 1);

                rightSafeAreaMask.anchorMin = new Vector2(anchorMax.x, 0);
                rightSafeAreaMask.anchorMax = Vector2.one;

                leftSafeAreaMask.gameObject.SetActive(area.width != Screen.width);
                rightSafeAreaMask.gameObject.SetActive(area.width != Screen.width);
            }
        }
    }
}


