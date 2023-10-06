using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public class WindowService : MonoBehaviour, IWindowService
    {
        [SerializeField] private WindowFactory _factory;
        [SerializeField] private Transform _root;

        private readonly Dictionary<string, IClosedWindow> _openedWindows = new();
        private readonly Queue<UniTaskCompletionSource> _queue = new();

        
        public async UniTask<TWindow> OpenAsync<TWindow, TPayload>(string windowId, TPayload payload = default)
            where TWindow : IWindow<TPayload>
        {
            var window = await GetWindowAsync<TWindow>(windowId);
            await window.OpenAsync(payload);
            return window;
        }

        public async UniTask<TWindow> OpenInQueueAsync<TWindow, TPayload>(string windowId, TPayload payload = default)
            where TWindow : IWindow<TPayload>
        {
            await WaitInQueue();
            return await OpenAsync<TWindow, TPayload>(windowId, payload);
        }
        
        public async UniTask CloseAsync(string windowId)
        {
            if(!HasWindow(windowId, out _))
                return;
            
            var window = _openedWindows[windowId];
            _openedWindows.Remove(windowId);
            await window.CloseAsync();
            _factory.DestroyWindow(window);
            ProcessQueue();
        }
        
        private async UniTask<TWindow> GetWindowAsync<TWindow>(string windowId) where TWindow : IClosedWindow
        {
            if (HasWindow(windowId, out var existWindow))
                return (TWindow)existWindow;
            
            _openedWindows[windowId] = default; //запоминаем за собой ячейку, чтобы до загрузки уже работала очередь
            var window = await _factory.InstantiateAsync<TWindow>(windowId, _root);
            _openedWindows[windowId] = window;
            window.Id = windowId;
            window.Parent = this;
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
            if (!HasWindowsInQueue() && _queue.Count > 0)
            {
                var process = _queue.Dequeue();
                process?.TrySetResult();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasWindowsInQueue() => _openedWindows.Count != 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasWindow(string windowId, out IClosedWindow window) =>
            _openedWindows.TryGetValue(windowId, out window);

        private void OnValidate()
        {
            _root ??= transform;
            _factory ??= GetComponent<WindowFactory>();
        }
    }
}