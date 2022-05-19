using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace UISystem
{
    public class UIWindowAnimationController : MonoBehaviour
    {

        private List<UIBaseWindow> _animatedWindows = new List<UIBaseWindow>(); //TODO: clear on scene switch
        private List<UIBaseWindow> _stoppedAnimations = new List<UIBaseWindow>();

        #region Update

        void Update()
        {
            for (int i = 0; i < _animatedWindows.Count; i++)
            {
                AnimateWindow(_animatedWindows[i]);
            }

            for (int i = 0; i < _stoppedAnimations.Count; i++)
            {
                _animatedWindows.Remove(_stoppedAnimations[i]);
            }

            _stoppedAnimations.Clear();
        }

        #endregion

        #region Interface

        public void PlayOpenAnimation(UIBaseWindow targetWindow)
        {
            targetWindow.gameObject.SetActive(true);
            if (targetWindow.windowAnimation)
            {
                if (targetWindow.CurrentWindowState == UIBaseWindow.WindowState.Closed)
                {
                    targetWindow.SwitchState(UIBaseWindow.WindowState.OpenAnimation);
                    targetWindow.windowAnimation.PlayOpenAnimation();
                    _animatedWindows.Add(targetWindow);
                }
                else if (targetWindow.CurrentWindowState == UIBaseWindow.WindowState.CloseAnimation)
                {
                    targetWindow.SwitchState(UIBaseWindow.WindowState.OpenAnimation);
                    targetWindow.windowAnimation.StopAnimation();
                    targetWindow.windowAnimation.PlayOpenAnimation();
                }
            }
            else
            {
                targetWindow.SwitchState(UIBaseWindow.WindowState.Open);
            }
        }

        public void PlayCloseAnimation(UIBaseWindow targetWindow)
        {
            if (targetWindow == null)
                return;

            if (targetWindow.windowAnimation != null)
            {
                if (targetWindow.CurrentWindowState == UIBaseWindow.WindowState.Open)
                {
                    targetWindow.SwitchState(UIBaseWindow.WindowState.CloseAnimation);
                    targetWindow.windowAnimation.PlayCloseAnimation();
                    _animatedWindows.Add(targetWindow);
                }
                else if (targetWindow.CurrentWindowState == UIBaseWindow.WindowState.Closed)
                {
                    targetWindow.gameObject.SetActive(false);
                }
                else if (targetWindow.CurrentWindowState == UIBaseWindow.WindowState.OpenAnimation)
                {
                    targetWindow.SwitchState(UIBaseWindow.WindowState.CloseAnimation);
                    targetWindow.windowAnimation.StopAnimation();
                    targetWindow.windowAnimation.PlayCloseAnimation();
                }
            }
            else
            {
                targetWindow.gameObject.SetActive(false);
                targetWindow.SwitchState(UIBaseWindow.WindowState.Closed);
            }
        }

        public void HideImmediately(UIBaseWindow targetWindow)
        {
            if (targetWindow.windowAnimation)
            {
                targetWindow.windowAnimation.HideImmediately();
            }
        }
        public void ShowImmediately(UIBaseWindow targetWindow)
        {
            if (targetWindow.windowAnimation)
            {
                targetWindow.windowAnimation.ShowImmediately();
            }
        }

        public void RemoveWindowAnimation(UIBaseWindow targetWindow)
        {
            _animatedWindows.Remove(targetWindow);
            _stoppedAnimations.Remove(targetWindow);
        }

        #endregion

        #region Utils

        private void AnimateWindow(UIBaseWindow window)
        {
            switch (window.CurrentWindowState)
            {
                case UIBaseWindow.WindowState.CloseAnimation:
                    window.windowAnimation.UpdateAnimation();
                    if (!window.windowAnimation.IsAnimationPlaying())
                    {
                        window.SwitchState(UIBaseWindow.WindowState.Closed);
                        StopWindowAnimation(window);
                    }
                    break;
                case UIBaseWindow.WindowState.OpenAnimation:
                    window.windowAnimation.UpdateAnimation();
                    if (!window.windowAnimation.IsAnimationPlaying())
                    {
                        window.SwitchState(UIBaseWindow.WindowState.Open);
                        StopWindowAnimation(window);
                    }
                    break;
                default:
                    //
                    StopWindowAnimation(window);
                    break;
            }
        }

        private void StopWindowAnimation(UIBaseWindow window)
        {
            _stoppedAnimations.Add(window);
            if (window.CurrentWindowState == UIBaseWindow.WindowState.Closed)
            {
                window.gameObject.SetActive(false);
            }
        }

        #endregion

    }
}