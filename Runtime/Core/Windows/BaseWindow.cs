using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public abstract class BaseWindow<TPayload> : MonoBehaviour, IWindow<TPayload>
    {
        [SerializeField] private BaseWindowAnimation _windowAnimation;
        public string Id { get; set; }
        public Status Status { get; set; }

        private IWindowService _parent;
        IWindowService IClosedWindow.Parent
        {
            get => _parent;
            set => _parent = value;
        }

        async UniTask IWindow<TPayload>.OpenAsync(TPayload payload)
        {
            Status = Status.Opening;
            await OpenAsync(payload);
            Status = Status.Opened;
        }

        async UniTask IClosedWindow.CloseAsync()
        {
            Status = Status.Closing;
            await CloseAsync();
            Status = Status.Closed;
        } 
        protected virtual UniTask OpenAsync(TPayload payload) => _windowAnimation.OpenAnimation();
        protected virtual UniTask CloseAsync() => _windowAnimation.CloseAnimation();

        public void CloseWindow()
        {
            _parent
                .CloseAsync(Id)
                .Forget();
        }
        
        private void OnValidate()
        {
            _windowAnimation ??= GetComponent<BaseWindowAnimation>();
        }
    }
}