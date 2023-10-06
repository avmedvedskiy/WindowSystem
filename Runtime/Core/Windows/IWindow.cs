using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IWindow<in TPayload> : IClosedWindow
    {
        UniTask OpenAsync(TPayload payload);
    }
}