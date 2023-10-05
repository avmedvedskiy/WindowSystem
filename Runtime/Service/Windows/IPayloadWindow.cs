using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IPayloadWindow<in TPayload> : IClosedWindow
    {
        UniTask OpenAsync(TPayload payload);
    }
}