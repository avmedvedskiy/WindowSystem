using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace UISystem
{
    public abstract class UIBaseWindow : MonoBehaviour
    {
        public enum WindowState
        {
            Open,
            Closed,
            CloseAnimation,
            OpenAnimation
        }

        [SerializeField] private string _nameId;
        [SerializeField] private WindowState _currentState = WindowState.Closed;

        /// <summary>
        /// Window will not be added to Queue
        /// </summary>
        [Tooltip("Window will not be added to Queue")] [SerializeField]
        private bool _preventQueue;

        /// <summary>
        /// Window does not react to Escape
        /// </summary>
        [Tooltip("Window does not react to Escape")] [SerializeField]
        private bool _preventEscape;

        [SerializeField] private UIBaseWindowAnimation _windowAnimation;

        public event Action<WindowState> OnCurrentWindowState;

        public WindowState CurrentWindowState
        {
            get => _currentState;
            set
            {
                if (value != _currentState)
                {
                    _currentState = value;
                    OnCurrentWindowState?.Invoke(_currentState);
                }
            }
        }

        public string NameId => _nameId;
        public bool PreventQueue => _preventQueue;
        public bool PreventEscape => _preventEscape;

        public UIBaseWindowAnimation WindowAnimation => _windowAnimation;

        public event Action OnWindowOpen;
        public event Action OnWindowClosed;

        public void SwitchWindow()
        {
            Controller.AutoCloseAllWindow();
            OpenWindow();
        }

        [ContextMenu("OpenWindow")]
        public virtual void OpenWindow()
        {
            if (Controller.AnimationController != null)
            {
                Controller.AnimationController.PlayOpenAnimation(this);
            }
            else
            {
                gameObject.SetActive(true);
                SwitchState(WindowState.Open);
            }

            if (!PreventQueue)
            {
                Controller.AddWindowToQueue(this);
            }

            Controller.ReportOpen(this);
            OnWindowOpen?.Invoke();
        }

        [ContextMenu("OpenWindowInQueue")]
        public virtual void OpenWindowInQueue()
        {
            Controller.OpenInQueue(this);
        }

        public virtual void CloseWindow()
        {
            if (Controller != null)
            {
                if (Controller.AnimationController != null)
                {
                    Controller.AnimationController.PlayCloseAnimation(this);
                    Controller.RemoveWindowFromQueue(this);
                }
                else
                {
                    HideWindowImmediately();
                }

                Controller.ReportClose(this);
                OnWindowClosed?.Invoke();
            }
        }

        public virtual void TryCloseWindowByEscapeButton()
        {
            if (!_preventEscape)
                CloseWindow();
        }

        public virtual void HideWindowImmediately()
        {
            if (Controller.AnimationController != null)
            {
                Controller.AnimationController.HideImmediately(this);
            }

            gameObject.SetActive(false);
            SwitchState(WindowState.Closed);

            Controller.RemoveWindowFromQueue(this);
            Controller.ReportClose(this);
        }

        public virtual void ShowWindowImmediately()
        {
            if (Controller.AnimationController != null)
            {
                Controller.AnimationController.ShowImmediately(this);
            }

            gameObject.SetActive(true);
            SwitchState(WindowState.Open);
            if (!PreventQueue)
            {
                Controller.AddWindowToQueue(this);
            }

            Controller.ReportOpen(this);
            OnWindowOpen?.Invoke();
        }

        public bool IsWindowCompletelyOpen() => _currentState == WindowState.Open;

        public bool IsWindowCompletelyClosed() => _currentState == WindowState.Closed;

        protected bool IsWindowVisible() =>
            _currentState == WindowState.OpenAnimation || _currentState == WindowState.Open;

        internal void DestroyWindow()
        {
            //maybe need to change it to controller
            Addressables.ReleaseInstance(gameObject);
        }

        internal void SwitchState(WindowState state)
        {
            CurrentWindowState = state;
        }

        private UIMainController Controller { get; set; }

        internal void CreateWindow(UIMainController controller)
        {
            Controller = controller;
        }

        private void OnValidate()
        {
            _windowAnimation = gameObject.GetComponent<UIBaseWindowAnimation>();
            _nameId = gameObject.name;
        }
    }
}