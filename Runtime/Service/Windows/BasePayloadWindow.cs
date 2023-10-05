using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public abstract class BasePayloadWindow<TPayload> : MonoBehaviour, IPayloadWindow<TPayload>
    {
        [SerializeField] private BaseWindowAnimation _windowAnimation;

        public virtual UniTask OpenAsync(TPayload payload) => _windowAnimation.OpenAnimation();

        public virtual UniTask CloseAsync() => _windowAnimation.CloseAnimation();

        private void OnValidate()
        {
            _windowAnimation ??= GetComponent<BaseWindowAnimation>();
        }
    }
}