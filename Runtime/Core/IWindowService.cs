using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IWindowService
    {
        Transform Root { get; }
        UniTask<TWindow> OpenAsync<TWindow, TPayload>(string windowId, TPayload payload = default)
            where TWindow : IWindow<TPayload>;
        UniTask<TWindow> OpenInQueueAsync<TWindow, TPayload>(string windowId, TPayload payload = default)
            where TWindow : IWindow<TPayload>;
        TWindow GetOpenedWindow<TWindow>(string windowId);
        UniTask CloseAsync(string windowId);
    }
}