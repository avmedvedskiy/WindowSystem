using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IWindowService
    {
        UniTask<TWindow> OpenAsync<TWindow, TPayload>(string windowId, TPayload payload = default)
            where TWindow : IWindow<TPayload>;
        UniTask<TWindow> OpenInQueueAsync<TWindow, TPayload>(string windowId, TPayload payload)
            where TWindow : IWindow<TPayload>;
        UniTask CloseAsync(string windowId);
    }
}