using Cysharp.Threading.Tasks;

namespace UISystem
{
    public abstract class BaseResultPopup<TPayload,TResult> : BaseWindow<TPayload>, IResultWindow<TResult>
    {
        public UniTaskCompletionSource<TResult> Result { get; private set; }

        protected override UniTask OnOpenAsync(TPayload payload)
        {
            Result = new();
            return base.OnOpenAsync(payload);
        }
    }
}