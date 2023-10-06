using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public abstract class BaseWindow<TPayload> : MonoBehaviour, IWindow<TPayload>
    {
        [SerializeField] private BaseWindowAnimation _windowAnimation;
        
        public string Id { get; set; }
        public IWindowService Parent { get; set; }

        UniTask IWindow<TPayload>.OpenAsync(TPayload payload) => OpenAsync(payload);
        UniTask IClosedWindow.CloseAsync() => CloseAsync();
        protected virtual UniTask OpenAsync(TPayload payload) => _windowAnimation.OpenAnimation();
        protected virtual UniTask CloseAsync() => _windowAnimation.CloseAnimation();

        public void CloseWindow()
        {
            Parent.CloseAsync(Id);
        }
        
        private void OnValidate()
        {
            _windowAnimation ??= GetComponent<BaseWindowAnimation>();
        }
    }
}