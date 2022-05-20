using UnityEngine;
using System;
namespace UISystem
{
    public class UIPopupWindow : UIBaseWindow
    {

        protected Action<PopupCallbackParameters> _callback;

        #region Overrides

        public override void CloseWindow()
        {
            _callback = null;
            base.CloseWindow();
        }

        #endregion

        #region Interface
        public virtual void OpenPopup(Action<PopupCallbackParameters> callback, bool queue = false)
        {
            _callback = callback;
            if (!queue)
                OpenWindow();
            else
            {
                OpenWindowInQueue();
            }
        }

        [ContextMenu("OpenPopup")]
        public virtual void OpenPopup()
        {
            OpenPopup(x => { });
        }

        public virtual void ClosePopupAccepted()
        {
            CloseWindow(new PopupCallbackParameters() { CallbackType = PopupCallbackType.Accepted });
        }
        public virtual void ClosePopupDenied()
        {
            CloseWindow(new PopupCallbackParameters() { CallbackType = PopupCallbackType.Denied });
        }

        #endregion

        #region Overrides

        public override void TryCloseWindowByEscapeButton()
        {
            if (!_preventEscape)
            {
                if (IsWindowCompletelyOpen())//User can close popup only when it CompletelyOpen. Prevents repeated pressing.
                {
                    CloseWindow(new PopupCallbackParameters() { CallbackType = PopupCallbackType.Denied });
                }
            }
        }

        #endregion

        #region Utils

        protected void CloseWindow(PopupCallbackParameters popupCallbackParameters)
        {
            if (IsWindowVisible())//User can close popup when WindowState is OpenAnimation or Open. Prevents repeated pressing.
            {
                if (_callback != null)
                {
                    _callback(popupCallbackParameters);
                    _callback = null;
                }
                CloseWindow();
            }
        }

        public void AddListener(Action<PopupCallbackParameters> listener)
        {
            _callback += listener;
        }
        #endregion

    }
}