using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IWindow<TPayload> : IClosedWindow
    {
        TPayload Payload { get; }
        internal UniTask OpenAsync(TPayload payload);
    }

    public interface IWindow : IClosedWindow
    {
        internal UniTask OpenAsync();
    }
}