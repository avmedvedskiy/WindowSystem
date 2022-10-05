using UnityEngine;
using System;

namespace UISystem
{
    public class UIPopupWindow : UIBaseWindow
    {
        protected Action<PopupCallbackParameters> callback;


        public override void CloseWindow()
        {
            callback = null;
            base.CloseWindow();
        }

        public virtual void OpenPopup(Action<PopupCallbackParameters> action, bool queue = false)
        {
            callback = action;
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

        public override void TryCloseWindowByEscapeButton()
        {
            if (!PreventEscape)
            {
                if (IsWindowCompletelyOpen()) //User can close popup only when it CompletelyOpen. Prevents repeated pressing.
                {
                    CloseWindow(new PopupCallbackParameters() { CallbackType = PopupCallbackType.Denied });
                }
            }
        }

        protected void CloseWindow(PopupCallbackParameters popupCallbackParameters)
        {
            if (IsWindowVisible()) //User can close popup when WindowState is OpenAnimation or Open. Prevents repeated pressing.
            {
                if (callback != null)
                {
                    callback(popupCallbackParameters);
                    callback = null;
                }

                CloseWindow();
            }
        }

        public void AddListener(Action<PopupCallbackParameters> listener)
        {
            callback += listener;
        }
    }
}