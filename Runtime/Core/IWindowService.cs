using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IWindowService
    {
        Transform Root { get; }
        UniTask<TWindow> OpenAsync<TWindow, TPayload>(string windowId, TPayload payload = default, bool inQueue = false)
            where TWindow : IWindow<TPayload>;
        TWindow GetOpenedWindow<TWindow>(string windowId);
        UniTask CloseAsync(string windowId);
    }
}