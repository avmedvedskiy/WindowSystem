using Cysharp.Threading.Tasks;

namespace UISystem
{
    public static class WindowServiceExtensions
    {
        public static async UniTask<IWindow<TPayload>> ReopenInQueue<TPayload>(this IWindow<TPayload> window)
        {
            return await window.Parent.OpenAsync<IWindow<TPayload>, TPayload>(window.Id, window.Payload, true);
        }
        
        public static async UniTask AndWaitClose<TWindow>(this UniTask<TWindow> window)
            where TWindow : IClosedWindow
        {
            var w = await window;
            await UniTask.WaitWhile(() => w.Status != Status.Closed);
        }

        public static async UniTask<TResult> AndWaitResult<TWindow, TResult>(this UniTask<TWindow> window)
            where TWindow : IResultWindow<TResult>, IClosedWindow
        {
            var w = await window;
            return await w.Result.Task;
        }

        public static async UniTask<TResult> OpenPopup<TWindow, TPayload, TResult>(
            this IWindowService service,
            string windowName,
            TPayload payload = default)
            where TWindow : IWindow<TPayload>, IResultWindow<TResult>

        {
            var result = await service
                .OpenAsync<TWindow, TPayload>(windowName, payload)
                .AndWaitResult<TWindow, TResult>();
            await service.CloseAsync(windowName);
            return result;
        }
    }
}