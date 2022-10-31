using Cysharp.Threading.Tasks;
using UISystem;

public static class UIWindowAsyncExtensions
{
    public static async UniTask OpenWindowAsync(this UIBaseWindow window)
    {
        window.OpenWindow();
        await UniTask.WaitUntil(window.IsWindowCompletelyOpen);
    }

    public static async UniTask OpenWindowInQueueAsync(this UIBaseWindow window)
    {
        window.OpenWindowInQueue();
        await UniTask.WaitUntil(window.IsWindowCompletelyOpen);
    }

    public static async UniTask CloseWindowAsync(this UIBaseWindow window)
    {
        window.CloseWindow();
        await UniTask.WaitUntil(window.IsWindowCompletelyClosed);
    }

    public static async UniTask<T> GetAndOpenWindowAsync<T>(this UIMainController controller, string windowId,
        bool inQueue)
        where T : UIBaseWindow
    {
        var window = await controller.GetWindowAsync<T>(windowId);
        if (inQueue)
            await window.OpenWindowInQueueAsync();
        else
            await window.OpenWindowAsync();

        await UniTask.WaitUntil(window.IsWindowCompletelyOpen);
        return window;
    }
}