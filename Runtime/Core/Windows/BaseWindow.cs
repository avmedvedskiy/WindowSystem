using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public abstract class BaseWindow<TPayload> : MonoBehaviour, IWindow<TPayload>
    {
        public event Action<Status> OnStatusChanged;
        public string Id { get; private set; }
        public Status Status { get; private set; }
        public IWindowService Parent { get; private set; }
        public TPayload Payload { get; private set; }
        
        [SerializeField] private BaseWindowAnimation _windowAnimation;

        UniTask IWindow<TPayload>.OpenAsync(TPayload payload)
        {
            Payload = payload;
            return OnOpenAsync(payload);
        }

        UniTask IClosedWindow.CloseAsync() => OnCloseAsync();

        protected virtual UniTask OnOpenAsync(TPayload payload) => _windowAnimation.OpenAnimationAsync();
        protected virtual UniTask OnCloseAsync() => _windowAnimation.CloseAnimationAsync();

        public void CloseWindow() =>
            Parent
                .CloseAsync(Id)
                .Forget();

        private void OnValidate() => _windowAnimation ??= GetComponent<BaseWindowAnimation>();

        void IClosedWindow.SetStatus(Status status)
        {
            Status = status;
            OnStatusChanged?.Invoke(Status);
        }

        void IClosedWindow.Initialize(string id,IWindowService parent)
        {
            Id = id;
            Parent = parent;
        }
    }
}