using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public class BaseWindow<TPayload> : MonoBehaviour, IOpenedWindow<TPayload>
    {
        [SerializeField] private BaseWindowAnimation _windowAnimation;
        public IWindowAnimation Animation => _windowAnimation;

        public virtual UniTask OpenAsync(TPayload payload = default) => Animation.OpenAnimation();

        public virtual UniTask CloseAsync() => Animation.CloseAnimation();

        private void OnValidate()
        {
            _windowAnimation ??= GetComponent<BaseWindowAnimation>();
        }
    }
}