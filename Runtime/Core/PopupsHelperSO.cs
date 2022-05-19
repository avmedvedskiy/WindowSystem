
using UnityEngine;
using UISystem;

namespace UISystem
{
    /// <summary>
    /// Not for often use.
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/HelperSo/PopupsHelperSO")]
    internal class PopupsHelperSO : ScriptableObject
    {
        public void OpenWindow(string name)
        {
            var target = UIMainController.Instance.GetWindow<UIBaseWindow>(name);
            if(target)
                target.OpenWindow();
        }
        public void OpenWindowInQueue(string name)
        {
            var target = UIMainController.Instance.GetWindow<UIBaseWindow>(name);
            if (target)
                target.OpenWindowInQueue();
        }
        public void CloseWindow(string name)
        {
            var target = UIMainController.Instance.GetWindow<UIBaseWindow>(name);
            if (target)
                target.CloseWindow();
        }
        public void TryCloseWindowByEscapeButton(string name)
        {
            var target = UIMainController.Instance.GetWindow<UIBaseWindow>(name);
            if (target)
                target.TryCloseWindowByEscapeButton();
        }
        public void ClosePopupAccepted(string name)
        {
            var target = UIMainController.Instance.GetWindow<UIPopupWindow>(name);
            if (target)
                target.ClosePopupAccepted();
        }
        public void ClosePopupDenied(string name)
        {
            var target = UIMainController.Instance.GetWindow<UIPopupWindow>(name);
            if (target)
                target.ClosePopupDenied();
        }

        public void HideWindowImmediately(string name)
        {
            var target = UIMainController.Instance.GetWindow<UIBaseWindow>(name);
            if (target)
                target.HideWindowImmediately();
        }
    }
}
