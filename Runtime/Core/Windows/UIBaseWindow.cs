using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
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

        #region Unity properties

        public string nameId;
        [SerializeField]
        private WindowState _currentState = WindowState.Closed;

        public event Action<WindowState> OnCurrentWindowState;
        public WindowState CurrentWindowState
        {
            get { return _currentState; }
            set
            {
                if (value != _currentState)
                {
                    _currentState = value;
                    OnCurrentWindowState?.Invoke(_currentState);
                }
            }
        }

        public UIBaseWindowAnimation windowAnimation;

        /// <summary>
        /// Window will not be added to Queue
        /// </summary>
        [Tooltip("Window will not be added to Queue")]
        [SerializeField]
        protected bool _preventQueue;
        public bool PreventQueue => _preventQueue;
        
        /// <summary>
        /// Window does not react to Escape
        /// </summary>
        [Tooltip("Window does not react to Escape")]
        [SerializeField]
        protected bool _preventEscape;
        public bool PreventEscape => _preventEscape;

        #endregion

        #region Private fields

        //private List<UIBaseWindow> _childPopups = new List<UIBaseWindow>();
        private UIMainController _controller { get; set; }

        #endregion

        public event Action OnWindowOpen;
        public event Action OnWindowClosed;

        #region Interface

        public void SwitchWindow()
        {
            _controller.AutoCloseAllWindow();
            OpenWindow();
        }

        [ContextMenu("OpenWindow")]
        public virtual void OpenWindow()
        {
            if (_controller.AnimationController != null)
            {
                _controller.AnimationController.PlayOpenAnimation(this);
            }
            else
            {
                gameObject.SetActive(true);
                SwitchState(WindowState.Open);
            }
            if (!_preventQueue)
            {
                _controller.AddWindowToQueue(this);
            }
            _controller.ReportOpen(this);
            OnWindowOpen?.Invoke();

        }

        [ContextMenu("OpenWindowInQueue")]
        public virtual void OpenWindowInQueue()
        {
            _controller.OpenInQueue(this);
        }

        public virtual void CloseWindow()
        {
            if (_controller != null)
            {
                if (_controller.AnimationController != null)
                {
                    _controller.AnimationController.PlayCloseAnimation(this);
                    _controller.RemoveWindowFromQueue(this);
                }
                else
                {
                    HideWindowImmediately();
                }
                _controller.ReportClose(this);
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
            if (_controller.AnimationController != null)
            {
                _controller.AnimationController.HideImmediately(this);
            }
            gameObject.SetActive(false);
            SwitchState(WindowState.Closed);

            _controller.RemoveWindowFromQueue(this);
            _controller.ReportClose(this);
        }

        public virtual void ShowWindowImmediately()
        {
            if (_controller.AnimationController != null)
            {
                _controller.AnimationController.ShowImmediately(this);
            }
            gameObject.SetActive(true);
            SwitchState(WindowState.Open);
            if (!_preventQueue)
            {
                _controller.AddWindowToQueue(this);
            }
            _controller.ReportOpen(this);
            OnWindowOpen?.Invoke();

        }

        public bool IsWindowCompletelyOpen() => _currentState == WindowState.Open;

        public bool IsWindowCompletelyClosed() => _currentState == WindowState.Closed;

        public bool IsWindowVisible() => _currentState == WindowState.OpenAnimation || _currentState == WindowState.Open;

        internal void DestroyWindow()
        {
            //DestroyChilds();
            //maybe need to change it to controller
#if UISYSTEM_ADDRESSABLES
            UnityEngine.AddressableAssets.Addressables.ReleaseInstance(gameObject);
#else
            Destroy(gameObject);
#endif
        }

        internal void SwitchState(WindowState state)
        {
            CurrentWindowState = state;
        }

        public void PreventWindowEscape(bool value)
        {
            _preventEscape = value;
        }
        public void PreventWindowQueue(bool value)
        {
            _preventQueue = value;
        }


        //public List<RectTransform> AdditionalRoots = new List<RectTransform>();
        
        //public void AddChildPopup(UIBaseWindow prefab)
        //{
        //    _childPopups.Add(prefab);
        //}

        //public T AddChildWindow<T>(T window, Sibling sibling = Sibling.SetAsLastSibling, bool active = true) where T : UIBaseWindow
        //{
        //    return AddChildWindow(window, (RectTransform)transform, sibling, active);
        //}

        //public T AddChildWindow<T>(T window, string rootId, Sibling sibling = Sibling.SetAsLastSibling, bool active = true) where T : UIBaseWindow
        //{
        //    var additionalRoot = AdditionalRoots.Find(x => x.name == rootId);
        //    return AddChildWindow(window, additionalRoot, sibling, active);
        //}

        //public void RemoveChildWindow(UIBaseWindow childWindow)
        //{
        //    if (childWindow)
        //    {
        //        _childPopups.Remove(childWindow);
        //        _controller.RemoveWindow(childWindow);
        //    }
        //}

        //public T AddChildWindow<T>(T window, RectTransform root, Sibling sibling = Sibling.SetAsLastSibling, bool active = true) where T : UIBaseWindow
        //{
        //    T existsWindow = _controller.GetWindow<T>(window.nameId);
        //    if (existsWindow != null)
        //    {
        //        return existsWindow;
        //    }

        //    var cloned = window.InstantiateClone(root, sibling);
        //    cloned.gameObject.SetActive(active);
        //    cloned.ParentWindow = this;
        //    _childPopups.Add(cloned);
        //    return cloned;
        //}

        //public T GetChildWindow<T>() where T : UIBaseWindow
        //{
        //    var window = _childPopups.Find(x => x is T);
        //    if (window)
        //    {
        //        return window as T;
        //    }
        //    return null;
        //}

        //public T GetChildWindow<T>(string id) where T : UIBaseWindow
        //{
        //    var window = _childPopups.Find(x => x is T && x.nameId == id);
        //    if (window != null)
        //    {
        //        return window as T;
        //    }
        //    return null;
        //}

        //public void DestroyChilds()
        //{
        //    foreach (var childPopup in _childPopups)
        //    {
        //        _controller.RemoveWindow(childPopup);
        //    }
        //}

#endregion

#region Utils

        internal void CreateWindow(UIMainController controller)
        {
            _controller = controller;
        }
#endregion

    }
}