using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IWindow<TPayload> : IClosedWindow
    {
        TPayload Payload { get; }
        UniTask OpenAsync(TPayload payload);
    }

    public interface IWindow : IClosedWindow
    {
        UniTask OpenAsync();
    }
}