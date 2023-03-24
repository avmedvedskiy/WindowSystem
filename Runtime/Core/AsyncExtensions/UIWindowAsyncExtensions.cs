using Cysharp.Threading.Tasks;
using UISystem;

public static class UIWindowAsyncExtensions
{
    public static UniTask OpenWindowAsync(this UIBaseWindow window)
    {
        window.OpenWindow();
        return UniTask.WaitUntil(window.IsWindowCompletelyOpen);
    }

    public static UniTask OpenWindowInQueueAsync(this UIBaseWindow window)
    {
        window.OpenWindowInQueue();
        return UniTask.WaitUntil(window.IsWindowCompletelyOpen);
    }

    public static UniTask CloseWindowAsync(this UIBaseWindow window)
    {
        window.CloseWindow();
        return UniTask.WaitUntil(window.IsWindowCompletelyClosed);
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