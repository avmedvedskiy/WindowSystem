using UnityEngine;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace UISystem
{
    public class UIMainController : MonoBehaviour
    {
        [SerializeField] private UIBaseWindow[] _preloadedWindows;
        [SerializeField] private RectTransform _windowsRoot;
        [SerializeField] private RectTransform _sortedWindowsRoot;
        [SerializeField] private RectTransform _preloadedWindowsRoot;
        [SerializeField] private UIWindowAnimationController _animationController;

        /// <summary>
        /// u can set up a default popupdatabase, or use a set method for late initialization(like bundles)
        /// </summary>
        [SerializeField] private PopupDatabase _popupDatabase;

        private readonly Dictionary<string, UIBaseWindow> _loadedWindows = new();
        private readonly List<int> _indexes = new();
        private readonly List<UIBaseWindow> _activeWindowsQueue = new();
        private readonly Queue<UIBaseWindow> _toOpenWindowsQueue = new();
        
        public event Action<UIBaseWindow> OnWindowOpened;
        public event Action<UIBaseWindow> OnWindowClosed;

        public void SetExternalDatabase(PopupDatabase popupDatabase)
        {
            _popupDatabase = popupDatabase;
        }
        
        public async UniTask<T> GetWindowAsync<T>(string nameId) where T : UIBaseWindow
        {
            if (HasWindow(nameId))
                return _loadedWindows[nameId] as T;

            var (prefab, index) = await _popupDatabase.GetWindowAsync<T>(nameId);
            return LoadWindowByPrefab(prefab, true, index);
        }

        public T GetWindow<T>(string nameId) where T : UIBaseWindow
        {
            if (HasWindow(nameId))
                return _loadedWindows[nameId] as T;

            var prefab = _popupDatabase.GetWindow<T>(nameId);
            return LoadWindowByPrefab(prefab.Item1, true, prefab.Item2);
        }

        private T LoadWindowByPrefab<T>(T prefabComponent, bool attachToUIRoot = false, int index = -1)
            where T : UIBaseWindow
        {
            T window = attachToUIRoot
                ? (Instantiate(prefabComponent, index > -1 ? _sortedWindowsRoot : _windowsRoot, false))
                : Instantiate(prefabComponent);

            if (index > -1)
            {
                int i = GetLocalIndex(index);
                window.transform.SetSiblingIndex(i);
            }

            window.name = prefabComponent.name;
            AddWindow(window);
            return window;
        }

        private bool HasWindow(string nameId) => _loadedWindows.ContainsKey(nameId);

        private void AddWindow(UIBaseWindow window)
        {
            _loadedWindows.Add(window.NameId, window);
            window.CreateWindow(this);
        }

        public void RemoveWindow(UIBaseWindow window)
        {
            _loadedWindows.Remove(window.NameId);
            if (AnimationController != null)
            {
                AnimationController.RemoveWindowAnimation(window);
            }

            RemoveWindowFromQueue(window);
            window.DestroyWindow();
        }

        internal void ReportOpen(UIBaseWindow window)
        {
            OnWindowOpened?.Invoke(window);
        }

        internal void ReportClose(UIBaseWindow window)
        {
            OnWindowClosed?.Invoke(window);
        }

        internal void AddWindowToQueue(UIBaseWindow window)
        {
            _activeWindowsQueue.Remove(window);
            _activeWindowsQueue.Add(window);
        }

        internal void OpenInQueue(UIBaseWindow window)
        {
            if (_activeWindowsQueue.Count > 1)
            {
                if (!_toOpenWindowsQueue.Contains(window))
                {
                    _toOpenWindowsQueue.Enqueue(window);
                }
            }
            else
                window.OpenWindow();
        }

        private void TryOpenWindowFromOpenQueue()
        {
            if (_activeWindowsQueue.Count == 1)
            {
                if (_toOpenWindowsQueue.Count > 0)
                {
                    _toOpenWindowsQueue.Dequeue().OpenWindow();
                }
            }
        }

        public void RemoveWindowFromQueue(UIBaseWindow window)
        {
            _activeWindowsQueue.Remove(window);
            if (_activeWindowsQueue.Count == 1)
                TryOpenWindowFromOpenQueue();
        }

        public void AutoCloseAllWindow()
        {
            for (int i = _activeWindowsQueue.Count - 1; i >= 0; i--)
            {
                _activeWindowsQueue[i].CloseWindow();
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                int count = _activeWindowsQueue.Count;
                if (count > 0)
                {
                    var lastWindow = _activeWindowsQueue[count - 1];
                    lastWindow.TryCloseWindowByEscapeButton();
                }
            }
        }

        protected void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            foreach (var windows in _preloadedWindows)
            {
                var window = Instantiate(windows, _preloadedWindowsRoot, false);
                AddWindow(window);
            }
        }

        private int GetLocalIndex(int index)
        {
            int count = _indexes.Count;
            for (int i = 0; i < count; i++)
            {
                if (_indexes[i] > index)
                {
                    _indexes.Insert(i, index);
                    return i;
                }
            }

            _indexes.Add(index);
            return count;
        }


        #region internal

        internal static UIMainController Instance { get; private set; }
        internal UIWindowAnimationController AnimationController => _animationController;

        #endregion
    }
}