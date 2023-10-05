using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UISystem
{
    public class UIWindowAnimationController : MonoBehaviour
    {
        private readonly List<UIBaseWindow> _animatedWindows = new(); 
        private readonly List<UIBaseWindow> _stoppedAnimations = new();


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

        public void PlayOpenAnimation(UIBaseWindow targetWindow)
        {
            targetWindow.gameObject.SetActive(true);
            if (targetWindow.WindowAnimation)
            {
                if (targetWindow.CurrentWindowState == UIBaseWindow.WindowState.Closed)
                {
                    targetWindow.SwitchState(UIBaseWindow.WindowState.OpenAnimation);
                    targetWindow.WindowAnimation.PlayOpenAnimation();
                    _animatedWindows.Add(targetWindow);
                }
                else if (targetWindow.CurrentWindowState == UIBaseWindow.WindowState.CloseAnimation)
                {
                    targetWindow.SwitchState(UIBaseWindow.WindowState.OpenAnimation);
                    targetWindow.WindowAnimation.StopAnimation();
                    targetWindow.WindowAnimation.PlayOpenAnimation();
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

            if (targetWindow.WindowAnimation != null)
            {
                if (targetWindow.CurrentWindowState == UIBaseWindow.WindowState.Open)
                {
                    targetWindow.SwitchState(UIBaseWindow.WindowState.CloseAnimation);
                    targetWindow.WindowAnimation.PlayCloseAnimation();
                    _animatedWindows.Add(targetWindow);
                }
                else if (targetWindow.CurrentWindowState == UIBaseWindow.WindowState.Closed)
                {
                    targetWindow.gameObject.SetActive(false);
                }
                else if (targetWindow.CurrentWindowState == UIBaseWindow.WindowState.OpenAnimation)
                {
                    targetWindow.SwitchState(UIBaseWindow.WindowState.CloseAnimation);
                    targetWindow.WindowAnimation.StopAnimation();
                    targetWindow.WindowAnimation.PlayCloseAnimation();
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
            if (targetWindow.WindowAnimation)
            {
                targetWindow.WindowAnimation.HideImmediately();
            }
        }

        public void ShowImmediately(UIBaseWindow targetWindow)
        {
            if (targetWindow.WindowAnimation)
            {
                targetWindow.WindowAnimation.ShowImmediately();
            }
        }

        public void RemoveWindowAnimation(UIBaseWindow targetWindow)
        {
            _animatedWindows.Remove(targetWindow);
            _stoppedAnimations.Remove(targetWindow);
        }

        private void AnimateWindow(UIBaseWindow window)
        {
            switch (window.CurrentWindowState)
            {
                case UIBaseWindow.WindowState.CloseAnimation:
                    window.WindowAnimation.UpdateAnimation();
                    if (!window.WindowAnimation.IsAnimationPlaying())
                    {
                        window.SwitchState(UIBaseWindow.WindowState.Closed);
                        StopWindowAnimation(window);
                    }

                    break;
                case UIBaseWindow.WindowState.OpenAnimation:
                    window.WindowAnimation.UpdateAnimation();
                    if (!window.WindowAnimation.IsAnimationPlaying())
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
    }
}