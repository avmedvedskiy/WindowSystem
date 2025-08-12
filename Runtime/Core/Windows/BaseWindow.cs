using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public abstract class BaseWindow<TPayload> : MonoBehaviour, IWindow<TPayload>
    {
        public event Action<Status> OnStatusChanged;
        [field: SerializeField] public bool IgnoreInQueue { get;private set; }
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

        protected virtual UniTask OnOpenAsync(TPayload payload) =>
            Status != Status.Opened
                ? _windowAnimation.OpenAnimationAsync()
                : UniTask.CompletedTask;

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

        void IClosedWindow.Initialize(string id, IWindowService parent)
        {
            Id = id;
            Parent = parent;
        }
    }
    
    
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        [field: SerializeField] public bool IgnoreInQueue { get;private set; }
        
        public event Action<Status> OnStatusChanged;
        public string Id { get; private set; }
        public Status Status { get; private set; }
        public IWindowService Parent { get; private set; }

        [SerializeField] private BaseWindowAnimation _windowAnimation;

        UniTask IWindow.OpenAsync()
        {
            return OnOpenAsync();
        }

        UniTask IClosedWindow.CloseAsync() => OnCloseAsync();

        protected virtual UniTask OnOpenAsync() =>
            Status != Status.Opened
                ? _windowAnimation.OpenAnimationAsync()
                : UniTask.CompletedTask;

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

        void IClosedWindow.Initialize(string id, IWindowService parent)
        {
            Id = id;
            Parent = parent;
        }
    }
}