using UnityEngine;
using System;
using System.Collections.Generic;

namespace UISystem
{
    public class UIMainController : MonoBehaviour
    {

        [SerializeField] private ReferenceWindow[] preloadedWindows;
        [SerializeField] private RectTransform windowsRoot;
        [SerializeField] private RectTransform sortedWindowsRoot;
        [SerializeField] private RectTransform preloadedWindowsRoot;
        [SerializeField] private UIWindowAnimationController _animationController;


        /// <summary>
        /// u can set up a default popupdatabase, or use a set method for late initialization(like bundles)
        /// </summary>
        [SerializeField] private BasePopupDatabase _popupDatabase;

        private readonly Dictionary<string, UIBaseWindow> _dict = new Dictionary<string, UIBaseWindow>();
        private readonly List<int> _indexes = new List<int>();
        private readonly List<UIBaseWindow> _activeWindowsQueue = new List<UIBaseWindow>();
        private readonly Queue<UIBaseWindow> _toOpenWindowsQueue = new Queue<UIBaseWindow>();

        public AnchorsController AnchorController { get; } = new AnchorsController();
        public event Action<string> OnWindowAdded;
        public event Action<string> OnWindowRemoved;
        public event Action<UIBaseWindow> OnWindowOpened;
        public event Action<UIBaseWindow> OnWindowClosed;

        #region Interface

        public void SetExternalDatabase(BasePopupDatabase popupDatabase)
        {
            _popupDatabase = popupDatabase;
        }


#if UISYSTEM_ADDRESSABLES
        public async Cysharp.Threading.Tasks.UniTask<T> GetWindowAsync<T>(string nameId) where T : UIBaseWindow
        {
            if (HasWindow(nameId))
                return _dict[nameId] as T;

            if (_popupDatabase is IAsyncPopupBase db)
            {
                var (prefab, index) = await db.GetWindowAsync<T>(nameId);
                return LoadWindowByPrefab(prefab, true, index);
            }

            return GetWindow<T>(nameId);
        }
#endif

        public T GetWindow<T>(string nameId) where T : UIBaseWindow
        {
            if (HasWindow(name))
                return _dict[name] as T;

            var prefab = _popupDatabase.GetWindow<T>(nameId, out var index);
            return LoadWindowByPrefab(prefab, true, index);
        }

        private T LoadWindowByPrefab<T>(T prefabComponent, bool attachToUIRoot = false, int index = -1)
            where T : UIBaseWindow
        {
            T window = attachToUIRoot
                ? (Instantiate(prefabComponent, index > -1 ? sortedWindowsRoot : windowsRoot, false))
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
        
        private bool HasWindow(string nameId) => _dict.ContainsKey(nameId);

        private void AddWindow(UIBaseWindow window)
        {
            _dict.Add(window.nameId, window);
            window.CreateWindow(this);
            OnWindowAdded?.Invoke(window.nameId);
        }

        public void RemoveWindow(UIBaseWindow window)
        {
            OnWindowRemoved?.Invoke(window.nameId);

            _dict.Remove(window.nameId);
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
                var lastWindow = _activeWindowsQueue[i];
                if (!(lastWindow is IUIDisableAutoClose))
                    lastWindow.CloseWindow();
            }
        }

        public void AutoCloseAllWindow<T>()
        {
            for (int i = _activeWindowsQueue.Count - 1; i >= 0; i--)
            {
                var lastWindow = _activeWindowsQueue[i];
                if (!(lastWindow is IUIDisableAutoClose) && lastWindow is not T)
                    lastWindow.CloseWindow();
            }
        }

        public void ForEach<T>(Action<T> action) where T : class
        {
            foreach (var window in _dict.Values)
            {
                if (window is T w)
                {
                    action?.Invoke(w);
                }
            }
        }

        #endregion

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

        #region Utils

        protected void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            foreach (var windowReference in preloadedWindows)
            {
                var window = (Instantiate(windowReference.GetSource(), preloadedWindowsRoot, false));
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

        #endregion


        #region internal

        internal static UIMainController Instance { get; private set; }
        internal UIWindowAnimationController AnimationController => _animationController;

        #endregion
    }
}