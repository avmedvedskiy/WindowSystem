using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IOpenedWindow<in TPayload> : IClosedWindow
    {
        UniTask OpenAsync(TPayload payload = default);
    }
}