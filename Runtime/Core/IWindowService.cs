using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IWindowService
    {
        event Action<IClosedWindow> OnWindowOpened;
        event Action<IClosedWindow> OnWindowClosed;
        Transform Root { get; }
        UniTask<TWindow> OpenAsync<TWindow, TPayload>(string windowId, TPayload payload = default, bool inQueue = false)
            where TWindow : IWindow<TPayload>;
        UniTask<TWindow> OpenAsync<TWindow>(string windowId, bool inQueue = false)
            where TWindow : IWindow;
        TWindow GetOpenedWindow<TWindow>(string windowId);
        UniTask CloseAsync(string windowId);
    }
}