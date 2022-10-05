using UnityEngine;

namespace UISystem
{
    public class ApplySafeAreaRectTransform : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        private void Start()
        {
            if (_rectTransform == null)
                _rectTransform = this.GetComponent<RectTransform>();

            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                _rectTransform.rect.width - SafeAreaManager.SafeRect.x * 2);
        }
    }
}