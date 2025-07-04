using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public class WindowService : IWindowService
    {
        private readonly IWindowFactory _windowFactory;
        private readonly IWindowRootProvider _windowRootProvider;

        private readonly Dictionary<string, IClosedWindow> _openedWindows = new();
        private readonly Queue<UniTaskCompletionSource> _queue = new();

        public event Action<IClosedWindow> OnWindowOpened;
        public event Action<IClosedWindow> OnWindowClosed;
        public Transform Root => _windowRootProvider.Root;

        public WindowService(IWindowFactory windowFactory, IWindowRootProvider windowRootProvider)
        {
            _windowFactory = windowFactory;
            _windowRootProvider = windowRootProvider;
        }

        public async UniTask<TWindow> OpenAsync<TWindow, TPayload>(string windowId,
            TPayload payload = default,
            bool inQueue = false)
            where TWindow : IWindow<TPayload>
        {
            if (inQueue)
                await WaitInQueue();

            TWindow window;
            if (HasWindow(windowId, out var result))
            {
                window = (TWindow)result;
                await window.OpenAsync(payload);
            }
            else
            {
                window = await CreateNewWindow<TWindow>(windowId);
                window.SetStatus(Status.Opening);
                await window.OpenAsync(payload);
                window.SetStatus(Status.Opened);
                OnWindowOpened?.Invoke(window);
            }

            return window;
        }

        public async UniTask<TWindow> OpenAsync<TWindow>(string windowId, bool inQueue = false)
            where TWindow : IWindow
        {
            if (inQueue)
                await WaitInQueue();

            TWindow window;
            if (HasWindow(windowId, out var result))
            {
                window = (TWindow)result;
                await window.OpenAsync();
            }
            else
            {
                window = await CreateNewWindow<TWindow>(windowId);
                window.SetStatus(Status.Opening);
                await window.OpenAsync();
                window.SetStatus(Status.Opened);
                OnWindowOpened?.Invoke(window);
            }

            return window;
        }

        public TWindow GetOpenedWindow<TWindow>(string windowId)
        {
            if (HasWindow(windowId, out var window))
                return (TWindow)window;
            return default;
        }

        public async UniTask CloseAsync(string windowId)
        {
            if (!HasWindow(windowId, out _))
                return;

            var window = _openedWindows[windowId];
            _openedWindows.Remove(windowId);
            window.SetStatus(Status.Closing);
            await window.CloseAsync();
            window.SetStatus(Status.Closed);
            OnWindowClosed?.Invoke(window);
            _windowFactory.DestroyWindow(window);
            ProcessQueue();
        }

        private async UniTask<TWindow> CreateNewWindow<TWindow>(string windowId) where TWindow : IClosedWindow
        {
            _openedWindows[windowId] = null; //запоминаем за собой ячейку, чтобы до загрузки уже работала очередь
            var window = await _windowFactory.InstantiateAsync<TWindow>(windowId, _windowRootProvider.Root);
            _openedWindows[windowId] = window;
            window.Initialize(windowId, this);
            return window;
        }

        private UniTask WaitInQueue()
        {
            var completionSource = new UniTaskCompletionSource();
            _queue.Enqueue(completionSource);
            return completionSource.Task;
        }

        private void ProcessQueue()
        {
            if (HasOpenedWindows() == false && HasWindowsInQueue())
            {
                _queue
                    .Dequeue()
                    .TrySetResult();
            }
        }

        private bool HasWindowsInQueue()
        {
            return _queue.Count > 0;
        }

        private bool HasOpenedWindows() => _openedWindows.Count != 0;

        private bool HasWindow(string windowId, out IClosedWindow window) =>
            _openedWindows.TryGetValue(windowId, out window);
    }
}