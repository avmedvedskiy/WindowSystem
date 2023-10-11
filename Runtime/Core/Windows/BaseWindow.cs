using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public abstract class BaseWindow<TPayload> : MonoBehaviour, IWindow<TPayload>
    {
        public string Id { get; private set; }
        public Status Status { get; private set; }
        public IWindowService Parent { get; private set; }
        
        [SerializeField] private BaseWindowAnimation _windowAnimation;
        
        UniTask IWindow<TPayload>.OpenAsync(TPayload payload) => OpenAsync(payload);
        UniTask IClosedWindow.CloseAsync() => CloseAsync();

        protected virtual UniTask OpenAsync(TPayload payload) => _windowAnimation.OpenAnimation();
        protected virtual UniTask CloseAsync() => _windowAnimation.CloseAnimation();

        public void CloseWindow() =>
            Parent
                .CloseAsync(Id)
                .Forget();

        private void OnValidate() => _windowAnimation ??= GetComponent<BaseWindowAnimation>();

        void IClosedWindow.SetStatus(Status status) => Status = status;

        void IClosedWindow.Initialize(string id,IWindowService parent)
        {
            Id = id;
            Parent = parent;
        }
    }
}